using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReferee : MonoBehaviourPunCallbacks
{
    public static GameReferee Instance;
    public PhotonView PV;

    public Board Board;

    public GamePlayer[] Players;
    public int[] chosenElements = {-1, -1};

    public int GoldBonus;
    public int MaxManaBonus;
    public int MaxFoodBonus;

    private int FIRE = 0;
    private int WATER = 1;
    private int EARTH = 2;
    private int AIR = 3;

    private const int NR_CARD_DRAGONS_START = 3;
    private const int NR_CARD_SPELLS_START = 3;

    public GameObject DragonCardPrefab;
    public GameObject SpellCardPrefab;
    private CardsGenerator physicalCardGenerator;

    public void Awake()
    {
        Instance = this;
    }

    public void CallRPCMethod(string name, params object[] parameters)
    {
        if (PlayerInfoScene.Instance.PhotonPresent != 0)
        {
            PV.RPC(name, RpcTarget.All, parameters);
        }
        else
        {
            typeof(GameReferee).GetMethod(name).Invoke(Instance, parameters);
        }
    }

    [PunRPC]
    public void SetPlayerInfo(int playerId, int chosenElement)
    {
        Debug.Log("Received setting for player " + playerId);
        chosenElements[playerId - 1] = chosenElement;

        if(chosenElements[0] != -1 && chosenElements[1] != -1)
        {
            Debug.Log($"Elements {chosenElements[0]} {chosenElements[1]}");
            StartGame();
        }
    }
     
    [PunRPC]
    public void CreateOnBoardDragon(int[] targetPosition, int type, int race, int owner)
    {
        CardDragon card = new CardDragon(type, race, owner);
        OnBoardDragon onBoardDragon = new OnBoardDragon(new Vector2Int(targetPosition[0], targetPosition[1]), Board, card);
        onBoardDragon.UpdateOnBoard();
    }

    [PunRPC]
    public void MoveOnBoardDragon(int[] startPos, int[] destPos)
    {
        Debug.Log($"Move On Board RPC Called {startPos[0]} {startPos[0]} {startPos[0]} {startPos[0]}");
        OnBoardDragon OnBoardDragon = (OnBoardDragon)Board.Destructables[startPos[1], startPos[0]];
        OnBoardDragon.MoveOn(new Vector2Int(destPos[0], destPos[1]));
        OnBoardDragon.UpdateOnBoard();
    }

    [PunRPC]
    public void PlaySpell(int[] targetPosition, int spellID, int race, int owner)
    {
        //Take the references before the code puts the pointer to null if they die
        var dragons = Board.GetAllDragons();
        
        //Deal damage or whatever
        var cardSpell = CardSpellCreator.GenerateCardSpell(spellID, owner, race);
        cardSpell.GoPlay(new Vector2Int(targetPosition[0], targetPosition[1]));
        
        //In case a dragon died or received damage, update the visuals
        foreach (var dragon in dragons)
        {
            dragon.UpdateOnBoard();
        }
    }

    [PunRPC]
    public void AttackOnBoardDragon(int[] allyPos, int[] attackedPos)
    {
        int x = attackedPos[0];
        int y = attackedPos[1];

        OnBoardDragon chosenDragon = (OnBoardDragon)Board.Destructables[allyPos[1], allyPos[0]];
        OnBoardDestructible attackedDestructible = Board.Destructables[y, x];

        chosenDragon.AttackOn(new Vector2Int(x, y));

        if (x == 0 || x == Board.Width - 1 || y == 0 || y == Board.Height - 1)
        {
            //Attacked a building
            return;
        }
        //Attacked a dragon
        var attackedDragon = (OnBoardDragon) attackedDestructible;
        chosenDragon.UpdateOnBoard();
        attackedDragon.UpdateOnBoard();
    }

    void Start()
    {
        Board = new Board(this);
        //Debug.Log("Direct mesajul");

        physicalCardGenerator = new CardsGenerator(DragonCardPrefab, SpellCardPrefab);

        GoldBonus = 5;
        MaxManaBonus = 1;
        MaxFoodBonus = 1;

        Players = new GamePlayer[2];
        Players[0] = new GamePlayer(1, WATER, Board, true, physicalCardGenerator);
        Players[1] = new GamePlayer(2, WATER, Board, false, physicalCardGenerator);
        //Players[PlayerInfoScene.Instance.playerId - 1].Race = PlayerInfoScene.Instance.chosenElement;
        //Debug.Log("Direct mesajul");
        if(PlayerInfoScene.Instance.PhotonPresent == 0)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        if (PlayerInfoScene.Instance.PhotonPresent != 0)
        {
            Players[0].Race = chosenElements[0];
            Players[1].Race = chosenElements[1];
        }

        for (int i = 0; i < NR_CARD_DRAGONS_START; i++) 
        {
            GiveCardDragon(0);
            GiveCardDragon(1);
            Debug.Log($"Given {i+1} card");
        }
        //Debug.Log("Direct mesajul");

        for (int i = 0; i < NR_CARD_SPELLS_START; i++)
        {
            Debug.Log($"Given {i + 1} spell");
            GiveCardSpell(0);
            GiveCardSpell(1);
            Debug.Log($"Given {i + 1} spell");
        }
        //Debug.Log("Direct mesajul");
        Players[0].ResetTurn(GoldBonus, MaxManaBonus, MaxFoodBonus);
    }

    void GiveCardDragon(int index)
    {
        var barrack = (OnBoardBarrack)Board.Buildings[3 * index + 1];
        Card card = barrack.ResetTurn(Players[index].Race);
        card.Board = Board;
        Players[index].ReceiveCard(card);
    }

    void GiveCardSpell(int index)
    {
        var mageTower = (OnBoardMageTower)Board.Buildings[3 * index + 2];
        Card card = mageTower.ResetTurn(Players[index].Race);
        card.Board = Board;
        Players[index].ReceiveCard(card);
    }

    void PassTurnToPlayer(int index)
    {
        Players[1-index].EndedTurn = false;
        Players[index].HisTurn = true;
        Players[index].ResetTurn(GoldBonus, MaxManaBonus, MaxFoodBonus);

        GiveCardDragon(index);
        GiveCardSpell(index);
        Board.ResetTurn(Players[index].ID);
    }
    // Update is called once per frame
    void Update()
    {
        if (Players[0].EndedTurn)
        {
            PassTurnToPlayer(1);
        }
        else if (Players[1].EndedTurn)
        {
            PassTurnToPlayer(0);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Board.DebugDestructibles();
        }
    }
}
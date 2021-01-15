using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private int turn = 0;

    private const int NR_CARD_DRAGONS_START = 3;
    private const int NR_CARD_SPELLS_START = 2;

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
            Debug.Log("StartGame called from SetPlayerInfo()");
        }
    }
     
    [PunRPC]
    public void CreateOnBoardDragon(int[] targetPosition, int type, int race, int owner)
    {
        CardDragon card = new CardDragon(type, race, owner);
        OnBoardDragon onBoardDragon = new OnBoardDragon(new Vector2Int(targetPosition[1], targetPosition[0]), Board, card);
        onBoardDragon.UpdateOnBoard();
        if (!IsMe(owner))
        {
            physicalCardGenerator.UpdateLastCard(race, type);
        }
    }

    [PunRPC]
    public void MoveOnBoardDragon(int[] startPos, int[] destPos)
    {
        Debug.Log($"Move On Board RPC Called {startPos[0]} {startPos[1]} {destPos[0]} {destPos[1]}");
        OnBoardDragon OnBoardDragon = (OnBoardDragon)Board.Destructables[startPos[0], startPos[1]];
        OnBoardDragon.MoveOn(new Vector2Int(destPos[1], destPos[0]));
        OnBoardDragon.UpdateOnBoard();
    }

    [PunRPC]
    public void PlaySpell(int[] targetPosition, int spellID, int race, int owner)
    {
        Random.InitState(12345);
        //Take the references before the code puts the pointer to null if they die
        var dragons = Board.GetAllDragons();
        
        //Deal damage or whatever
        var cardSpell = CardSpellCreator.GenerateCardSpell(spellID, owner, race);
        cardSpell.Board = Board;
        cardSpell.GoPlay(new Vector2Int(targetPosition[0], targetPosition[1]));
        
        //In case a dragon died or received damage, update the visuals
        foreach (var dragon in dragons)
        {
            dragon.UpdateOnBoard();
        }

        if (!IsMe(owner))
        {
            physicalCardGenerator.UpdateLastCard(race, spellID + 4);
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
            var attackedBuilding = (OnBoardBuilding)attackedDestructible;
            attackedBuilding.UpdateStatus();
            return;
        }
        //Attacked a dragon
        var attackedDragon = (OnBoardDragon) attackedDestructible;
        chosenDragon.UpdateOnBoard();
        attackedDragon.UpdateOnBoard();
    }

    [PunRPC]
    public void SwitchTurns(int index)
    {
        Debug.Log("END turn summoned by " + index);
        PassTurnToPlayer(1 - index);
        if(index == 0 && (turn == 3 || turn == 5))
        {
            UpgradeBuildings();
        }
    }

    [PunRPC]
    public void UpdateOtherPlayerQueen(float rotation, int playerID)
    {
        if (IsMe(playerID)) return;

        GameObject otherQueen = GameObject.Find("OpponentCenter");

        otherQueen.transform.localRotation = Quaternion.Euler(new Vector3(0, rotation, 0));

    }

    [PunRPC]
    public void GameOver(int winner, bool surrender=false)
    {
        string info = "";
        if (IsMe(winner))
        {
            info = "You WON!";
            if (surrender)
            {
                info += " Your enemy GAVE UP";
            }
        }
        else
        {
            info = "You LOST the GAME!";
        }
        GameObject.Find("TurnInfoText").GetComponent<Text>().text = info;

        StartCoroutine("EndGame");
    }

    void Start()
    {
        Board = new Board(this);
        //Debug.Log("Direct mesajul");

        physicalCardGenerator = new CardsGenerator(DragonCardPrefab, SpellCardPrefab);

        GoldBonus = 2;
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
            Debug.Log("StartGame called from Start()");
        }
    }

    void StartGame()
    {
        if (PlayerInfoScene.Instance.PhotonPresent != 0)
        {
            Players[0].Race = chosenElements[0];
            Players[1].Race = chosenElements[1];
        }

        int other = 3 - PlayerInfoScene.Instance.playerId;
        int otherRace = Players[other - 1].Race;

        DragonGenerator dragonGenerator = GameObject.Find("DragonGenerator").GetComponent<DragonGenerator>();
        GameObject otherQueen = GameObject.Find("Opponent");
        dragonGenerator.ApplyRaceToQueen(otherQueen, otherRace);

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
        turn = 1;
        UpdateTurnText(0);
        UpdateResources();
        UpdateAllStats();
    }

    void UpgradeBuildings()
    {
        for(int index = 0; index < 2; index++)
        {
            var barrack = (OnBoardBarrack)Board.Buildings[3 * index + 1];
            barrack.Upgrade();
            var mageTower = (OnBoardMageTower)Board.Buildings[3 * index + 2];
            mageTower.Upgrade();
        }
    }

    void GiveCardDragon(int index)
    {
        var barrack = (OnBoardBarrack)Board.Buildings[3 * index + 1];
        Card card = barrack.ResetTurn(Players[index].Race);
        if (card == null) return;
        card.Board = Board;
        Players[index].ReceiveCard(card);
    }

    void GiveCardSpell(int index)
    {
        var mageTower = (OnBoardMageTower)Board.Buildings[3 * index + 2];
        Card card = mageTower.ResetTurn(Players[index].Race);
        if (card == null) return;
        card.Board = Board;
        Players[index].ReceiveCard(card);
    }

    void PassTurnToPlayer(int index)
    {
        if (index == 0)
        {
            turn += 1;
            if(turn % 3 == 0) { GoldBonus += 1; }
        }

        Players[1-index].EndedTurn = false;
        Players[1-index].HisTurn = false;
        
        Players[index].HisTurn = true;
        Players[index].ResetTurn(GoldBonus, MaxManaBonus, MaxFoodBonus);

        GiveCardDragon(index);
        GiveCardSpell(index);

        if(turn >= 5)
        {
            GiveCardDragon(index);
            GiveCardSpell(index);
        }

        Board.ResetTurn(Players[index].ID);
        UpdateResources();

        UpdateTurnText(index);
    }
    // Update is called once per frame
    void Update()
    {
        //if (Players[0].EndedTurn)
        //{
        //    PassTurnToPlayer(1);
        //}
        //else if (Players[1].EndedTurn)
        //{
        //    PassTurnToPlayer(0);
        //}

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Board.DebugDestructibles();
        }
    }

    public void UpdateResources()
    {
        GamePlayer player = Players[PlayerInfoScene.Instance.playerId - 1];
        GameObject.Find("ManaIndicator").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = player.Mana.ToString();
        GameObject.Find("GoldIndicator").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = player.Gold.ToString();
    }

    public bool IsMe(int owner)
    {
        return PlayerInfoScene.Instance.playerId == owner;
    }

    public bool IsMyTurn()
    {
        return Players[PlayerInfoScene.Instance.playerId - 1].HisTurn;
    }

    public void UpdateAllStats()
    {
        foreach(var x in Board.GetAllDestructibles())
        {
            x.UpdateStatus();
        }
    }

    public IEnumerator EndGame()
    {
        yield return new WaitForSeconds(5);
        //TODO: leave room in PhotonView!!
        if (PlayerInfoScene.Instance.PhotonPresent > 0) 
        {
            PhotonNetwork.LeaveRoom();
        }
        
        Debug.Log("END GAMEEEE");
        yield return null;
    }

    public override void OnLeftRoom() 
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateTurnText(int index)
    {
        if (IsMe(1 + index))
        {
            GameObject.Find("TurnInfoText").GetComponent<Text>().text = $"Turn {turn}: It is your turn!";
            GameObject.Find("LastPlayedCard").transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            GameObject.Find("TurnInfoText").GetComponent<Text>().text = $"Turn {turn}: Your Opponent played";
            GameObject.Find("LastPlayedCard").transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void CallDeath(OnBoardDestructible toDie)
    {
        StartCoroutine(DieCoroutine(toDie));
    }

    IEnumerator DieCoroutine(OnBoardDestructible toDie)
    {
        //yield on a new YieldInstruction that waits for 3 seconds.
        yield return new WaitForSeconds(3);
        if (toDie.DestructibleType == OnBoardDestructible.DRAGON) GameObject.Destroy(toDie.PhysicInstance);
        else toDie.PhysicInstance.SetActive(false);
        //Destroy(this);

        if (IsMyTurn() && typeof(OnBoardNest).IsInstanceOfType(toDie))
        {
            CallRPCMethod("GameOver", 3 - toDie.Owner, false);
        }
        yield return null;
    }
}
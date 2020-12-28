using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance; 

    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] Text roomNameText;
    [SerializeField] Text chosenElementText;
    [SerializeField] Text currentRoomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject startGameButton;

    private int chosenElement;
    private int playerId;
    private int currentRoomIndex = 0;
    private List<RoomInfo> availableRooms = new List<RoomInfo>();

    private readonly string[] elementStrings = { "Fire", "Water", "Earth", "Air" };

    void Awake()
    {
        Instance = this;
    }

    public void SelectElement(int elementId)
    {
        SetElement(elementId);
        MenuManager.Instance.OpenMenu("StartMenu");
    }

    public void SetElement(int elementId)
    {
        chosenElement = elementId;
        chosenElementText.text = "Your current chosen element is:\n" + elementStrings[chosenElement];
    }

    public void CreateRoom(int chosenElement)
    {
        this.chosenElement = chosenElement;
        CreateRoom();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting to master");
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        PhotonNetwork.JoinLobby();

    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("StartMenu");
        Debug.Log("Joined lobby");
        SetElement(0);
        PhotonNetwork.NickName = "Player" + Random.Range(0, 2000).ToString("0000");
    }

    public void CreateRoom()
    {
        string roomName = "Room" + Random.Range(0, 2000).ToString("0000");
        PhotonNetwork.CreateRoom(roomName);
        MenuManager.Instance.OpenMenu("LoadingMenu");
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void SetPlayerId(bool isMasterClient)
    {
        playerId = isMasterClient ? 1 : 2;
    }

    public override void OnJoinedRoom()
    {
        
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        SetPlayerId(PhotonNetwork.IsMasterClient);

        if (players.Length < 2)
        {
            MenuManager.Instance.OpenMenu("RoomMenu");
        }
    }

    public void IncrementRoom()
    {
        currentRoomIndex += 1;
        UpdateRoomInfoText();
    }

    public void DecrementRoom()
    {
        currentRoomIndex -= 1;
        UpdateRoomInfoText();
    }

    public void UpdateRoomInfoText()
    {
        if(currentRoomIndex < 0)
        {
            currentRoomIndex = availableRooms.Count - 1;
        }
        if(availableRooms.Count == 0)
        {
            currentRoomNameText.text = "No rooms available right now!";
            return;
        }
        if (currentRoomIndex >= availableRooms.Count)
        {
            currentRoomIndex %= availableRooms.Count;
        }

        currentRoomNameText.text = availableRooms[currentRoomIndex].Name;
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        SetPlayerId(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + message;
        MenuManager.Instance.OpenMenu("error");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("LoadingMenu");

    }

    public void JoinRoom()
    {
        if(availableRooms.Count == 0)
        {
            return;
        }

        PhotonNetwork.JoinRoom(availableRooms[currentRoomIndex].Name);
        MenuManager.Instance.OpenMenu("LoadingMenu");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("LoadingMenu");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("StartMenu");
    }
     
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        availableRooms.Clear();
        for(int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            availableRooms.Add(roomList[i]);
        }
        UpdateRoomInfoText();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}

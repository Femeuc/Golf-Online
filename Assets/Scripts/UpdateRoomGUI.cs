using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class UpdateRoomGUI : MonoBehaviourPunCallbacks
{
    private Text playersInRoomList;
    // Start is called before the first frame update
    void Start()
    {
        playersInRoomList = GameObject.Find("AllRoomPlayers").GetComponent<Text>();

        adaptGUI();
        updatePlayersInRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void adaptGUI()
    {
        if (PhotonNetwork.IsMasterClient)
            useMasterClientUI();
        else
            useClientUI();
    }

    private void useMasterClientUI()
    {
        GameObject.Find("WaitingForHost").SetActive(false);
    }

    private void useClientUI()
    {
        Button btn = GameObject.Find("StartButton").GetComponent<Button>();
        Text text = btn.GetComponentInChildren<Text>();
        text.text = "Jogadores na sala";
        btn.enabled = false;
    }

    private void updatePlayersInRoom()
    {
        string allPlayersList = "";
        allPlayersList += PhotonNetwork.LocalPlayer.NickName;
        foreach (Player player in PhotonNetwork.PlayerListOthers)
        {
            allPlayersList += "\n" + player.NickName;
        }
        playersInRoomList.text = allPlayersList;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        updatePlayersInRoom();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        updatePlayersInRoom();
    }
}

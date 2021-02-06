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

    private void adaptGUI()
    {
        bool isMaster = PhotonNetwork.IsMasterClient;

        showWaitingForHostMessage(!isMaster);
        shouldButtonBeSetToStartGame(isMaster);

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        adaptGUI();
        updatePlayersInRoom();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        adaptGUI();
        updatePlayersInRoom();
    }

    private void showWaitingForHostMessage(bool show)
    {
        Text text = GameObject.Find("WaitingForHost").GetComponent<Text>();
        text.enabled = show;
    }

    private void shouldButtonBeSetToStartGame(bool should)
    {
        Button btn = GameObject.Find("StartButton").GetComponent<Button>();
        Text buttonText = btn.GetComponentInChildren<Text>();

        if (should)
        {
            buttonText.text = "Começar";

            if (PhotonNetwork.PlayerList.Length < 2) // if there is only one player
            {
                btn.enabled = false;
                ColorBlock cb = btn.colors;
                cb.normalColor = new Color32(255, 255, 255, 125);
                btn.colors = cb;
                buttonText.color = new Color32(0, 0, 0, 125);
            }
            else
            {
                btn.enabled = true;
                ColorBlock cb = btn.colors;
                cb.normalColor = new Color32(255, 255, 255, 255);
                btn.colors = cb;
                buttonText.color = new Color32(0, 0, 0, 255);
            }
        } else
        {
            buttonText.text = "Jogadores na sala";
            btn.enabled = false;
        }
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
}

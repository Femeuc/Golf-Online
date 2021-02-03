using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Com.Femeuc.GolfOnline
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;

        private void Start()
        {
            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);

                addaptGUI();
            }
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        private void addaptGUI()
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
        }
    }
}
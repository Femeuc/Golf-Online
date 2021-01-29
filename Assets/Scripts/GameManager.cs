using UnityEngine.SceneManagement;
using Photon.Pun;


namespace Com.Femeuc.GolfOnline
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}
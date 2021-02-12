using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class OnStartButtonClicked : MonoBehaviour
{
    private Button startButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(onStartButtonClicked);
    }

    private void onStartButtonClicked()
    {
        PhotonNetwork.LoadLevel("Room-1");
    }
}

using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviourPun
{
    private Rigidbody playerRb;
    private Camera cam;          // the ball must be thrown to the forward direction of the camera
    private Button throwBallButton;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        cam = Camera.main;

        throwBallButton = GameObject.Find("ThrowBallButton").GetComponent<Button>();
        throwBallButton.onClick.AddListener(ThrowBall);
    }

    void Update()
    {
        
    }

    private void ThrowBall()
    {
        if (!photonView.IsMine) return; 

        playerRb.AddForce(cam.transform.forward * 100, ForceMode.Force);
    }
}

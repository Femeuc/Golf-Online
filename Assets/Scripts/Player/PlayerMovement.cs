using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviourPun
{
    private Rigidbody playerRb;
    private Camera cam;          // the ball must be thrown to the forward direction of the camera
    private Button throwBallButton;
    private Scrollbar upForceScrollbar;
    private Scrollbar forwardForceScrollBar;

    void Start()
    {
        if (!photonView.IsMine)
        {
            this.enabled = false;
            return;
        }
        playerRb = GetComponent<Rigidbody>();
        cam = Camera.main;

        throwBallButton = GameObject.Find("ThrowBallButton").GetComponent<Button>();
        throwBallButton.onClick.AddListener(ThrowBall);

        upForceScrollbar = GameObject.Find("UpForceScrollbar").GetComponent<Scrollbar>();
        forwardForceScrollBar = GameObject.Find("ForwardForceScrollbar").GetComponent<Scrollbar>();
    }

    void Update()
    {
        
    }

    private void ThrowBall()
    {
        int upForce = (int) (upForceScrollbar.value * 10 - upForceScrollbar.value);
        int forwardForce = (int) (forwardForceScrollBar.value * 10 - forwardForceScrollBar.value);

        // Applies forwardForce and upForce to the Rigidbody
        // Quarternion.Euler is necessary so the ball is thorwn only forward, not upward, even if the camera is pointing upward.
        playerRb.AddForce(Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0) * Vector3.forward * forwardForce * 200  +  Vector3.up * upForce * 200, ForceMode.Force);
    }
}

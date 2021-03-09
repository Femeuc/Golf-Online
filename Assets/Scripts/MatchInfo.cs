using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchInfo : MonoBehaviour, IPunObservable
{

    int myNumberTest;
    int theirNumberTest = -1;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
            myNumberTest = 1;
        else
            myNumberTest = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("My number is : " + myNumberTest);
        Debug.Log("Their number is : " + theirNumberTest);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(myNumberTest);
        }
        else
        {
            // Network player, receive data
            this.theirNumberTest = (int)stream.ReceiveNext();
        }
    }
}

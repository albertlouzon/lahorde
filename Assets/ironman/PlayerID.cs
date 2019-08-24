using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
public class PlayerID : NetworkBehaviour {
    [SyncVar] public string playerUniqueName;
    private NetworkInstanceId PlayerNetId;
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        GetNetIdentity();
        SetIdentity();
        SetCameraTarget();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.name == "" || transform.name == "ironmanPrefab(Clone)")
        {
            SetIdentity();
        }
	}
    [Client]
    void GetNetIdentity()
    {
        PlayerNetId = GetComponent<NetworkIdentity>().netId;
        CmdTellServerMyIdentity( MakeUniqueIdentity() );

    }
    [Client]
    void SetIdentity()
    {
        if(!isLocalPlayer)
        {
            this.transform.name = playerUniqueName;

        }  else{
            transform.name = MakeUniqueIdentity();

        }
    }


    [Command]
    void CmdTellServerMyIdentity(string name)
    {
        playerUniqueName = name;

    }

    string MakeUniqueIdentity()
    {
        return "Player" + PlayerNetId.ToString();
    }

    void SetCameraTarget()
    {
        if(isLocalPlayer)
        {
          //  Camera.main.GetComponent<CameraFollow>().SetCameraOffset(transform.position);
            Camera.main.GetComponent<CameraFollow>().Target = this.gameObject;

        }
    }
}


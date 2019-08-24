using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerRespawn : NetworkBehaviour {
    bool respawning = false;
	// Use this for initialization
	void Start () {
        respawning = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<PlayerHealth>().currentHealth <= 0 && !respawning)
        {
            respawning = true;
            Invoke("RpcRespawn", 2.5f);
        }
	}
    [ClientRpc]
    void RpcRespawn()
    {
        // get the initial spawn position of the local player
        Transform spawn = NetworkManager.singleton.GetStartPosition();
        transform.position = spawn.position;

        // reset the health points 

        GetComponent<PlayerHealth>().currentHealth = GetComponent<PlayerHealth>().startingHealth;
        // we put the player in the idle animation 
        GetComponent<Animator>().Play("IdleWalk");
        GetComponent<PlayerHealth>().isDead = false;
        respawning = false;
           

    }
}

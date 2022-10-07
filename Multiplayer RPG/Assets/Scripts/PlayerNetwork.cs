using System.Collections;
using System.Collections.Generic;
//using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private void Update()
    {
        if(!IsOwner) return;

		Vector3 position = transform.position;

        Vector3 moveDir = new Vector3(0, 0, 0);

		if(Input.GetKey(KeyCode.W)) moveDir.z = +1f;
		if(Input.GetKey(KeyCode.S)) moveDir.z = -1f;
		if(Input.GetKey(KeyCode.A)) moveDir.x = -1f;
		if(Input.GetKey(KeyCode.D)) moveDir.x = +1f;

		float moveSpeed = 3f;
		position = moveDir * moveSpeed * Time.deltaTime;
		//transform.position += moveDir * moveSpeed * Time.deltaTime;
		ChangePositionServerRpc(position);
    }

	[ServerRpc]
    private void TestServerRpc(ServerRpcParams serverRpcParams)
	{
		Debug.Log("TestServerRpc " + OwnerClientId + " ; " + serverRpcParams.Receive.SenderClientId);
	}

	[ServerRpc]
    private void ChangePositionServerRpc(Vector3 position)
	{
		transform.position += position;
	}
}

using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
	[SerializeField] private Transform camLookAt;
	[SerializeField] private MeshRenderer baseMesh;

	//private NetworkVariable<Color> randomColor = new NetworkVariable<Color>(Color.white, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

	public override void OnNetworkSpawn()
	{
		Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		if(IsOwner)
		{
			ChangeColorServerRPC(randomColor);

			if(IsClient)
			{
				if(camLookAt == null) camLookAt = transform;
				PlayerCameraFollow.Instance.Attach(transform, camLookAt);
			}
		}

		ChangeColorClientRPC(randomColor);

		Debug.Log("Last: " + randomColor);
	}

    private void Update()
    {
        if(!IsOwner) return;
    }

	/*private void GetRandomColor()
	{
		if(randomColor == Color.white) return;
		randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
	}*/

	[ServerRpc]
	private void ChangeColorServerRPC(Color color)
	{
		Debug.Log("Server: " + color);
		baseMesh.material.color = color;
	}

	[ClientRpc]
	private void ChangeColorClientRPC(Color color)
	{
		Debug.Log("Client: " + color);
		baseMesh.material.color = color;
	}
}

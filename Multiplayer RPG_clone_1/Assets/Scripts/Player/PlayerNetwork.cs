using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
	[SerializeField] private Transform camLookAt;
	[SerializeField] private MeshRenderer baseMesh;

	private NetworkVariable<Color> baseColor = new NetworkVariable<Color>(Color.white);

	public override void OnNetworkSpawn()
	{
		if(IsOwner)
		{
			ChangeColor();
			
			if(IsClient)
			{
				if(camLookAt == null) camLookAt = transform;
				PlayerCameraFollow.Instance.Attach(transform, camLookAt);
			}
		}
	}

	private void Update()
	{
		baseMesh.material.color = baseColor.Value; //This should probably be a delegate
	}

	private void ChangeColor()
	{
		if(IsServer)
		{
			Color randomColor = GetRandomColor();
			baseMesh.material.color = randomColor;
			baseColor.Value = randomColor;
		} else
		{
			ChangeColorServerRPC();
		}
	}

	private Color GetRandomColor()
	{
		return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
	}

	[ServerRpc]
	private void ChangeColorServerRPC()
	{
		baseColor.Value = GetRandomColor();
	}
}

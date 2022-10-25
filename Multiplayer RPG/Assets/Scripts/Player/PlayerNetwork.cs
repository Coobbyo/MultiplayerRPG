using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
	public PlayerStats stats;

	[SerializeField] private Transform camLookAt;
	[SerializeField] private MeshRenderer baseMesh;

	private NetworkVariable<Color> baseColor = new NetworkVariable<Color>(Color.white);

	public Vector3 SpawnPoint = Vector3.zero;

	public override void OnNetworkSpawn()
	{
		if(IsOwner)
		{
			stats = GetComponent<PlayerStats>();
			stats.OnHealthReachedZero += RespawnServerRPC;

			//ChangeColor();

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

	public void ChangeColor()
	{
		Color randomColor = GetRandomColor();
		ChangeColor(randomColor);

	}

	public void ChangeColor(Color newColor)
	{
		if(IsServer)
		{
			baseMesh.material.color = newColor;
			baseColor.Value = newColor;
		} else
		{
			ChangeColorServerRPC(newColor);
		}
	}

	private Color GetRandomColor()
	{
		return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
	}

	[ServerRpc]
	private void ChangeColorServerRPC(Color color)
	{
		baseColor.Value = color;
	}

	[ServerRpc]
	private void RespawnServerRPC()
	{
		Debug.Log("Player Died");
		GetComponent<PlayerMovementNetwork>().Teleport(SpawnPoint);
		stats.HasDied = false;
	}
}

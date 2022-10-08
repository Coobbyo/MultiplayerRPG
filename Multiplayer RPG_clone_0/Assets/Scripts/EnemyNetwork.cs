using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemyNetwork : NetworkBehaviour
{
	[SerializeField] private EnemyMovement movement;

	void Update()
	{
		if(IsServer)
		{
			movement.Move();
		}
	}
}

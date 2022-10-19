using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EnemyMovement : NetworkBehaviour
{
	private Vector3 target, moveDir = Vector3.zero;
	private float moveTimer = 0.25f;

	private void Update()
	{
		if(!IsServer) return;
			Move();
	}

	public void Move()
	{
		Wander();
	}

	private void Wander()
	{
		if(Vector3.Distance(target, transform.localPosition) < 0.1f)
		{
			if(moveTimer >= 0f)
			{
				moveTimer -= Time.deltaTime;
			} else
			{
				Vector3 newPosition = GetNewPosition();
				target = newPosition;
				moveTimer += 2.5f + Random.Range(0.5f, 10.0f);
			}
		} else
		{
			float moveSpeed = 3f;
			moveDir = target - transform.localPosition;
			transform.localPosition += moveDir.normalized * moveSpeed * Time.deltaTime;
		}
	}

	private Vector3 GetNewPosition()
	{
		Vector3 newPosition = Vector3.zero;
		newPosition.x = Random.Range(-10, 10);
		newPosition.z = Random.Range(-10, 10);
		return newPosition;
	}
}

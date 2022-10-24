using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(CharacterCombat))]
public class EnemyMovement : NetworkBehaviour
{
	[SerializeField] private float moveSpeed = 3f;
	[SerializeField] private float followDistance = 5f;
	[SerializeField] private float stopDistance = 1f;

	private Transform target;
	private Vector3 targetPosition, moveDir = Vector3.zero;
	private float moveTimer = 0.25f;

	private CharacterCombat combatManager;

	public override void OnNetworkSpawn()
	{
		combatManager = GetComponent<CharacterCombat>();
	}

	private void Update()
	{
		if(!IsServer) return;
		
		if(target == null)
		{
			Wander();
		} else
		{
			Follow();
			if(Vector3.Distance(target.position, transform.position) > followDistance)
			{
				target = null;
				targetPosition = GetNewPosition();
			}
		}
	}

	private void FixedUpdate()
	{
		target = CheckForPlayers();
	}

	public void Move()
	{
		moveDir = targetPosition - transform.position;
		transform.localPosition += moveDir.normalized * moveSpeed * Time.deltaTime;
	}

	private void Follow()
	{
		float distance = Vector3.Distance(target.position, transform.position);

		if(distance <= followDistance && distance >= stopDistance)
		{
			Debug.Log("Following: " + target.position);
			targetPosition = target.position;
			Move();
			if(distance <= stopDistance)
			{
				//combatManager.Attack(Player.instance.playerStats);
				//FaceTarget();
			}
		}
	}

	private void Wander()
	{
		if(Vector3.Distance(targetPosition, transform.localPosition) < 0.1f)
		{
			if(moveTimer >= 0f)
			{
				moveTimer -= Time.deltaTime;
			} else
			{
				Vector3 newPosition = GetNewPosition();
				targetPosition = newPosition;
				moveTimer += 2.5f + Random.Range(0.5f, 10.0f);
			}
		} else
		{
			moveDir = targetPosition - transform.localPosition;
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

	private void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	private Transform CheckForPlayers()
	{
		List<PlayerNetwork> playerList = new List<PlayerNetwork>();
		Collider[] colliderArray = Physics.OverlapSphere(transform.position, followDistance);
		foreach(Collider collider in colliderArray)
		{
			if(collider.TryGetComponent(out PlayerNetwork player))
			{
				playerList.Add(player);
			}
		}

		PlayerNetwork closestPlayer = null;
		foreach(PlayerNetwork player in playerList)
		{
			if(closestPlayer == null)
			{
				closestPlayer = player;
			} else
			{
				if(Vector3.Distance(transform.position, player.transform.position) <
					Vector3.Distance(transform.position, player.transform.position))
				{
					closestPlayer = player;
				}
			}
		}

		if(closestPlayer == null)
		{
			return null;
		} else
		{
			return closestPlayer.transform;
		}
	}
}

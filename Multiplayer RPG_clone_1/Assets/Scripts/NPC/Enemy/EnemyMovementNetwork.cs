using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(CharacterCombatNetwork))]
public class EnemyMovementNetwork : NetworkBehaviour
{
	public Vector3 debugPosition;

	[SerializeField] private float moveSpeed = 3f;
	[SerializeField] private float followDistance = 10f;
	[SerializeField] private float stopDistance = 0.9f;
	[SerializeField] private float roamingRadius = 10f;

	private Transform target;
	private Vector3 targetPosition, moveDir = Vector3.zero;
	private float moveTimer = 0.25f;

	private CharacterCombatNetwork combatManager;

	public override void OnNetworkSpawn()
	{
		combatManager = GetComponent<CharacterCombatNetwork>();
		targetPosition = transform.position + GetNewPosition();
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
				DropTarget();
		}
	}

	private void FixedUpdate()
	{
		target = CheckForPlayers();
	}

	private void Move()
	{
		//if(transform.localPosition.magnitude >= roamingRadius)
			//DropTarget();

		moveDir = targetPosition - transform.position;
		transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
	}

	private void Stop()
	{
		moveDir = Vector3.zero;
	}

	private void Follow()
	{
		float distance = Vector3.Distance(target.position, transform.position);

		if(distance <= followDistance)
		{
			targetPosition = target.position;
			Move();
			if(distance <= stopDistance)
			{
				Debug.Log("Try to Attack");
				if(target.TryGetComponent(out PlayerNetwork player))
				{
					Debug.Log("Exterminate");
					combatManager.Attack(player.stats);
					FaceTarget();
				}
			}
		}
	}

	private void Wander()
	{
		if(Vector3.Distance(targetPosition, transform.position) < 0.1f)
		{
			Stop();
			if(moveTimer >= 0f)
			{
				moveTimer -= Time.deltaTime;
			} else
			{
				Vector3 newPosition = transform.position + GetNewPosition();
				targetPosition = newPosition;
				moveTimer += 2.5f + Random.Range(0.5f, 10.0f);
			}
		} else
		{
			Move();
		}
	}

	private Vector3 GetNewPosition()
	{
		Vector3 newPosition = new Vector3(Random.Range(-1.0f, 1.0f), 0f, Random.Range(-1.0f, 1.0f));
		newPosition = newPosition * roamingRadius;
		return newPosition;
	}

	private void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	private void DropTarget()
	{
		target = null;
		targetPosition = transform.parent.position + GetNewPosition();
		//Debug.Log("Spawner: " + transform.parent.position);
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

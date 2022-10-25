using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(CharacterStatsNetwork))]
public class EnemyNetwork : NetworkBehaviour, IInteractable
{
	private CharacterStatsNetwork stats;

	public event System.Action OnDespawn;
	
	public override void OnNetworkSpawn()
	{
		if(IsOwner)
		{
			stats = GetComponent<CharacterStatsNetwork>();
			stats.OnHealthReachedZero += DespawnServerRPC;
		}
	}

	public void Interact(Transform interactTransform)
	{
		if(interactTransform.TryGetComponent(out CharacterCombatNetwork combatManager))
		{
			combatManager.Attack(stats);
		}
	}

    public string GetInteractText()
	{
		return "Fight Slime";
	}

    public Transform GetTransform()
	{
		return transform;
	}

	[ServerRpc]
	private void DespawnServerRPC()
	{
		Debug.Log("Enemy Died");
		OnDespawn?.Invoke();
		Destroy(gameObject);
	}
}

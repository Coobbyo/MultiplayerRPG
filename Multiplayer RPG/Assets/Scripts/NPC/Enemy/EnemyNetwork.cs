using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(CharacterStatsNetwork))]
public class EnemyNetwork : NetworkBehaviour, IInteractable
{
	CharacterStatsNetwork stats;
	
	private void Start()
	{
		stats = GetComponent<CharacterStatsNetwork>();
		stats.OnHealthReachedZero += Die;
	}

	private void Die()
	{
		Destroy(gameObject, 5f);
	}

	public void Interact(Transform interactorTransform)
	{
		Debug.Log("Fight");
	}

    public string GetInteractText()
	{
		return "Fight Slime";
	}

    public Transform GetTransform()
	{
		return transform;
	}
}

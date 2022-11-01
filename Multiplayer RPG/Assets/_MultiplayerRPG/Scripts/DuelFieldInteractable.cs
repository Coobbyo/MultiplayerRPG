using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelFieldInteractable : MonoBehaviour, IInteractable
{
	[SerializeField] private Transform defender;
	[SerializeField] private Transform challenger;
	[SerializeField] private Transform defenderMonster;
	[SerializeField] private Transform challengerMonster;

	private bool hasDefender;

	public void Interact(Transform interactTransform)
	{
		if(!hasDefender)
		{
			interactTransform.position = defender.position;
			hasDefender = true;
		} else
		{
			interactTransform.position = challenger.position;
		}
	}

	public string GetInteractText()
	{
		return "Time to duel!";
	}

	public Transform GetTransform()
	{
		return transform;
	}
}

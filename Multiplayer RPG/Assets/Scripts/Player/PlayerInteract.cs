using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
	[SerializeField] private float interactRange = 2f;

	private PlayerInteractUI interactUI;

	private void Awake()
	{
		interactUI = FindObjectOfType<PlayerInteractUI>();
		interactUI.AttachPlayer(this);
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.E)) //TODO: convert this to Input System
		{
			IInteractable interactable = GetInteractableObject();
			if(interactable != null)
			{
				interactable.Interact(transform);
			}
		}
	}

	public IInteractable GetInteractableObject()
	{
		List<IInteractable> interactableList = new List<IInteractable>();
		Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
		foreach(Collider collider in colliderArray)
		{
			if(collider.TryGetComponent(out IInteractable interactable))
			{
				interactableList.Add(interactable);
			}
		}

		IInteractable closestInteractable = null;
		foreach(IInteractable interactable in interactableList)
		{
			if(closestInteractable == null)
			{
				closestInteractable = interactable;
			} else
			{
				if(Vector3.Distance(transform.position, interactable.GetTransform().position) <
					Vector3.Distance(transform.position, closestInteractable.GetTransform().position))
				{
					closestInteractable = interactable;
				}
			}
		}

		return closestInteractable;
	}
	
}

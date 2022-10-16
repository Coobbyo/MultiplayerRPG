using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
	[SerializeField] private float interactRange = 2f;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.E))//TODO: convert this to Input System
		{
			Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
			foreach(Collider collider in colliderArray)
			{
				if(collider.TryGetComponent(out NPCInteractable nPCInteractable))
				{
					nPCInteractable.Interact();
				}
			}
		}
	}
	
}

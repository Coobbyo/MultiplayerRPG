using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform playerAnchor;

    public void Interact(Transform interactTransform)
    {
        if(interactTransform.TryGetComponent(out PlayerNetwork player))
		{
            player.SpawnPoint = playerAnchor.position;
        }
    }

    public string GetInteractText()
    {
        return "Set Spawn";
    }

    public Transform GetTransform()
    {
        return transform;
    }
}

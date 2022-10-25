using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ColorDisplay : NetworkBehaviour, IInteractable
{
    [SerializeField] Color displayColor = Color.white;
    [SerializeField] MeshRenderer displayMesh;

    public override void OnNetworkSpawn()
    {
        if(!IsServer) return;

        displayMesh.material.color = displayColor;
    }

    public void Interact(Transform interactTransform)
    {
        interactTransform.GetComponent<PlayerNetwork>().ChangeColor(displayColor);
    }

    public string GetInteractText()
    {
        return "Select Color";
    }

    public Transform GetTransform()
    {
        return transform;
    }
}

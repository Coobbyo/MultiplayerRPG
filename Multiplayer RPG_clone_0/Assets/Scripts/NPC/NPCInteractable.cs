using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;

    public void Interact(Transform interactTransform)
    {
        ChatBubble.Create(transform.transform, new Vector3(-1.25f, 1.7f, 0f), ChatBubble.IconType.Happy, "Hello there!");
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}

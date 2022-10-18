using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    public void Interact()
    {
        ChatBubble.Create(transform.transform, new Vector3(-1.25f, 1.7f, 0f), ChatBubble.IconType.Happy, "Hello there!");
    }
}

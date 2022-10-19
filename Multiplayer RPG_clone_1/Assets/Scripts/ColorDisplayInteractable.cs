using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDisplayInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] Color displayColor = Color.white;

    public void Interact(Transform interactTransform)
    {
        Debug.Log("Display interacting");
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

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVitualCamera;
    
    private void Awake()
    {
        cinemachineVitualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void Attach(Transform transform)
    {
        cinemachineVitualCamera.Follow = transform;
    }
}

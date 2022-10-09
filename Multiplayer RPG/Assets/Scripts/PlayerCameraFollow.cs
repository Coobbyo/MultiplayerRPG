using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    static PlayerCameraFollow mInstance;
 
    public static PlayerCameraFollow Instance
    {
        get
        {
            if(mInstance == null)
            {
                GameObject go = new GameObject();
                mInstance = go.AddComponent<PlayerCameraFollow>();
            }
            return mInstance;
        }
    }

    private CinemachineVirtualCamera cinemachineVitualCamera;

    private void Awake()
    {
        mInstance = this;
        cinemachineVitualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void Attach(Transform transform)
    {
        cinemachineVitualCamera.Follow = transform;
    }
}

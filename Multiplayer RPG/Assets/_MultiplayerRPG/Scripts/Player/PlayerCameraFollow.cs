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

    private CinemachineFreeLook cinemachineFreeLook;

    private void Awake()
    {
        mInstance = this;
        cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
    }

    public void Attach(Transform followTransform, Transform LookAtTransform)
    {
        cinemachineFreeLook.Follow = followTransform;
        cinemachineFreeLook.LookAt = LookAtTransform;
    }
}

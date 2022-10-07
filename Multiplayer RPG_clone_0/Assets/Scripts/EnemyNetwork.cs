using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemyNetwork : NetworkBehaviour
{
    [SerializeField] private Transform target;

    private float moveTimer = 1f;

    void Update()
    {
        if(!IsOwner) return;
        if(moveTimer > 0f)
        {
            moveTimer -= Time.deltaTime;
            return;
        }
        
		Vector3 position = transform.position;
        Vector3 newPosition = new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));

        if(Vector3.Distance(target.position, transform.position) < 0.1f) return;

        Vector3 moveDir = target.position = newPosition;
		float moveSpeed = 3f;
		position = moveDir * moveSpeed * Time.deltaTime;
		transform.position += moveDir * moveSpeed * Time.deltaTime;

        moveTimer = 2.5f + Random.Range(0.5f, 10.0f);
    }
}

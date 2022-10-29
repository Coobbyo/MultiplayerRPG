using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementNetwork : NetworkBehaviour
{
	[SerializeField] private float moveSpeed = 1f;
	//[SerializeField] private float jumpSpeed = 0.5f;
    [SerializeField] private float gravity = 9.8f;

	private float turnsmoothTime = 0.1f;
	private float turnSmoothVelocity;

	private PlayerInput playerInput;
	private Transform cam;
	private CharacterController characterController;

	public override void OnNetworkSpawn()
	{
		playerInput = GetComponent<PlayerInput>();
		cam = Camera.main.transform;
		characterController = GetComponent<CharacterController>();
	}

	private void Update()
	{
		if(!IsOwner) return;

		Move();
		
	}

	private void Move()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

		if(direction.magnitude < 0.1f) return; //Don't bother moving or rotating if we aren't

		//Rotate Player
		float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
		float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnsmoothTime);
		RotateServerRPC(Quaternion.Euler(0f, targetAngle, 0f));

		//Set flat direction and speed of movement
		Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
		Vector3 movement = moveDir * moveSpeed * Time.deltaTime;

		//Factor in gravity
		if(characterController.isGrounded)
		{
            movement.y = 0f;
		} else
		{
            movement.y -= gravity * Time.deltaTime;
		}
		
		MoveServerRpc(movement);
	}

	public void Teleport(Vector3 newPosition)
	{
		TeleportServerRPC(newPosition);
	}

	[ServerRpc]
    private void MoveServerRpc(Vector3 movement)
	{
		characterController.Move(movement);
	}

	[ServerRpc]
	private void RotateServerRPC(Quaternion rotation)
	{
		transform.rotation = rotation;
	}

	[ServerRpc]
	private void ScaleServerRPC(float scalar)
	{
		transform.localScale *= scalar;
	}

	[ServerRpc]
	private void TeleportServerRPC(Vector3 newPosition)
	{
		transform.position = newPosition;
	}
}

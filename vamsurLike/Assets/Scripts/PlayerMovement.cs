using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 5.0f;

    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private PlayerInput playerInput;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();

        playerAnimator.SetFloat("Move", playerInput.upMove);
    }

    private void Move()
    {
        Vector3 inputDistance = new Vector3(playerInput.rightMove, 0f, playerInput.upMove);
        Vector3 moveDistance = inputDistance.normalized * moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }

    private void Rotate()
    {
        Vector3 rotatePosition = new Vector3(playerInput.mousePosition.x, 0f, playerInput.mousePosition.z) - playerRigidbody.position;
        playerRigidbody.rotation = Quaternion.Slerp(playerRigidbody.rotation, Quaternion.LookRotation(rotatePosition), rotateSpeed * Time.deltaTime);
    }
}

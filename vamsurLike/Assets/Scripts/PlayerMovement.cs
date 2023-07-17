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
        Vector3 moveDistance = Vector3.Magnitude(new Vector3(playerInput.rightMove, 0f, playerInput.upMove)) * moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }

    private void Rotate()
    {
        print(playerInput.mousePosition);
    }
}

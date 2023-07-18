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

        if (playerInput.upMove == 0 && playerInput.rightMove != 0)
        {
            playerAnimator.SetFloat("Move", Mathf.Abs(playerInput.rightMove));
        }
        else
        {
            playerAnimator.SetFloat("Move", playerInput.upMove);
        }
    }

    private void Move()
    {
        Vector3 inputDistance = Vector3.ClampMagnitude(new Vector3(playerInput.rightMove, 0f, playerInput.upMove), 1f);
        Vector3 moveDistance = inputDistance * moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }

    private void Rotate()
    {
        Vector3 rotatePosition = new Vector3(playerInput.mousePosition.x, 0f, playerInput.mousePosition.z) - playerRigidbody.position;
        playerRigidbody.rotation = Quaternion.Slerp(playerRigidbody.rotation, Quaternion.LookRotation(rotatePosition), rotateSpeed * Time.deltaTime);
    }
}

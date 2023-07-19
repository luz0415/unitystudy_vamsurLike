using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;
    public Transform gunPivot;
    public Transform leftHandMount;
    public Transform rightHandMount;

    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        gun.enabled = true;
    }
    private void OnDisable()
    {
        gun.enabled = false;
    }

    private void Update()
    {
        if(GameManager.instance != null && !GameManager.instance.isGameover)
        {
            gun.Fire();
        }   
    }
    private void OnAnimatorIK(int layerIndex)
    {
        // gunPivot 포지션을 오른쪽 팔꿈치에 고정
        gunPivot.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);

        // 오른쪽 손 IK 설정
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);

        // 왼쪽 손 IK 설정
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);
    }
}

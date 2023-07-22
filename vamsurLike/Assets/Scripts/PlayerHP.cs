using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : LivingEntity
{
    private PlayerMovement playerMovement;
    private PlayerShooter playerShooter;

    private Animator playerAnimator;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();

        playerAnimator = GetComponent<Animator>();
    }
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    protected override void Dead()
    {
        base.Dead();

        playerAnimator.SetTrigger("Die");

        playerMovement.enabled = false;
        playerShooter.enabled = false;
    }
}

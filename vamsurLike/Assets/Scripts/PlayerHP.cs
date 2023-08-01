using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : LivingEntity
{
    private PlayerMovement playerMovement;
    private PlayerShooter playerShooter;

    private Animator playerAnimator;

    private Slider playerHPSlider;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();

        playerAnimator = GetComponent<Animator>();
        playerHPSlider = GetComponentInChildren<Slider>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        playerHPSlider.value = startingHP;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
        playerHPSlider.value -= damage;
    }

    public override void RestoreHP(float restoreHP)
    {
        base.RestoreHP(restoreHP);
        playerHPSlider.value = HP;
    }

    public void IncreaseStartHP(float increaseHP)
    {
        startingHP += increaseHP;
        HP += increaseHP;
        playerHPSlider.maxValue = startingHP;
        playerHPSlider.value = HP;
    }

    public override void Dead()
    {
        base.Dead();

        playerAnimator.SetTrigger("Die");

        playerMovement.enabled = false;
        playerShooter.enabled = false;
    }
}

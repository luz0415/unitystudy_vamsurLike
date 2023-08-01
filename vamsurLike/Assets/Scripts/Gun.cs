using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform fireTransform;

    public ParticleSystem muzzleFlashEffect;
    public ParticleSystem shellEjectEffect;

    public float damage = 20f;
    public float bulletDistance = 50f;

    public float timeBetFire = 0.5f;
    private float lastFireTime;

    public float thirdBulletDamage;
    private int fireCount;

    public float lifeStealRatio;
    public PlayerHP playerHP;

    public float bulletLineWidth { get; private set; } = 0.02f;
    private LineRenderer bulletLineRenderer;

    private void Start()
    {
        lastFireTime = 0f;
        fireCount = 0;

        thirdBulletDamage = 1f;
        lifeStealRatio = 0f;

        bulletLineRenderer = GetComponent<LineRenderer>();
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.startWidth = bulletLineWidth;
        bulletLineRenderer.enabled = false;
    }

    public void Fire()
    {
        if(lastFireTime + timeBetFire <= Time.time)
        {
            lastFireTime = Time.time;
            Shot();
        }
    }

    private void Shot()
    {
        float shotDamage = damage;
        fireCount++;
        if (fireCount == 3) // 크라켄 적용
        {
            if(thirdBulletDamage > 1f) // 능력 적용이 되었다면
            {
                shotDamage *= thirdBulletDamage;
                bulletLineRenderer.material.SetColor("_EmissionColor", Color.red);
            }
            fireCount = 0;
        }
        else // 크라켄 미적용
        {
            bulletLineRenderer.material.SetColor("_EmissionColor", new Color(255, 232, 0));
        }

        RaycastHit hit;
        Vector3 hitPosition;

        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, bulletDistance)){
            IDamageable hitObject = hit.collider.GetComponent<IDamageable>();
            if(hitObject != null)
            {
                hitObject.OnDamage(shotDamage, hit.point, hit.normal);
                playerHP.RestoreHP(shotDamage * lifeStealRatio);
            }

            hitPosition = hit.point;
        }
        else
        {
            hitPosition = fireTransform.position + fireTransform.forward * bulletDistance;
        }

        StartCoroutine(ShotEffect(hitPosition));

    }

    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        muzzleFlashEffect.Play();
        shellEjectEffect.Play();

        bulletLineRenderer.SetPosition(0, fireTransform.position);
        bulletLineRenderer.SetPosition(1, hitPosition);
        bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);

        bulletLineRenderer.enabled = false;
    }

    public void SetBulletLineWidth(float newBulletLineWidth)
    {
        bulletLineWidth = newBulletLineWidth;
        bulletLineRenderer.startWidth = bulletLineWidth;
    }
}

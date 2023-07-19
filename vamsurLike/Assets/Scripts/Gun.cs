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

    public float bulletLineWidth { get; private set; } = 0.02f;
    private LineRenderer bulletLineRenderer;

    private void Start()
    {
        lastFireTime = 0f;

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
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;

        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, bulletDistance)){
            // 충돌한 물체 인식
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

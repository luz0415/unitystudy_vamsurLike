using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ExpCoin : MonoBehaviour, IItem
{
    public float rotateSpeed = 5f;
    public float getExp = 1f;

    public event Action onUse;
    public void Use(GameObject target)
    {
        PlayerExp playerExp = target.GetComponent<PlayerExp>();
        if (playerExp != null)
        {
            playerExp.AddExp(getExp);
        }

        if(onUse != null) onUse();

        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0f, rotateSpeed, 0f));
    }
}

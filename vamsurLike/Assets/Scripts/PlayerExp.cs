using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour
{
    private PlayerHP playerHP;

    public Slider expSlider;
    public float levelUpMultiple = 0.3f;
    public float levelExp = 30f;
    private float exp;

    private void Awake()
    {
        playerHP = GetComponent<PlayerHP>();
        exp = 0f;

        expSlider.maxValue = levelExp;
        expSlider.value = exp;
    }

    public void AddExp(float newExp)
    {
        exp += newExp;

        if(exp >= levelExp) 
        {
            GameManager.instance.LevelUp();
            exp -= levelExp;
            levelExp *= levelUpMultiple;
        }

        expSlider.maxValue = levelExp;
        expSlider.value = exp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!playerHP.dead)
        {
            IItem item = other.GetComponent<IItem>();
            if(item != null)
            {
                item.Use(gameObject);
            }
        }
    }
}

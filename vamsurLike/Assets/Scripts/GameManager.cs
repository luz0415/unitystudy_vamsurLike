using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }

    private static GameManager m_instance;

    public GameObject player;

    public GameObject AbilityChooseUI;
    public Button[] AbilityButtons;
    
    private enum Ability
    {
        MoveSpeed,
        MaxHP,
        LifeSteal,
        Damage,
        FireTime,
        ThirdBullet,
        Portion,
        LastNumber
    }

    private string[] AbilityTexts =
    {
        "이동속도 증가",
        "최대체력 증가",
        "체력흡혈 증가",
        "데미지 증가",
        "공격속도 증가",
        "세번째 공격마다 공격력 증가",
        "체력 모두 회복",
    };

    private int[] AbilityLevels = { 0, 0, 0, 0, 0, 0 };
    private float[] AbilityIncreaseRatio = { 1.2f, 1.1f, 1.01f, 1.1f, 1.1f, 1.5f };

    //텍스트 멘트, 능력 올리는 함수들
    public bool isGameover { get; private set; }

    private void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
        }

        AbilityChooseUI.SetActive(false);
    }

    public void LevelUp()
    {
        Time.timeScale = 0f;

        AbilityChooseUI.SetActive(true);
        int[] threeAbilities = NotOverlapRandomNumber(0, (int)Ability.LastNumber, 3);

        foreach (int i in threeAbilities)
        {
            AbilityButtons[i].GetComponentInChildren<TextMeshPro>().text = AbilityTexts[i];
            switch(i)
            {
                case (int) Ability.MoveSpeed: AbilityMoveSpeed(); break;
                case (int) Ability.MaxHP: break;
                case (int) Ability.LifeSteal: break;
                case (int) Ability.Damage: break;
                case (int) Ability.FireTime: break;
                case (int) Ability.ThirdBullet: break;
                case (int) Ability.Portion: break;
            }
        }
    }

    private int[] NotOverlapRandomNumber(int start, int end, int num)
    {
        List<int> numbers = new List<int>();
        for (int i = start; i < end; i++)
        {
            numbers.Add(i);
        }

        int[] randomNumbers = new int[num];
        for (int i = 0; i < randomNumbers.Length; i++)
        {
            int index = Random.Range(0, numbers.Count);
            randomNumbers[i] = numbers[index];
            numbers.RemoveAt(index);
        }

        return randomNumbers;
    }

    private void AbilityMoveSpeed()
    {

    }
}

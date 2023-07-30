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
        "�̵��ӵ� ����",
        "�ִ�ü�� ����",
        "ü������ ����",
        "������ ����",
        "���ݼӵ� ����",
        "����° ���ݸ��� ���ݷ� ����",
        "ü�� ��� ȸ��",
    };

    private int[] AbilityLevels = { 0, 0, 0, 0, 0, 0 };
    private float[] AbilityIncreaseRatio = { 1.2f, 1.1f, 1.01f, 1.1f, 1.1f, 1.5f };

    //�ؽ�Ʈ ��Ʈ, �ɷ� �ø��� �Լ���
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

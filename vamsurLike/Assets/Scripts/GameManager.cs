using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
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

    private PlayerMovement playerMovement;
    private PlayerHP playerHP;
    private Gun gun;

    public TextMeshProUGUI timeText;
    private int timeMinutes;
    private int timeSeconds;

    private enum Ability
    {
        MoveSpeed,
        MaxHP,
        LifeSteal,
        Damage,
        AttackSpeed,
        ThirdBullet,
        Portion,
        LastNumber
    }

    private string[] AbilityTexts =
    {
        "MOVE SPPED",
        "MAX HP",
        "LIFE STEAL",
        "DAMAGE",
        "ATTACK SPEED",
        "THIRD BULLET",
        "PORTION",
    };

    private int[] AbilityLevels = { 0, 0, 0, 0, 0, 0, 0 };
    private float[] AbilityIncreaseRatio = { 1.1f, 0.1f, 0.01f, 1.1f, 0.9f, 1.5f };

    //텍스트 멘트, 능력 올리는 함수들
    public bool isGameover { get; private set; }
    public GameObject gameoverUI;
    public TextMeshProUGUI bestTimeText;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }

        AbilityChooseUI.SetActive(false);

        playerMovement = player.GetComponent<PlayerMovement>();
        playerHP = player.GetComponent<PlayerHP>();
        gun = player.GetComponentInChildren<Gun>();

        timeMinutes = 0;
        timeSeconds = 0;
    }
    private void Update()
    {
        if (isGameover)
        {
            RestartGame();
            return;
        }

        timeMinutes = (int)Time.time / 60;
        timeSeconds = (int)Time.time % 60;

        timeText.text = TimeToString(timeMinutes, timeSeconds);
    }

    public void EndGame()
    {
        int bestTime = PlayerPrefs.GetInt("Time");
        if(bestTime < timeMinutes * 60 + timeMinutes)
        {
            bestTime = timeMinutes * 60 + timeMinutes;
            PlayerPrefs.SetInt("Time", timeMinutes * 60 + timeSeconds);
        }
        bestTimeText.text = "Best Time\n"+TimeToString(bestTime / 60, bestTime % 60);

        isGameover = true;
        timeText.gameObject.SetActive(false);
        AbilityChooseUI.SetActive(false);
        gameoverUI.SetActive(true);
    }

    private void RestartGame()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private string TimeToString(int minutes, int seconds)
    {
        string stringMinutes;
        string stringSeconds;
        if (timeMinutes < 10)
        {
            stringMinutes = "0" + timeMinutes.ToString();
        }
        else
        {
            stringMinutes = timeMinutes.ToString();
        }
        if (timeSeconds < 10)
        {
            stringSeconds = "0" + timeSeconds.ToString();
        }
        else
        {
            stringSeconds = timeSeconds.ToString();
        }

        return stringMinutes + ":" + stringSeconds;
    }

    public void LevelUp()
    {
        Time.timeScale = 0f;

        AbilityChooseUI.SetActive(true);
        int[] threeAbilities = NotOverlapRandomNumber(0, (int)Ability.LastNumber, 3);

        for (int i = 0; i < 3; i++)
        {
            AbilityButtons[i].onClick.RemoveAllListeners();

            int thisTimeAbility = threeAbilities[i];

            if(thisTimeAbility != (int)Ability.Portion)
            {
                AbilityButtons[i].GetComponentInChildren<TextMeshProUGUI>().text
                    = AbilityTexts[thisTimeAbility] + "\nLEVEL " + AbilityLevels[thisTimeAbility];
            }
            else
            {
                AbilityButtons[i].GetComponentInChildren<TextMeshProUGUI>().text
                    = AbilityTexts[thisTimeAbility];
            }

            switch (thisTimeAbility)
            {
                case (int)Ability.MoveSpeed: AbilityButtons[i].onClick.AddListener(AbilityMoveSpeed); break;
                case (int)Ability.MaxHP: AbilityButtons[i].onClick.AddListener(AbilityMaxHP); break;
                case (int)Ability.LifeSteal: AbilityButtons[i].onClick.AddListener(AbilityLifeSteal); break;
                case (int)Ability.Damage: AbilityButtons[i].onClick.AddListener(AbilityDamage); break;
                case (int)Ability.AttackSpeed: AbilityButtons[i].onClick.AddListener(AbilityAttackSpeed); break;
                case (int)Ability.ThirdBullet: AbilityButtons[i].onClick.AddListener(AbilityThirdBullet); break;
                case (int)Ability.Portion: AbilityButtons[i].onClick.AddListener(AbilityPortion); break;
            }

            AbilityLevels[thisTimeAbility]++;
            AbilityButtons[i].onClick.AddListener(OnClickAbilityButton);
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
        playerMovement.moveSpeed *= AbilityIncreaseRatio[(int)Ability.MoveSpeed];
    }
    private void AbilityMaxHP()
    {
        playerHP.IncreaseStartHP(playerHP.startingHP * AbilityIncreaseRatio[(int)Ability.MaxHP]);
    }
    private void AbilityLifeSteal()
    {
        gun.lifeStealRatio += AbilityIncreaseRatio[(int)Ability.LifeSteal];
    }
    private void AbilityDamage()
    {
        gun.damage *= AbilityIncreaseRatio[(int)Ability.Damage];
    }
    private void AbilityAttackSpeed()
    {
        gun.timeBetFire *= AbilityIncreaseRatio[(int)Ability.AttackSpeed];
    }
    private void AbilityThirdBullet()
    {
        gun.thirdBulletDamage *= AbilityIncreaseRatio[(int)Ability.ThirdBullet];
    }
    private void AbilityPortion()
    {
        playerHP.RestoreHP(playerHP.startingHP);
    }

    private void OnClickAbilityButton()
    {
        AbilityChooseUI.SetActive(false);
        Time.timeScale = 1.0f;
    }
}

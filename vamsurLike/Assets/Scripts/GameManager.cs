using UnityEngine;

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

    private void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public bool isGameover { get; private set; }
}

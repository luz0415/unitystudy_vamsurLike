using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string upAxisName = "Vertical"; // 상하 축
    public string rightAxisName = "Horizontal"; // 좌우 축
    
    public float upMove { get; private set; }
    public float rightMove { get; private set; }
    public Vector3 mousePosition { get; private set; }

    private void Start()
    {
        upMove = 0f;
        rightMove = 0f;
        mousePosition = Vector3.zero;
    }

    private void Update()
    {
        if(GameManager.instance != null && GameManager.instance.isGameover)
        {
            upMove = 0f;
            rightMove = 0f;
            mousePosition = Vector3.zero;
            return;
        }

        upMove = Input.GetAxis(upAxisName);
        rightMove = Input.GetAxis(rightAxisName);
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}

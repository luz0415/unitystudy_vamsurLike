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
        mousePosition = transform.rotation.eulerAngles;
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

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 화면 좌표에서 출발해 카메라를 통해 월드좌표로 발사할 Ray
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) // Ray가 발사해 맞았다면
        {
            mousePosition = hit.point; // mousePosition에 맞은 월드좌표 대입
        }
    }
}

using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string upAxisName = "Vertical"; // ���� ��
    public string rightAxisName = "Horizontal"; // �¿� ��
    
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

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ȭ�� ��ǥ���� ����� ī�޶� ���� ������ǥ�� �߻��� Ray
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) // Ray�� �߻��� �¾Ҵٸ�
        {
            mousePosition = hit.point; // mousePosition�� ���� ������ǥ ����
        }
    }
}

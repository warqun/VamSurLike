using UnityEngine;

public class PlayerCharter : MonoBehaviour
{
    public Camera mainCamera; // �� ī�޶�
    public float rotationSpeed = 5f; // ȸ�� �ӵ�

    void Update()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPos = hit.point;
            targetPos.y = transform.position.y; // ĳ���Ͱ� �������� �ʵ��� Y�� ����

            Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
            transform.LookAt(targetPos);
        }
    }
}


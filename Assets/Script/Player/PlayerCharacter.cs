using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public Camera mainCamera; // 주 카메라
    public float rotationSpeed = 5f; // 회전 속도

    void Update()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPos = hit.point;
            targetPos.y = transform.position.y; // 캐릭터가 기울어지지 않도록 Y축 고정
            Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
            transform.LookAt(targetPos);
        }
    }
}


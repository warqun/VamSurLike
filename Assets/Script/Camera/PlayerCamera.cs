using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    Player player;
    public void Start()
    {
        /// ���� ���̽� ����� ��ŸƮ���� �����Ѵ�.
        player = GameBase.gameBase.player;
    }
    private void FixedUpdate()
    {

    }
}

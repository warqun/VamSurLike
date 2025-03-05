using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    Player player;
    public void Start()
    {
        /// 게임 베이스 사용은 스타트에서 시작한다.
        player = GameBase.gameBase.player;
    }
    private void FixedUpdate()
    {

    }
}

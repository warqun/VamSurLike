using UnityEngine;

public class PlayerController : MonoBehaviour
{
    AliveObject state = null;
    float playerMoveVerctorX = 0;
    float playerMoveVerctorY = 0;
    [Header("#SPEED PAR")]   
    float speed = 0f;

    Rigidbody rb = null;
    private void Start()
    {
        state = null;
        state = GetComponent<AliveObject>();
        if(state != null)
            speed = state.GetStatusValue(ObjectDataType.AliveObjectStatus.Speed);

        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        /// Player Move Controller
        {
            playerMoveVerctorX = Input.GetAxisRaw("Horizontal");
            playerMoveVerctorY = Input.GetAxisRaw("Vertical");
        }
        
    }

    private void FixedUpdate()
    {
        // MoveController
        float speedBuff = state.GetBuffValue(ObjectDataType.AliveObjectStatus.Speed);
        Vector3 moveVec = new Vector3(playerMoveVerctorX, 0, playerMoveVerctorY);
        transform.Translate(moveVec.normalized * (speed * speedBuff) * Time.fixedDeltaTime);
    }
}

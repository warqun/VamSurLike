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
            speed = state.SpeedPoint;

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
        Vector3 moveVec = new Vector3(playerMoveVerctorX, 0, playerMoveVerctorY);
        transform.Translate(moveVec.normalized * speed * Time.fixedDeltaTime);
    }
}

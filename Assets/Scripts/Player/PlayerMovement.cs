using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform PlayerT;
    [Range(0,.5f)]
    public float moveSpeed;
    private float hInput;
    private float vInput;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Move();
    }
    private void ProcessInputs() 
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
    }
    private void Move()
    {
        var currentPos = new Vector3(PlayerT.position.x, PlayerT.position.y, PlayerT.position.z);
        PlayerT.position = currentPos + new Vector3(hInput, vInput, 0) * moveSpeed;
    }
}

using UnityEngine;
//mapbox unity sdk for 3D worlds

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform PlayerT;
    [Range(0, 5)]
    public int moveSpeed;
    private float speedVar;
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
        hInput = Input.GetAxisRaw("Horizontal") * .01f;
        vInput = Input.GetAxisRaw("Vertical") * .01f;
    }
    private void Move()
    {
        if (hInput > 0 && vInput > 0)
        {
            speedVar = moveSpeed * .7f;
        } else 
        {
            speedVar = moveSpeed;
        }
        var currentPos = new Vector3(PlayerT.position.x, PlayerT.position.y, PlayerT.position.z);
        PlayerT.position = currentPos + new Vector3(hInput, vInput, 0) * speedVar;
    }
}

using UnityEngine;
//mapbox unity sdk for 3D worlds

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform PlayerT;
    public Animator anim;
    [Range(0, 5)]
    public int moveSpeed;
    private float speedVar;
    private float hInput;
    private float vInput;
    private int _playerHealth;
    void Start()
    {
        _playerHealth = this.GetComponent<Fighter>().health;
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        if (hInput != 0 || vInput != 0)
        {
            Move();
            Emote();
        }
        else 
        {
            anim.Play("player_idle");

        }
        Debug.Log(_playerHealth);
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
    private void Emote()
    {
        if (hInput > 0) 
        {
            anim.Play("walk_right");
        }
        if (hInput < 0)
        {
            anim.Play("walk_left");
        }
        if (vInput > 0)
        {
            anim.Play("walk_up");
        }
        if (vInput < 0)
        {
            anim.Play("walk_down");
        }

    }
}

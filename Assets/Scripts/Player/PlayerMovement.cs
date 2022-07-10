using UnityEngine;
//mapbox unity sdk for 3D worlds

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float characterSpeed = 4f;
    // Start is called before the first frame update
    public Transform PlayerT;
    public Animator anim;
    [Range(0, 5)]
    public int moveSpeed;
    private float speedVar;
    private float hInput;
    private float vInput;
    private string walk_dir;
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
        //Debug.Log(_playerHealth);
    }
    private void ProcessInputs()
    {
        hInput = Input.GetAxisRaw("Horizontal") * characterSpeed * Time.deltaTime;
        vInput = Input.GetAxisRaw("Vertical") * characterSpeed * Time.deltaTime;
    }
    private void Move()
    {
        if (hInput != 0 && vInput != 0)
        {
            speedVar = moveSpeed * .7f;
        }
        else
        {
            speedVar = moveSpeed;
        }
        var currentPos = new Vector3(PlayerT.position.x, PlayerT.position.y, PlayerT.position.z);
        PlayerT.position = currentPos + new Vector3(hInput, vInput, 0) * speedVar;
    }
    //change this so that if walking one direction and then change direction the current face direction doesn't change
    private void Emote()
    {
        //get the current animation clip, if we're moving still
        walk_dir = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        //if we're stoppes, or going the same direction but with new diagonal input, play clip 

        if (hInput > 0)
        {
            if (walk_dir == "player_idle" || walk_dir == "walk_right" && vInput != 0)
            {
                anim.Play("walk_right");
            }

        }
        else if (hInput < 0)
        {
            if (walk_dir == "player_idle" || walk_dir == "walk_left" && vInput != 0)
            {
                anim.Play("walk_left");
            }
        }
        else if (vInput > 0)
        {
            if (walk_dir == "player_idle" || walk_dir == "walk_up" && hInput != 0)
            {
                anim.Play("walk_up");
            }
        }
        else if (vInput < 0)
        {
            if (walk_dir == "player_idle" || walk_dir == "walk_down" && hInput != 0)
            {
                anim.Play("walk_down");
            }
        }

    }
}

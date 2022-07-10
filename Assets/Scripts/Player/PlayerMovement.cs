using UnityEngine;
//mapbox unity sdk for 3D worlds

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform PlayerT;
    public Animator anim;
    [Range(0, 9)]
    public int moveSpeed;
    private float speedVar;
    private float hInput;
    private float vInput;
    private string walk_dir = "player_idle";
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
            anim.SetBool("isMoving", true);
            Move();
            Emote();
        }
        else if (hInput == 0 && vInput == 0)
        {
            anim.Play("player_idle");
        }
    }
    private void ProcessInputs() 
    {
        hInput = Input.GetAxisRaw("Horizontal"); //* .1f;
        vInput = Input.GetAxisRaw("Vertical"); //* .1f;
    }
    private void Move()
    {
        if (hInput != 0 && vInput != 0)
        {
            speedVar = moveSpeed * .7f;
        } else 
        {
            speedVar = moveSpeed;
        }
        var currentPos = new Vector3(PlayerT.position.x, PlayerT.position.y, PlayerT.position.z);
        PlayerT.position = currentPos + new Vector3(hInput, vInput, 0) * speedVar * Time.deltaTime;
    }
    //change this so that if walking one direction and then change direction the current face direction doesn't change
    private void Emote()
    {
        //get the current animation clip
        walk_dir = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        //if we're stopped, going the opposite direction, or going the same direction but with new diagonal input, play clip 

        if (hInput > 0)
        {
            if (walk_dir == "player_idle" || walk_dir == "walk_left" || walk_dir == "walk_right")
            {
                walk_dir = "walk_right";
            }
        }
        else if (hInput < 0)
        {
            if (walk_dir == "player_idle" || walk_dir == "walk_right" || walk_dir == "walk_left")
            {
                walk_dir = "walk_left";
            }
        }
        else if (vInput > 0)
        {
            if (walk_dir == "player_idle" || walk_dir == "walk_down" || walk_dir == "walk_up")
            {
                walk_dir = "walk_up";
            }
        }
        else if (vInput < 0)
        {
            if (walk_dir == "player_idle" || walk_dir == "walk_up" || walk_dir == "walk_down")
            {
                walk_dir = "walk_down";
            }
        }
        anim.Play(walk_dir);
    }
}

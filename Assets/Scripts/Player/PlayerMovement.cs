using System.Collections.Generic;
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
    private Vector3 moveVector;
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
            anim.SetBool("isMoving", false);
            anim.Play("player_idle");
        }
    }
    private void ProcessInputs() 
    {
        hInput = Input.GetAxisRaw("Horizontal"); //* .1f;
        vInput = Input.GetAxisRaw("Vertical"); //* .1f;
        moveVector = new Vector3(hInput, vInput, 0f);
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
        PlayerT.position = currentPos + moveVector * speedVar * Time.deltaTime;
    }
    //change this so that if walking one direction and then change direction the current face direction doesn't change
    private void Emote()
    {
        //get the current animation clip
        walk_dir = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        //horizontal input, ignore if up or down
        if (moveVector != Vector3.up && moveVector != Vector3.down)
        {
            if (hInput > 0)
            {
                walk_dir = "walk_right";
            }
            else if (hInput < 0)
            {
                walk_dir = "walk_left";
            }
        }
        //vertical movement
        else if (moveVector != Vector3.right && moveVector != Vector3.left)
        {
            if (vInput > 0)
            {
                walk_dir = "walk_up";
            }
            else if (vInput < 0)
            {
                walk_dir = "walk_down";
            }
        }
        //if nothing changed play the last animation clip
        anim.Play(walk_dir);
    }
}

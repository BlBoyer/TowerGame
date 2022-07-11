using System.Collections.Generic;
using UnityEngine;
//mapbox unity sdk for 3D worlds

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform PlayerT;
    public Animator anim;
    //public GameObject renderComponent;
    private SpriteRenderer _spriteRenderer;
    private Sprite[] _spriteLib;
    private Sprite _thisSprite;
    [Range(0, 9)]
    public int moveSpeed;
    private float speedVar;
    private float hInput;
    private float vInput;
    private string walk_dir = "player_idle";
    //private int _playerHealth;
    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _spriteLib = GetComponentInChildren<SpriteLib>().spriteArray;
        _thisSprite = _spriteLib[1];
       //_playerHealth = GetComponent<Fighter>().health;

    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        if (hInput != 0 || vInput != 0)
        {
            anim.enabled = true;
            anim.SetBool("isMoving", true);
            Move();
            Emote();
        }
        else if (hInput == 0 && vInput == 0)
        {
            anim.SetBool("isMoving", false);
            //anim.Play("player_idle");
            anim.StopPlayback();
            anim.enabled = false;
            //set Sprite here
            _spriteRenderer.sprite = _thisSprite;
            Debug.Log(_spriteRenderer.sprite);
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
        PlayerT.position = currentPos + new Vector3(hInput, vInput, 0f) * speedVar * Time.deltaTime;
    }
    //change this so that if walking one direction and then change direction the current face direction doesn't change
    private void Emote()
    {
        //get the current animation clip
        walk_dir = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        //horizontal input, ignore if up or down

        if (hInput > 0)
        {
            walk_dir = "walk_right";
            _thisSprite = _spriteLib[2];
            //Debug.Log(GetComponentInChildren<SpriteRenderer>().sprite.name);
        }
        else if (hInput < 0)
        {
            walk_dir = "walk_left";
            _thisSprite = _spriteLib[0];
        }
        //vertical movemente
        else if (vInput > 0)
        {
            walk_dir = "walk_up";
            _thisSprite = _spriteLib[3];
        }
        else if (vInput < 0)
        {
            walk_dir = "walk_down";
            _thisSprite = _spriteLib[1];
        }
        //if nothing changed play the last animation clip
        anim.Play(walk_dir);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public int state = 0;
    public int atstate = 0;
    Animator animator;
    CharacterController character;
    Vector3 v3;
    public float g = 10;
    private int rstate = 4;


    void Awake()
    {
        animator = GetComponent<Animator>();
        character = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Move();
        attack();
        rotateChar();
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            state = 1;
        }
        else state = 0;
        animator.SetInteger("state", state);
        animator.SetInteger("atstate", atstate);
    }
    void Move()
    {
        if (character.isGrounded)
        {
            v3 = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (Input.GetKeyDown(KeyCode.Space))
            {
                v3.y = jumpSpeed;
            }
        }
        v3 += Vector3.down * g * Time.deltaTime;
        character.Move(v3 * Time.deltaTime * speed);
    }
    void rotateChar()
    {
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            setState(1);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            setState(3);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            setState(5);
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            setState(7);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            setState(0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            setState(2);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            setState(4);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            setState(6);
        }
    }
    void setState(int currState)
    {
        int rotateValue = (currState - rstate) * 45;
        transform.Rotate(Vector3.up, rotateValue);
        rstate = currState;
    }
    void OnTriggerEnter(Collider character)
    {
        transform.position = new Vector3(0, 0, 0);
    }
    void attack()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (Input.GetKey(KeyCode.X))
        {
            atstate = 1;
            speed = 0;
        }
        else
        {
            atstate = 0;
            speed = 6;
        }
        if (info.IsName("attack"))
        {
            speed = 0;
        }
    }
}

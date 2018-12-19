﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{

    bool fWalk;
    bool bWalk = false;
    bool rWalk = false;
    bool lWalk = false;
    bool rAttack = false;
    bool lAttack = false;
    bool fAttack = false;
    bool bAttack = false;
    bool froll = false;
    bool broll = false;
    bool rroll = false;
    bool lroll = false;
    bool lbroll = false;
    bool rbroll = false;
    bool lfroll = false;
    bool rfroll = false;
    bool isGround = false;
    bool attack = false; //判斷是否為攻擊，讓移動攻擊不切換方向
    bool damage = false;
    bool roll = false;
    Animator animator;
    Vector3 v3;
    Vector3 s3;
    Vector3 p3;
    Rigidbody rb;
    public float runSpeed = 5;
    public bool safeTime = false;
    public bool invincible = false;
    public float invincibleTime = 0.4f;

    float dropScale;
    // Use this for initialization
    void Awake()
    {
        Physics.IgnoreLayerCollision(11, 11);
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        s3 = transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        p3 = transform.position;
        if (!damage && !roll)
        {
            Attack();
            Move();
        }
        Drop();
        if(isGround){
            Roll();
            RollMove();
        }
        animator.SetBool("fWalk", fWalk);
        animator.SetBool("bWalk", bWalk);
        animator.SetBool("rWalk", rWalk);
        animator.SetBool("lWalk", lWalk);
        animator.SetBool("rAttack", rAttack);
        animator.SetBool("lAttack", lAttack);
        animator.SetBool("fAttack", fAttack);
        animator.SetBool("bAttack", bAttack);
        animator.SetBool("attack", attack);
    }
    void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rWalk = true;
            lWalk = false;
            bWalk = false;
            fWalk = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            lWalk = true;
            rWalk = false;
            fWalk = false;
            bWalk = false;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            bWalk = true;
            fWalk = false;
            rWalk = false;
            lWalk = false;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            fWalk = true;
            bWalk = false;
            rWalk = false;
            lWalk = false;
        }
        if (!Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            fWalk = false;
            bWalk = false;
            rWalk = false;
            lWalk = false;
        }
        v3 = new Vector3(Input.GetAxis("Horizontal"), 0, (float)(Input.GetAxis("Vertical") * 1.5f));
        transform.Translate(v3 * Time.deltaTime * runSpeed);
    }
    void OnCollisionStay(Collision co)
    {
        if (co.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }
    void OnCollisionExit(Collision co) {
           if (co.gameObject.tag == "Ground")
        {
            isGround = false;
        }     
    }
    void Attack()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        rAttack = false;
        lAttack = false;
        fAttack = false;
        bAttack = false;
        if (Input.GetKeyDown(KeyCode.W))
        {
            bAttack = true;
            attack = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            fAttack = true;
            attack = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            rAttack = true;
            attack = true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            lAttack = true;
            attack = true;
        }
        if (info.IsName("rightattack") || info.IsName("leftattack") || info.IsName("backattack") || info.IsName("frontattack"))
        {
            runSpeed = 0;
        }
        if (!(info.IsName("rightattack") || info.IsName("leftattack") || info.IsName("backattack") || info.IsName("frontattack")) && !(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A)))
        {
            runSpeed = 7;
            attack = false;
            safeTime = false;
        }

    }
    public void Damage()
    {
        if (damage) { return; }
        damage = true;
        invincible = true;
        animator.SetTrigger("damage");
    }
    void EndDamge()
    {
        damage = false;
        Invoke("EndInvincible", invincibleTime);
    }
    void EndInvincible()
    {
        invincible = false;
    }

    void Drop()
    {
        dropScale = (float)(p3.y * 0.1 + 0.88);
        transform.localScale = s3 * dropScale;
        if (dropScale <= 0)
        {
            transform.position = new Vector3(0, 5, 0);
        }
    }
    void Roll()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(2);
        if (Input.GetKey(KeyCode.LeftArrow)&&Input.GetKey(KeyCode.UpArrow))
        {
            if ((Input.GetKeyDown(KeyCode.Space)) && roll == false && isGround)
            {
                Physics.IgnoreLayerCollision(10, 11);
                roll = true;
                lbroll = true;
                animator.SetBool("roll", roll);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow)&&Input.GetKey(KeyCode.UpArrow))
        {
            if ((Input.GetKeyDown(KeyCode.Space)) && roll == false && isGround)
            {
                Physics.IgnoreLayerCollision(10, 11);
                roll = true;
                rbroll = true;
                animator.SetBool("roll", roll);
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow)&&Input.GetKey(KeyCode.DownArrow))
        {
            if ((Input.GetKeyDown(KeyCode.Space)) && roll == false && isGround)
            {
                Physics.IgnoreLayerCollision(10, 11);
                roll = true;
                lfroll = true;
                animator.SetBool("roll", roll);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow)&&Input.GetKey(KeyCode.DownArrow))
        {
            if ((Input.GetKeyDown(KeyCode.Space)) && roll == false && isGround)
            {
                Physics.IgnoreLayerCollision(10, 11);
                roll = true;
                rfroll = true;
                animator.SetBool("roll", roll);
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if ((Input.GetKeyDown(KeyCode.Space)) && roll == false && isGround)
            {
                Physics.IgnoreLayerCollision(10, 11);
                roll = true;
                froll = true;
                animator.SetBool("roll", roll);
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if ((Input.GetKeyDown(KeyCode.Space)) && roll == false && isGround)
            {
                Physics.IgnoreLayerCollision(10, 11);
                roll = true;
                broll = true;
                animator.SetBool("roll", roll);
            }
            
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if ((Input.GetKeyDown(KeyCode.Space)) && roll == false && isGround)
            {
                Physics.IgnoreLayerCollision(10, 11);
                roll = true;
                lroll = true;
                animator.SetBool("roll", roll);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if ((Input.GetKeyDown(KeyCode.Space)) && roll == false && isGround)
            {
                Physics.IgnoreLayerCollision(10, 11);
                roll = true;
                rroll = true;
                animator.SetBool("roll", roll);
            }
        }
    }
    void RollMove(){
            if (froll)
            {
                Vector3 r3 = new Vector3(0, 0, transform.position.z - 9);
                r3 = r3.normalized;
                transform.Translate(r3 * Time.deltaTime * 13);
                
            }
            if (broll)
            {
                Vector3 r3 = new Vector3(0, 0, transform.position.z + 9);
                r3 = r3.normalized;
                transform.Translate(r3 * Time.deltaTime * 13);
            }
            if (rroll)
            {
                Vector3 r3 = new Vector3(transform.position.x + 9, 0, 0);
                r3 = r3.normalized;
                transform.Translate(r3 * Time.deltaTime * 9);
            }
            if (lroll)
            {
                Vector3 r3 = new Vector3(transform.position.x - 9, 0, 0);
                r3 = r3.normalized;
                transform.Translate(r3 * Time.deltaTime * 9);
            }
            if (lbroll)
            {
                Vector3 r3 = new Vector3(transform.position.x - 9, 0, transform.position.z + 9);
                r3 = r3.normalized;
                transform.Translate(r3 * Time.deltaTime * 11);
            }
            if (rbroll)
            {
                Vector3 r3 = new Vector3(transform.position.x + 9, 0, transform.position.z + 9);
                r3 = r3.normalized;
                transform.Translate(r3 * Time.deltaTime * 11);
            }
            if (lfroll)
            {
                Vector3 r3 = new Vector3(transform.position.x - 9, 0, transform.position.z - 9);
                r3 = r3.normalized;
                transform.Translate(r3 * Time.deltaTime * 11);
            }
            if (rfroll)
            {
                Vector3 r3 = new Vector3(transform.position.x + 9, 0, transform.position.z - 9);
                r3 = r3.normalized;
                transform.Translate(r3 * Time.deltaTime * 11);
            }
    }
    void EndRoll()
    {
        print(roll);
        roll = false;
        froll = false;
        broll = false;
        rroll = false;
        lroll = false;
        lbroll = false;
        rbroll = false;
        lfroll = false;
        rfroll = false;
        animator.SetBool("roll", roll);
        Physics.IgnoreLayerCollision(10, 11, false);
    }
    void StartSafe()
    {
        safeTime = true;
    }
    void EndSafe()
    {
        safeTime = false;
    }
}
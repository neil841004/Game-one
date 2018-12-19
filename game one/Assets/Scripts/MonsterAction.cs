using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAction : MonoBehaviour
{
    bool monsterAttack = true;
    bool charAttack = false;
    bool followChar = false;
    bool aiState = true;
    bool stopFollow = false;
    public int force = 8;
    private GameObject player;
    Vector3 distance;
    public int speed = 3;
    public float backRange = 7;
    int tMove = 50;
    public float AiRange = 4;
    Rigidbody rb;
    int aiX;
    int aiZ;
    float dropScale;
    Vector3 s3;
    bool safe;
    bool charInvincible;
    int charCombo;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        s3 = transform.localScale;
    }
    private void FixedUpdate()
    {
        charInvincible = GameObject.FindWithTag("Player").GetComponent<KnightController>().invincible;
        safe = GameObject.FindWithTag("Player").GetComponent<KnightController>().safeTime;
        charCombo = GameObject.FindWithTag("Player").GetComponent<KnightController>().combo;
        if (aiState == true)
        {
            aiX = Random.Range(-3, 3);
            aiZ = Random.Range(-3, 3);
            aiState = false;
        }
        distance = player.transform.position - transform.position;
        distance = distance.normalized;
        Move();
        Drop();
    }
    private void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag == "Player" && monsterAttack == true && safe == false && charInvincible == false)
        {
            co.rigidbody.velocity = -co.contacts[0].normal * force;
            co.gameObject.SendMessage("Damage");
            stopFollow = true;
            Invoke("ContinueMove", 1f);
            monsterAttack = false;
        }
    }
    void ContinueMove()
    {
        monsterAttack = true;
        stopFollow = false;
    }
    public void Back()
    {
        if (charAttack) { return; }
        charAttack = true;
        GetComponent<Rigidbody>().velocity = -distance * backRange * (0.7f + charCombo*0.2f);
        Invoke("EndCharAttack", 0.5f);
    }
    void EndCharAttack()
    {
        charAttack = false;
    }
    void Move()
    {
        float maxDistance;
        maxDistance = Vector3.Distance(player.transform.position, transform.position);
        if ((maxDistance < AiRange || followChar == true) && stopFollow == false)
        {
            followChar = true;
            transform.position += distance * speed * Time.deltaTime;
        }
        else if (maxDistance >= AiRange && followChar == false)
        {
            tMove++;
            if (tMove % 200 <= 49)
            {
                transform.Translate(aiX * Time.deltaTime, 0, aiZ * Time.deltaTime);
            }
            else if (tMove % 200 <= 99 && tMove >= 50)
            {
                transform.Translate(0, 0, 0);
            }
            else if (tMove % 200 <= 149 && tMove >= 100)
            {
                transform.Translate(-aiX * Time.deltaTime, 0, -aiZ * Time.deltaTime);
            }
            else if (tMove % 200 <= 200 && tMove >= 150)
            {
                transform.Translate(0, 0, 0);
            }
        }
    }
    void Drop()
    {
        dropScale = (float)(transform.position.y * 0.1 + 0.88);
        transform.localScale = s3 * dropScale;
        if (dropScale <= 0)
        {
            Destroy(gameObject);
        }
    }
}

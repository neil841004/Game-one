using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatGround : MonoBehaviour
{
    public GameObject littleGround;
    float x = 0;
    int t = 0;
    public Transform cube;
    // Use this for initialization

    // Update is called once per frame
    void Update()
    {

        bool clearState = GameObject.FindWithTag("Respawn").GetComponent<spawner>().clear;
        if (clearState)
        {
            if (t % 15 == 0)
            {
                CreatGround();
            }
            t++;
        }

    }

    void CreatGround()
    {
        Vector3 v3 = new Vector3(transform.position.x + cube.transform.forward.x * x, transform.position.y, transform.position.z + cube.transform.forward.z * x);
        Instantiate(littleGround, v3, transform.rotation, cube);
        x = x + 1.45f;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject buleSlime;
    public GameObject redSlime;
    public GameObject greenSlime;
    public GameObject[] monster;
    Vector3 vector3;
    int number = 3;
    public int spawnT = 150;
    public int beginSpawnT = 100;
    public bool clear = false;
    // Use this for initialization
    void Update()
    {
        if (!clear)
        {
            if (beginSpawnT % spawnT == 0)
            {
                Spawn();
            }
            beginSpawnT++;
        }
        if (beginSpawnT > 800)
        {
            beginSpawnT = 800;
        }
        if (beginSpawnT > 750 && !GameObject.FindGameObjectWithTag("Monster"))
        {
            clear = true;
        }
    }
    void Spawn()
    {
        for (float i = 0; i < 7; i += Random.RandomRange(1, 3.5f))
        {
            int targetX = Random.Range(-4, 4);
            int targetZ = Random.Range(-4, 4);
            vector3 = new Vector3(transform.position.x + targetX, 3, transform.position.z + targetZ);
            Instantiate(monster[Random.Range(0, 3)], vector3, transform.rotation);
        }
    }
}
// 38.52
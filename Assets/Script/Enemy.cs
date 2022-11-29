using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;

    // Boss
    public bool isBoss = false;
    public float spawnInterval;
    public int miniEnemySpawnCount;

    private float nextSpawn;

    private Rigidbody enemyRb;
    private GameObject player;

    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        if (isBoss)
        {
            spawnManager = FindObjectOfType<SpawnManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        
        enemyRb.AddForce(lookDirection * speed);

        if (isBoss)
        {
            if(Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnInterval;

                spawnManager.SpawnMiniEnemies(miniEnemySpawnCount);
            }
        }

        if(transform.position.y < -8)
        {
            Destroy(gameObject);
        }
    }
}

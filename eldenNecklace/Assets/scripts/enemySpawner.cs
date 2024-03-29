using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    private GameObject newEnemy;
    private int randomSpawnZone;
    private SpriteRenderer rend;
    private Vector3 spawnPosition;
    private float randomXpo, randomYpo;
    float difficulty = 10f;

    void Start()
    {
        InvokeRepeating("spawnNewEnemy", 1f, difficulty);
    }

    private void spawnNewEnemy(){
        randomSpawnZone = Random.Range(0,4);

        switch(randomSpawnZone){
            case 0:
                randomXpo = Random.Range(-1f, -1f);
                randomYpo = Random.Range(-1f, -1f);
                break;
            case 1:
                randomXpo = Random.Range(-1f, 1f);
                randomYpo = Random.Range(-1f, -1f);
                break;
            case 2:
                randomXpo = Random.Range(1f, 1f);
                randomYpo = Random.Range(-1f, 1f);
                break;
            case 3:
                randomXpo = Random.Range(1f, 1f);
                randomYpo = Random.Range(1f, 1f);
                break;
        }

        spawnPosition = new Vector3(randomXpo, randomYpo, 0f);
        newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        rend = newEnemy.GetComponent<SpriteRenderer>();
        if(difficulty > 1.0f){
            difficulty -= 0.5f;
        }
    }
}

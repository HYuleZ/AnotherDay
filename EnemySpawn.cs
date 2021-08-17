using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float maxZ, minZ;
    public GameObject[] enemy;
    public int numEnemies;
    public float spawnTime;

    private int currentNumEnemies;

    void Start()
    {
        
    }

    void Update()
    {
        if(currentNumEnemies >= numEnemies){
            int enemies = FindObjectsOfType<Enemy>().Length;
            if(enemies <= 0){
                FindObjectOfType<CameraFollow>().maxXandY.x = 200;
                gameObject.SetActive(false);
            }
        }
    }

    void SpawnEnemy(){
        bool positionX = Random.Range(0,2) == 0 ? true : false;
        Vector3 spawnPosition;
        spawnPosition.z = Random.Range(minZ, maxZ);
        if(positionX){
            spawnPosition = new Vector3(transform.position.x + 10, 0, spawnPosition.z);
        }else{
            spawnPosition = new Vector3(transform.position.x - 10, 0, spawnPosition.z);
        }
        Instantiate(enemy[Random.Range(0, enemy.Length)], spawnPosition, Quaternion.identity);
        currentNumEnemies++;
        if(currentNumEnemies < numEnemies){
            Invoke("SpawnEnemy", spawnTime);
        }
    }

    private void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            GetComponent<BoxCollider>().enabled = false;
            FindObjectOfType<CameraFollow>().maxXandY.x = transform.position.x;
            SpawnEnemy();
        }
    }
}

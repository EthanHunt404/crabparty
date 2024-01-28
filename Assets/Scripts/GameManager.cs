using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Setup Fields")]
    [SerializeField] private GameObject crab;
    [SerializeField] private GameObject player;

    [Header("Random Spawn Position Values")]
    [SerializeField] private float maxCrabXSpawnPosition;
    [SerializeField] private float minCrabXSpawnPosition;
    [SerializeField] private float maxCrabZSpawnPosition;
    [SerializeField] private float minCrabZSpawnPosition;

    private float randomX;
    private float randomZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            SpawnCrab();
        }
    }

    void SpawnCrab()
    {
        Instantiate(crab, player.transform.position + GenerateCrabSpawnPoint(), Quaternion.identity);
    }


    Vector3 GenerateCrabSpawnPoint()
    {
        float randomNumber = Random.Range(0, 4);

        if(randomNumber == 0)
        {
            randomX = Random.Range(minCrabXSpawnPosition, maxCrabXSpawnPosition);
            randomZ = Random.Range(minCrabZSpawnPosition, maxCrabZSpawnPosition);
        }else if(randomNumber == 1)
        {
            randomX = Random.Range(minCrabXSpawnPosition, -maxCrabXSpawnPosition);
            randomZ = Random.Range(minCrabZSpawnPosition, maxCrabZSpawnPosition);
        }else if(randomNumber == 2)
        {
            randomX = Random.Range(minCrabXSpawnPosition, maxCrabXSpawnPosition);
            randomZ = Random.Range(-minCrabZSpawnPosition, -maxCrabZSpawnPosition);
        }else if(randomNumber == 3)
        {
            randomX = Random.Range(-minCrabXSpawnPosition, -maxCrabXSpawnPosition);
            randomZ = Random.Range(-minCrabZSpawnPosition, -maxCrabZSpawnPosition);
        }



        Vector3 crabSpawnPosition = new Vector3(randomX, 0, randomZ);

        return crabSpawnPosition;
    }
}

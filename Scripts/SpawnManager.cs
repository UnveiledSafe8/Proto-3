using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject objectPrefab;
    private Vector3 objectspawnPos = new Vector3(35, 1, 0);
    private float startDelay = 2;
    private float spawnInterval = 2;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObjects", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnObjects()
    {
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(objectPrefab, objectspawnPos, objectPrefab.transform.rotation);
        }
        
    }
}

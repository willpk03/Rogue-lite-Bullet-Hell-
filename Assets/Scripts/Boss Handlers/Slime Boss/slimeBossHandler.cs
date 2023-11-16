using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeBossHandler : MonoBehaviour
{
    public int overallHealth; 
    public int Spawned;
    public GameObject initialEnemy;

    public GameObject Stage2;
    public GameObject Stage3;
    public GameObject Stage4;
    public GameObject Stage5;
    // Start is called before the first frame update
    void Start()
    {
        Spawned = 0;
        Camera mainCamera;
        mainCamera = Camera.main;
        // Get the camera's viewport dimensions
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Calculate the edges based on the camera's position and size
        float leftEdge = mainCamera.transform.position.x - cameraWidth / 2f;
        float rightEdge = mainCamera.transform.position.x + cameraWidth / 2f;
        float topEdge = mainCamera.transform.position.y + cameraHeight / 2f;
        float bottomEdge = mainCamera.transform.position.y - cameraHeight / 2f;


        // Randomly select one of the four edges
        int randomEdge = Random.Range(0, 4);

        Vector3 spawnPosition = Vector3.zero;

        // Depending on the selected edge, calculate the spawn position
         switch (randomEdge)
        {
            case 0: // Left edge
                spawnPosition = new Vector3(leftEdge, Random.Range(bottomEdge, topEdge), 0);
                break;
            case 1: // Right edge
                spawnPosition = new Vector3(rightEdge, Random.Range(bottomEdge, topEdge), 0);
                break;
            case 2: // Top edge
                spawnPosition = new Vector3(Random.Range(leftEdge, rightEdge), topEdge, 0);
                break;
            case 3: // Bottom edge
                spawnPosition = new Vector3(Random.Range(leftEdge, rightEdge), bottomEdge, 0);
                break;
        }

        // Spawn the object at the calculated position
        //Debug.Log(spawnPosition);
        // Convert viewport position to world space
        Vector3 randomWorldPosition = mainCamera.ViewportToWorldPoint(spawnPosition);

  
        Instantiate(initialEnemy, spawnPosition, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void healthdamaged() {
        overallHealth = overallHealth - 1;
        if(overallHealth > 0) {
            
        }else {
            //if(Spawned == 0){
                Debug.Log("--- Dead ---");
                GameObject playerObject = GameObject.FindWithTag("Player1");
                Player1Controller scriptComponent = playerObject.GetComponent<Player1Controller>();
                scriptComponent.spawnAmount = scriptComponent.spawnAmount + 1;
            //} 
        }
    }

    public void healthMerged() {
        if(overallHealth <= 0) {
            overallHealth = overallHealth + 1;
        }
    }
}

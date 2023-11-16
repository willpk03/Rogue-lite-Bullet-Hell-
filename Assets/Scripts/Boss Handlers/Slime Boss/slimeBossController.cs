using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Slime Boss Handler. Handles Movement for the individual characters and with happens if they hit the player or get hit by a bullet.
public class slimeBossController : MonoBehaviour
{
    private Transform playerTransform;
    public float speed = 5.0f; // Speed at which the object moves towards the player
    Vector3 direction;
    GameObject playerObject;
    public int health = 1; 
    public Color targetColor = Color.white; // Change this to the desired color
    private Color originalColor;
    private Renderer rend;
    public int Stage;
    float cameraHeight;
    float cameraWidth;
    float leftEdge;
    float rightEdge;
    float topEdge;
    float bottomEdge;
    Camera mainCamera;

    public float repelDuration = 2f;  // Duration for which the enemy moves away after collision
    private bool isRepelling = false;
    private float repelTimer = 0f;
    void Start()
    {
        mainCamera = Camera.main;
        cameraHeight = 2f * mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;
        leftEdge = mainCamera.transform.position.x - cameraWidth / 2f;
        rightEdge = mainCamera.transform.position.x + cameraWidth / 2f;
        topEdge = mainCamera.transform.position.y + cameraHeight / 2f;
        bottomEdge = mainCamera.transform.position.y - cameraHeight / 2f;

        // Find the player GameObject by its tag
        playerObject = GameObject.FindWithTag("Player1");
        playerTransform = playerObject.transform;
        if(Stage > 1) {
             int randomEdge = Random.Range(0, 4);

            Vector3 spawnPosition = Vector3.zero;

            // Depending on the selected edge, calculate the spawn position
            switch (randomEdge)
            {
            case 0: // Left edge
                direction = new Vector3(leftEdge, Random.Range(bottomEdge, topEdge), 0);
                break;
            case 1: // Right edge
                direction = new Vector3(rightEdge, Random.Range(bottomEdge, topEdge), 0);
                break;
            case 2: // Top edge
                direction = new Vector3(Random.Range(leftEdge, rightEdge), topEdge, 0);
                break;
            case 3: // Bottom edge
                direction = new Vector3(Random.Range(leftEdge, rightEdge), bottomEdge, 0);
                break;
            }
            Debug.Log(direction);
            direction = direction - transform.position;
        }else {
            if (playerObject != null){
                    // Get the Transform component of the player GameObject
                    
                    Debug.Log(playerObject);
                    direction = playerTransform.position - transform.position;
                    Debug.Log(playerTransform.position);
                }
                else
                {
                    Debug.LogError("Player not found with tag 'Player'. Make sure to assign the correct tag to the player GameObject.");
            }
        }

        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }


    private void Update()
    {
        if (isRepelling)
        {
            // Move away from the player
            transform.Translate(direction *-1 * speed * Time.deltaTime);

            // Update the timer
            repelTimer -= Time.deltaTime;

            // Check if repel duration is over
            if (repelTimer <= 0f)
            {
                isRepelling = false;
            }
        }
        else {
            if (playerTransform != null)
            {
                // Normalize the direction vector to have a length of 1
                direction.Normalize();
                // Move the object towards the player using the calculated direction and speed
                transform.Translate(direction * speed * Time.deltaTime);
            }
        }
        if(transform.position.x < leftEdge || transform.position.x > rightEdge || transform.position.y > topEdge || transform.position.y < bottomEdge) {
            //Redirects the Boss back to aiming towards the player.
            playerTransform = playerObject.transform;
            direction = playerTransform.position - transform.position;
            
            Debug.Log("Hit border");
            //If the boss had just hit the player and was repelled back this will ensure it doesn't go out of bounds.
            isRepelling = false;
            //Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1") {
            Player1Controller scriptComponent = playerObject.GetComponent<Player1Controller>();
            scriptComponent.health = scriptComponent.health - 1;
            scriptComponent.healthChange();
            //Destroy(gameObject);
            isRepelling = true;
            repelTimer = repelDuration;
            
        } else {
            //Debug.Log(collision.gameObject.tag);
        }
    }

    public IEnumerator healthChange() {
        rend.material.color = targetColor;
        yield return new WaitForSeconds(1.0f);
        rend.material.color = originalColor;
    }

    public void splitOff() {
        GameObject controllerObject = GameObject.FindWithTag("SlimeBossController");
        slimeBossHandler scriptComponent = controllerObject.GetComponent<slimeBossHandler>();
        GameObject nextStage = null;
        if(Stage == 1){
            nextStage = scriptComponent.Stage2;
            scriptComponent.healthdamaged();
        } else if (Stage == 2) {
            nextStage = scriptComponent.Stage3;
            scriptComponent.healthdamaged();
        }else if (Stage == 3) {
            scriptComponent.healthdamaged();
            if(scriptComponent.Stage4 == null) {
                Destroy(gameObject);
                return;
            }else {
                nextStage = scriptComponent.Stage4;
                
            }
        }else if (Stage == 4) {
            scriptComponent.healthdamaged();
            if(scriptComponent.Stage5 == null) {
                Destroy(gameObject);
                return;
            }else {
                nextStage = scriptComponent.Stage4;
                
            }
        }else{ 
            Debug.Log("Couldn't find a stage.");
            //nextStage = null;
        }
        GameObject splitOff1 = Instantiate(nextStage, transform.position, Quaternion.identity);
        GameObject splitOff2 = Instantiate(nextStage, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }

    
}

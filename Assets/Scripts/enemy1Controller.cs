using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy1Controller : MonoBehaviour
{
    private Transform playerTransform;
    public float speed = 5.0f; // Speed at which the object moves towards the player
    Vector3 direction;
    GameObject playerObject;
    public int health = 1; 
    public Color targetColor = Color.white; // Change this to the desired color
    private Color originalColor;
    private Renderer rend;
    void Start()
    {
        // Find the player GameObject by its tag
        playerObject = GameObject.FindWithTag("Player1");

        if (playerObject != null)
        {
            // Get the Transform component of the player GameObject
            playerTransform = playerObject.transform;
            direction = playerTransform.position - transform.position;
        }
        else
        {
            Debug.LogError("Player not found with tag 'Player'. Make sure to assign the correct tag to the player GameObject.");
        }

        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }


    private void Update()
    {
        if (playerTransform != null)
        {
            // Calculate the direction from the object to the player
            

            // Normalize the direction vector to have a length of 1
            direction.Normalize();

            // Move the object towards the player using the calculated direction and speed
            transform.Translate(direction * speed * Time.deltaTime);
            Camera mainCamera = Camera.main;
            float cameraHeight = 2f * mainCamera.orthographicSize;
            float cameraWidth = cameraHeight * mainCamera.aspect;
            float leftEdge = mainCamera.transform.position.x - cameraWidth / 2f;
            float rightEdge = mainCamera.transform.position.x + cameraWidth / 2f;
            float topEdge = mainCamera.transform.position.y + cameraHeight / 2f;
            float bottomEdge = mainCamera.transform.position.y - cameraHeight / 2f;

            if(transform.position.x < leftEdge || transform.position.x > rightEdge || transform.position.y > topEdge || transform.position.y < bottomEdge) {
                Destroy(gameObject);
            }
            if(health <= 0) {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1") {
            Player1Controller scriptComponent = playerObject.GetComponent<Player1Controller>();
            scriptComponent.health = scriptComponent.health - 1;
            scriptComponent.healthChange();
            Destroy(gameObject);
            if(scriptComponent.health <= 0) {
                Destroy(playerObject);
            }
        } else {
            //Debug.Log(collision.gameObject.tag);
        }
    }

    public IEnumerator healthChange() {
        rend.material.color = targetColor;
        yield return new WaitForSeconds(1.0f);
        rend.material.color = originalColor;
    }

    public int isClose(int min, int max, int point){
        scoretext.text = "Health: " + score.ToString();
    }
}

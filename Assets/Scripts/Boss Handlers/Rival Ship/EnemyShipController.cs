using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    
        private Transform playerTransform;
    public float speed; // Speed at which the object moves towards the player
    Vector2 mousePos;
    Vector3 direction;
    GameObject playerObject;
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
    public int previousBorder;
    public GameObject bullet;
    public float bulletCD;
    public int health;
    public int score;
    public DataHolder dataHolder;   
    void Start()
    {
        mainCamera = Camera.main;
        cameraHeight = 2f * mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;
        leftEdge = mainCamera.transform.position.x - cameraWidth / 2f;
        rightEdge = mainCamera.transform.position.x + cameraWidth / 2f;
        topEdge = mainCamera.transform.position.y + cameraHeight / 2f;
        bottomEdge = mainCamera.transform.position.y - cameraHeight / 2f;

        bulletCD = (float)dataHolder.myData.reloadCD;
        health = dataHolder.myData.health;
        speed = dataHolder.myData.speed;

        // Find the player GameObject by its tag
        
            // Get the Transform component of the player GameObject 
            direction = randBorderPos();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            direction = direction - transform.position;

            playerObject = GameObject.FindWithTag("Player1");
            if (playerObject != null){
                // Get the Transform component of the player GameObject 
                //Quaternion rotation = Quaternion.LookRotation(playerObject.transform.position - transform.position, transform.TransformDirection(Vector3.up));
                //transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
            }
            else{
                Debug.LogError("Player not found with tag 'Player'. Make sure to assign the correct tag to the player GameObject.");
            }

            //transform.up = direction;
            Debug.Log(direction);
            InvokeRepeating("LaunchProjectile", 2.0f, bulletCD);

        
    }


    private void Update()
    {
        Quaternion rotation = Quaternion.LookRotation(playerObject.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
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
            
                // Normalize the direction vector to have a length of 1
                direction.Normalize();
                // Move the object towards the player using the calculated direction and speed
                transform.Translate(direction * speed * Time.deltaTime);
        }
        if(transform.position.x < leftEdge || transform.position.x > rightEdge || transform.position.y > topEdge || transform.position.y < bottomEdge) {
            //Redirects the Boss back to aiming towards the player.
            
            
            direction = randBorderPos();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            direction = direction - transform.position;
            //transform.up = direction;
            
            Debug.Log("Hit border");
            //If the boss had just hit the player and was repelled back this will ensure it doesn't go out of bounds.
            isRepelling = false;
            //Destroy(gameObject);
        }

        if(health <= 0) {
            Destroy(gameObject);
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

    public Vector3 randBorderPos(){
        int randomEdge = Random.Range(0, 4);
        while(randomEdge == previousBorder){
            randomEdge = Random.Range(0, 4);
        }
            Vector3 newPos = Vector3.zero;


            // Depending on the selected edge, calculate the new position to go towards
            switch (randomEdge)
            {
            case 0: // Left edge
                newPos = new Vector3(leftEdge, Random.Range(bottomEdge, topEdge), 0);
                break;
            case 1: // Right edge
                newPos = new Vector3(rightEdge, Random.Range(bottomEdge, topEdge), 0);
                break;
            case 2: // Top edge
                newPos = new Vector3(Random.Range(leftEdge, rightEdge), topEdge, 0);
                break;
            case 3: // Bottom edge
                newPos = new Vector3(Random.Range(leftEdge, rightEdge), bottomEdge, 0);
                break;
            }

            previousBorder = randomEdge;


            //transform.position = newPos;

        return newPos;
    }

    void LaunchProjectile() {
        float xChange;
        float yChange;
        float posX;
        float posY;
        xChange = 0;
        yChange = 0;
        posX = transform.position.x;
        posY = transform.position.y;
        mousePos = Input. mousePosition;
        GameObject bulletClone = (GameObject) Instantiate(bullet, new Vector3(posX + xChange, posY + yChange, transform.position.z), transform.rotation);    
    }
}

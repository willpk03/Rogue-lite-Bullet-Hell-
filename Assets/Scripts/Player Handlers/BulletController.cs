using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BulletController : MonoBehaviour

{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    Vector2 mousePos;
    public GameObject target;
    float posX;
    float posY;
    public int health;
     Vector2 direction;
    public float speed = 4f;
    public Vector3 myVector;
    void Start()
    {
        target = GameObject.FindWithTag("Player1");
        posX = target.transform.position.x;
        posY = target.transform.position.y;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        direction = new Vector2(mousePos.x - posX, mousePos.y - posY);
         rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Sets speed towards mouse
        //Problem occuring need it to go towards the direction of the MOUSE but not consider its actuall location
        
        //posX = target.transform.position.x;
        //posY = target.transform.position.y;
        posX = transform.position.x;
        posY = transform.position.y;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        direction = new Vector2(mousePos.x - posX, mousePos.y - posY);
        //Debug.Log("test2" + direction);
        rb.velocity = myVector;

        //Checks if needs to delete
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
        
    }
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Bullet hit enemy");
        if (collision.gameObject.tag == "Enemy") {
            
            enemy1Controller scriptComponent = collision.gameObject.GetComponent<enemy1Controller>();
            scriptComponent.health = scriptComponent.health - 1;
            health = health - 1;
            Debug.Log("Enemy Hit");
            Player1Controller playerscriptComponent = target.GetComponent<Player1Controller>();
            playerscriptComponent.score = playerscriptComponent.score + 1;
            playerscriptComponent.scoreChange();
            StartCoroutine(scriptComponent.healthChange());
            if(health <= 0) {
                Destroy(gameObject);
            }
            
        } else if (collision.gameObject.tag == "SlimeBoss") {
            slimeBossController sbComponent = collision.gameObject.GetComponent<slimeBossController>();
            Player1Controller playerscriptComponent = target.GetComponent<Player1Controller>();
            playerscriptComponent.score = playerscriptComponent.score + 1;
            playerscriptComponent.scoreChange();
            sbComponent.splitOff();
            health = health - 1;
            if(health <= 0) {
                Destroy(gameObject);
            }
            //Debug.Log(collision.gameObject.tag);
        }  else if (collision.gameObject.tag == "TeleporterBoss") {
            TeleporterHandler scriptComponent = collision.gameObject.GetComponent<TeleporterHandler>();
            scriptComponent.health = scriptComponent.health - 1;
            health = health - 1;
            scriptComponent.randTeleport();

            Player1Controller playerscriptComponent = target.GetComponent<Player1Controller>();
            playerscriptComponent.score = playerscriptComponent.score + 1;
            playerscriptComponent.scoreChange();
        }
    }
}

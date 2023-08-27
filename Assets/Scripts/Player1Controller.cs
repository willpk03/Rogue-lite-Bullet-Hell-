using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player1Controller : MonoBehaviour
{
    public float speed;
    Vector2 targetPosition;
    Vector2 spawnPosition;
    public float direction;
    float xRot;
    float yRot;
    float zRot;
    float posX;
    float posY;
    float posZ;
    public GameObject obj;
    public float runSpeed = 1.0f;
    public float AngleDeg;
    float movelimiter;
    float bulletCD;
    float nextBullet;
    public GameObject bullet;
    public GameObject shield;
    private Rigidbody2D rb;
    public float bulletSpeed;
    public int health;
    public TMPro.TMP_Text healthtext;
    int healthcheck;
    Camera cam;
    Transform my;
    Rigidbody2D body;   
    //Direction
    //1= North
    //2 = East
    //3 = South
    //4 = West
    void Start()
    {
        body = GetComponent<Rigidbody2D>(); 
        direction = 1;
        targetPosition = new Vector2(0.0f, 0.0f);
        bulletSpeed = 2f;
        health = 3;
        healthChange();
        cam = Camera.main;
        my = this.transform;
        InvokeRepeating("LaunchProjectile", 2.0f, 0.3f);
    }
    void LaunchProjectile() {

    }

    // Update is called once per frame
    void Update()
    {
        healthcheck = 1;
        //The rotations currently
        xRot = transform.rotation.x;
        yRot = transform.rotation.y;
        zRot = transform.rotation.z;
        //The positions currently
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;
        Vector2 mousePos = Input. mousePosition;
        if (Input.GetKey(KeyCode.W))
        {
            body.velocity = new Vector2(0, 1 * runSpeed);
        }  
        if (Input.GetKey(KeyCode.A))
        {
            body.velocity = new Vector2(-1 * runSpeed, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            body.velocity = new Vector2(0, -1 * runSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            body.velocity = new Vector2(1 * runSpeed, 0);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            body.velocity = new Vector2(0 ,0);
        } 
        if (Input.GetKeyUp(KeyCode.A))
        {
            //Turn Right
            body.velocity = new Vector2(0 ,0);
        } else if (Input.GetKeyUp(KeyCode.D))
        {
            //Turn left
            body.velocity = new Vector2(0 ,0);
        } else if (Input.GetKeyUp(KeyCode.S))
        {
            //Move down
            body.velocity = new Vector2(0 ,0);
        }
        //Change it's direction
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - posX, mousePos.y - posY);
        transform.up = direction;
        
            
    }

   private void OnTriggerEnter2D(Collider2D collision)
    {
    if (collision.gameObject.tag == "Bullet") {
        Destroy(collision.gameObject);
            
        health = health - 1;
        healthChange();
        if (health == 0) {
            //Gameover
                    
            Debug.Log("GAMEOVER");
            Destroy(gameObject);
        }
    } else {
        Debug.Log(collision.gameObject.tag);
    }
            
        
    }

    private void healthChange() {
        healthtext.text = "Health: " + health.ToString();
    }
}

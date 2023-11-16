using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player2Controller : MonoBehaviour
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
    Rigidbody2D body;
    public float runSpeed = 1.0f;
    float movelimiter;
    float bulletCD;
    float nextBullet;
    public GameObject bullet;
    private Rigidbody2D rb;
    public float bulletSpeed;
    public int health;
    public TMPro.TMP_Text healthtext;
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
        nextBullet = 0f;
        healthChange();

        //Bullet CD
        bulletCD = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //The rotations currently
        xRot = transform.rotation.x;
        yRot = transform.rotation.y;
        zRot = transform.rotation.z;
        //The positions currently
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Move forward
            switch (direction){
                case 1:
                    //Vertical upwards
                    body.velocity = new Vector2(0, 1 * runSpeed);
                    break;
                case 2:
                    //Right
                    body.velocity = new Vector2(-1 * runSpeed, 0);
                    break;
                case 3:
                    //Vertical downwards
                    body.velocity = new Vector2(0, -1 * runSpeed);
                    break;
                case 4:
                    //Left
                    body.velocity = new Vector2(1 * runSpeed, 0);
                    break;
            }
        }  
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            body.velocity = new Vector2(0 ,0);
        } 
        if (Input.GetKeyUp(KeyCode.Keypad0) && Time.time > nextBullet) {
            float xChange;
            float yChange;
            xChange = 0;
            yChange = 0;
            nextBullet = Time.time + bulletCD;
             switch (direction){
                case 1:
                    yChange = +1;
                    //Vertical upwards
                    //Appear Upwards +y

                    break;
                case 2:
                    xChange = -1;
                    //Right
                    //Appear to the right +x
                    
                    
                    break;
                case 3:
                    yChange = -1;
                    //Vertical downwards
                    //Appear downwards -y
                    
                    
                    break;
                case 4:
                    xChange = +1;
                    //Left
                    //Appear to the left -x
                    
                    
                    break;
            }
            
            GameObject bulletClone = (GameObject) Instantiate(bullet, new Vector3(posX + xChange, posY + yChange, posZ), transform.rotation);
            switch (direction){
                case 1:
                    //Vertical upwards
                    //Appear Upwards +y
                    
                    
                    rb = bulletClone.GetComponent<Rigidbody2D>();
                    rb.velocity = new Vector2(0, 10 * bulletSpeed);

                    break;
                case 2:
                    //Right
            
                    
                    rb = bulletClone.GetComponent<Rigidbody2D>();
                    rb.velocity = new Vector2(-10 * bulletSpeed, 0);
                    //Appear to the right +x
                    break;
                case 3:
                    
                   
                    rb = bulletClone.GetComponent<Rigidbody2D>();
                    rb.velocity = new Vector2(0, -10 * bulletSpeed);
                    //Vertical downwards
                    //Appear downwards -y
                    break;
                case 4:
                    
                   
                    rb = bulletClone.GetComponent<Rigidbody2D>();
                    rb.velocity = new Vector2(10 * bulletSpeed, 0);
                    //Left
                    //Appear to the left -x
                    break;
            }
        } 
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            //Turn Right
            direction = direction + 1;
            if (direction == 5) {
                direction = 1;
            }
            transform.Rotate(0, 0, 90);
        } else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            //Turn left
            direction = direction - 1;
            if (direction == 0) {
                direction = 4;
            }
            transform.Rotate(xRot, yRot, -90);
        } else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            //Move down
        }
        //Change it's direction
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
        }
    }

    private void healthChange() {
        healthtext.text = "Health: " + health.ToString();
    }
}

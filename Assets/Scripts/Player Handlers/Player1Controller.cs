using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
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
    float nextBullet;
    public GameObject bullet;
    public GameObject bulletB;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject slimeBoss;
    public int spawnAmount;
    GameObject spawnedEnemy;
    public GameObject shield;
    private Rigidbody2D rb;
    public float bulletSpeed;
    public float bulletCD;
    public int health;
    public int score;
    public int BulletPierce;
    public TMPro.TMP_Text healthtext;
    public TMPro.TMP_Text scoretext;
    public GameObject GameOverUI;
    int healthcheck;
    Vector2 mousePos;
    Camera cam;
    Transform my;
    Rigidbody2D body;
    public string BulletType;
    public DataHolder dataHolder;   
    public bool slimeSpawned;

    //Direction
    //1= North
    //2 = East
    //3 = South
    //4 = West
    public float spawnDistance = 1.0f;
    void Start()
    {
        body = GetComponent<Rigidbody2D>(); 
        direction = 1;
        targetPosition = new Vector2(0.0f, 0.0f);
        bulletCD = (float)dataHolder.myData.reloadCD;
        health = dataHolder.myData.health;
        runSpeed = dataHolder.myData.speed;
        BulletPierce = dataHolder.myData.pierce;
        BulletType = dataHolder.myData.BulletType; 
        healthChange();
        cam = Camera.main;
        my = this.transform;
        Debug.Log(bulletCD);
        spawnAmount = 0;
        InvokeRepeating("LaunchProjectile", 2.0f, bulletCD);
        InvokeRepeating("CreateEnemy", 2.0f, 1.0f);
        slimeSpawned = false;
    }

    void CreateEnemy() {
        float xChange;
        float yChange;
        Camera mainCamera;
        xChange = 0;
        yChange = 0;
        posX = transform.position.x;
        posY = transform.position.y;

        //Select which enemy to spawn Scaffold
        int randomEnemyType;
        if(score < 20){
            randomEnemyType = 0;
        } else if (score < 50) { 
            randomEnemyType = Random.Range(0,3);
        }else{
            randomEnemyType = Random.Range(0,4);
        }
        Debug.Log(randomEnemyType);
        switch (randomEnemyType) {
            case 0: //Type 1
                spawnedEnemy = enemy1;
                break;
            case 1: //type 2
                spawnedEnemy = enemy2;
                break;
            case 2: //type 3
                spawnedEnemy = enemy3;
                break;
            case 3: 
                spawnedEnemy = enemy4;
                break;
            
        }

        mousePos = Input. mousePosition;
        Vector2 dir = Random.insideUnitCircle;
        Vector3 position = Vector3.zero;
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
        Vector3 randomWorldPosition = cam.ViewportToWorldPoint(spawnPosition);

        if (spawnAmount == 40) {
            //Run Boss phase 
            if(slimeSpawned == false){
                Instantiate(slimeBoss, spawnPosition, Quaternion.identity);
                slimeSpawned = true;
                Debug.Log("Slime Boss Spawned.");
                Debug.Log("---------------------------");
            } else {
                Debug.Log("Slime already spawned");
            }
            
            //Only change spawnAmount when the boss has died
        } else {
            GameObject createdEnemy = (GameObject) Instantiate(spawnedEnemy, spawnPosition, Quaternion.identity);
            spawnAmount++;
        }
        // Spawn the object at the calculated position
        
        
    
    }

    void LaunchProjectile() {
        float xChange;
        float yChange;
        xChange = 0;
        yChange = 0;
        posX = transform.position.x;
        posY = transform.position.y;
        mousePos = Input. mousePosition;
        if(BulletType == "A") {
            GameObject bulletClone = (GameObject) Instantiate(bullet, new Vector3(posX + xChange, posY + yChange, posZ), transform.rotation);
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 direction = new Vector2(mousePos.x - posX, mousePos.y - posY);
            Rigidbody2D b = bulletClone.GetComponent<Rigidbody2D>();
            float speedbullet = 4;
            Vector3 velocitybullet = speedbullet * direction;
            //b.velocity = velocitybullet;
            
            BulletController scriptComponent = bulletClone.GetComponent<BulletController>();
            scriptComponent.myVector = velocitybullet;
            scriptComponent.health = BulletPierce;
        } else if(BulletType == "B"){
            GameObject bulletClone = (GameObject) Instantiate(bulletB, new Vector3(posX + xChange, posY + yChange, posZ), transform.rotation);
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 direction = new Vector2(mousePos.x - posX, mousePos.y - posY);
            Rigidbody2D b = bulletClone.GetComponent<Rigidbody2D>();
            float speedbullet = 4;
            Vector3 velocitybullet = speedbullet * direction;
            //b.velocity = velocitybullet;
            
            BulletController scriptComponent = bulletClone.GetComponent<BulletController>();
            scriptComponent.myVector = velocitybullet;
            scriptComponent.health = BulletPierce;
        }
        
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
        mousePos = Input. mousePosition;
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
        /*Destroy(collision.gameObject);
            
        health = health - 1;
        healthChange();
        if (health == 0) {
            //Gameover
                    
            Debug.Log("GAMEOVER");
            Destroy(gameObject);
        }*/
    } else {
        Debug.Log(collision.gameObject.tag);
    }
            
        
    }

    public void healthChange() {
        healthtext.text = "Health: " + health.ToString();
        if(health <= 0) {
            //Handles Gameover screen
            GameOverUI.SetActive(true);
            if(dataHolder.myData.HScredits < score){
                dataHolder.myData.HScredits = score;
            }
            
            Destroy(gameObject);
                      
            
            //SceneManager.LoadScene("HomeScreen");
        }
    }

    public void scoreChange() {
        scoretext.text = "Score: " + score.ToString();
    }
}

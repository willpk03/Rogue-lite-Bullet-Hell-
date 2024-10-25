<h1>Run In In depth Details</h2> 
This is a project created to test an idea of creating an arcade game with a slight touch of roguelite aspect of slowly going in and out of a dungeon upgrading your character before each entry into the 'dungeon'. 

<h4>View a Demo of the game here:<a href="https://youtu.be/UA_tExFqTC4">click here</a></h4>

<h3>How to view this project</h3>
You can view this project on Unity 2020.3.29f1. Ensure you download this repisitory and in the unity laucher click on add project from disk in the open drop down menu.

<h2> How it was designed</h3>
Run In, takes two mains area, that being data that is changed inbetween each attempt (character speed, bullet speed, character health, bullet penetration), and the handling of everything when the game is occuring (Enemy Spawn, Controls, Player Character). 


<h3>The Shop</h3>
The shop main goal is to be a simple area for the player to buy new stats and quickly try out new builds and jump right back in. The current concept only includes 4 stats but this could go further beyond, to include different weapons, abilities or more stats.  
<img src="img2.PNG" alt="The upgrade screen of the game">
<br>

All the data for that is transfered between scenes is stored in a ScriptableObject. 


    [CreateAssetMenu(fileName = "myData", menuName = "ScriptableObjects/myData")]
    public class myData : ScriptableObject
    {
        public int health; //Player Health
        public float speed; //Player Movement Speed
        public int HScredits; //Players highest reached Score
        public double reloadCD; //Players Bullet CD
        public int pierce; //Bullet will do deal 1 Damage X amount of times
        public string BulletType; //What sort of bullet is being used
    }

> Excerpt taken from the following [File](https://github.com/willpk03/Rogue-lite-Bullet-Hell-/blob/main/Assets/Scripts/myData.asset)


<h3> Battle Screen</h3>
<img src="img1.PNG" alt="The main screen of the game">

<h4> Handling Enemy Spawns </h4> 
I wanted to mix both a static progression into the difficulty while also having a randomness that will change how each floor goes without putting the player off. This was done by aplying weights for each enemy spawn and setting a lowest possible floor spawn. While to ensure that new discovered enemies don't instantly overwhelm the player, no new spawn will appear twice until the player has at least defeated them once.
<br>



      //Select which enemy to spawn Scaffold
    int randomNumber;
    if(score < 20){
       randomNumber = Random.Range(1, 50);
    } else if (score < 50) { 
        randomNumber = Random.Range(1, 90);
    }else{
        randomNumber = Random.Range(40, 101);
    }
    float highestPercentile = -1f;
    GameObject highestPercentileObject = null;
    Debug.Log("Random number generated: " + randomNumber);
    // Check each specified percentile
    foreach (var percentileObject in percentileObjects)
    {
        float threshold = percentileObject.threshold;
        GameObject obj = percentileObject.gameObject;
    
        if (randomNumber >= threshold && threshold > highestPercentile)
        {
            // Update the highest percentile and its corresponding GameObject
            highestPercentile = threshold;
            highestPercentileObject = obj;
            Debug.Log("New highest value");
        }
    }
    // Checks to ensure object wasn't NULL
        if (highestPercentileObject == null)
        {
            // Code to be executed if the random number is outside all specified percentiles
            Debug.Log($"Random number {randomNumber} is outside all specified percentiles.");
        }else {
            //Sets the object to be the spawned object
            spawnedEnemy = highestPercentileObject;
        } 
> Excerpt taken from the following [File](https://github.com/willpk03/Rogue-lite-Bullet-Hell-/blob/main/Assets/Scripts/Player%20Handlers/Player1Controller.cs)

With the <b>PercentileObject</b> being a public created class containing both a threshold percentile and the enemy that this belongs to.
Each Enemy has specific movement patterns or stats that make them unique and difficult in their own way. 

<h5> Enemy Handling Example </h5> 
An example of one of the more interesting enemies is the 'Slime Boss' the first boss enemy to be able to be spawned. The main premise of it was that when shot it would disperse in half seperating itself and slowly become more difficult as faster and smaller orbs floated around. To make this happen there were two scripts created: simebossHandler and slimebossController. The Slime Boss Controller script can be looked at as an overrall parent event handler. Keeping track of when it first spawns and then all other splits, while also notifying other controllers once the boss is completely cleared. 

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
> Excerpt taken from the following [File](https://github.com/willpk03/Rogue-lite-Bullet-Hell-/blob/main/Assets/Scripts/Boss%20Handlers/Slime%20Boss/slimeBossController.cs)

<h3> Future plans</h3>
Current plans with the project is for the following:

-  &nbsp; Change the character design from the current placeholder

-  &nbsp; Improve the current enemy spawn system to include unqiue enemys that are more difficult at X amount of spawns or X amount of time to allow for roadblocks for the player to need to improve there high score.
  
-  &nbsp; Add in game sound.
  
-  &nbsp; Create a pause menu and settings.

-  &nbsp; Allow for better modification of the enemy that are not just speed and health. For example, size, color, if they can shoot bullets at the player or not, direction (are they going towards the player or a set path).
  
-  &nbsp; Add in the ability to change your bullet type in the store.
  

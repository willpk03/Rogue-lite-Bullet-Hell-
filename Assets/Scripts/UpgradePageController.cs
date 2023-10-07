using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePageController : MonoBehaviour
{
    public Text SpeedTxt;
    public Text HealthTxt;
    public Text CreditTxt;
    public int speed;
    public int health;
    public int Credits;
    int defspeed;
    int defhealth;
    int defCredits;
    
    public DataHolder dataHolder;
    // Start is called before the first frame update
    void Start()
    {
        defspeed = speed;
        defhealth = health;
        defCredits = Credits;
        changeTxt();
    }

    //Handles Speed Increasing or Decreasing
    public void increaseSpeed() {
        if(Credits - 3 > -1){
            speed = speed + 1;
            Credits = Credits - 3;
            changeTxt();
        }  
    }
    public void decreaseSpeed() {
        if(defspeed - 1 < speed - 1){
            speed = speed - 1;
            Credits = Credits + 1;
            changeTxt();
        }
    }

    //Handles Health Increasing or Decreasing
    public void increaseHealth() { 
        if(Credits - 5 > -1){ 
            health = health + 1;
            Credits = Credits - 5;
            changeTxt();
        }
    }
    public void decreaseHealth() {
        if(defhealth -1 < health - 1){  
            health = health - 1;
            Credits = Credits - 1;
            changeTxt();
        }
    }

    //Handles Text changes
    public void changeTxt() {
        SpeedTxt.text = speed.ToString();
        HealthTxt.text = health.ToString();
        CreditTxt.text = "Credits: " + Credits.ToString();
        dataHolder.myData.myVariable = health;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePageController : MonoBehaviour
{
    //MyData holds information around what these variables do to effect palyer

    //Variables to display current values
    public Text SpeedTxt;
    public Text HealthTxt;
    public Text reloadCDTxt;
    public Text pierceTxt;
    public Text CreditTxt;

    //Holds current values
    public int speed;
    public double reloadCD;
    public int health;
    public int pierce;
    public int Credits;

    //Default Value/Beginning Value
    int defspeed;
    int defhealth;
    double defreloadCD;
    int defCredits;
    int defpierce;
    
    
    public DataHolder dataHolder;
    // Start is called before the first frame update
    void Start()
    {
        //Stores Default Value. Default Values are set by setting them in ButtonManager in the Script section. 
        Credits = dataHolder.myData.HScredits;
        defspeed = speed;
        defhealth = health;
        defCredits = Credits;
        defreloadCD = reloadCD;
        defpierce = pierce;

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
            Credits = Credits + 3;
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
            Credits = Credits + 5;
            changeTxt();
        }
    }

    //Handles Reload time Increasing or Decreasing
    //Credits are taken away for decreasing not increasing
    public void decreaseReload() { 
        if(Credits - 10 > -1 && reloadCD - .1 >= 0){ 
            reloadCD = reloadCD - .1;
            Credits = Credits - 10;
            changeTxt();
        }
    }
    public void increaseReload() {
        if(defreloadCD + .2 < reloadCD - .2){  
            reloadCD = reloadCD + .1;
            Credits = Credits + 10;
            changeTxt();
        }
    }

     public void increasePierce() { 
        if(Credits - 10 > -1){ 
            pierce = pierce + 1;
            Credits = Credits - 10;
            changeTxt();
        }
    }
    public void decreasePierce() {
        if(defpierce - 1 < pierce - 1){  
            pierce = pierce - 1;
            Credits = Credits + 10;
            changeTxt();
        }
    }

    //Handles Text changes
    public void changeTxt() {
        //Changes Text
        SpeedTxt.text = speed.ToString();
        HealthTxt.text = health.ToString();
        CreditTxt.text = "Credits: " + Credits.ToString();
        reloadCDTxt.text = reloadCD.ToString();
        pierceTxt.text = pierce.ToString();

        //Stores Data for transfer
        dataHolder.myData.health = health;
        dataHolder.myData.reloadCD = reloadCD;
        dataHolder.myData.pierce = pierce;
        dataHolder.myData.speed = defspeed + (speed -defspeed)/4;
        dataHolder.myData.BulletType = "B";
    }

}

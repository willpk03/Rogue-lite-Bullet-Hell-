using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonControllers :  MonoBehaviour {

    public string sceneName;

    public void Scene1() {  
        SceneManager.LoadScene(sceneName);
    }

}
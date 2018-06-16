using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsBehavior : MonoBehaviour {
    	
	void Update () {
        if (InputManager.b_Button(1))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsBehavior : MonoBehaviour {

    public Rigidbody2D CreditText;
    
    public int endPosition;
    public long speed;

	// Use this for initialization
	void Start () {
        CreditText.velocity = new Vector3(0, speed);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (CreditText.position.y > endPosition)
        {
            CreditText.velocity = Vector3.zero;
        }

        if(InputManager.b_Button(1))
        {
            SceneManager.LoadScene("Menu");
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsBehavior : MonoBehaviour {

    public Rigidbody2D CreditRigidbody;
    public long speed;

    private RectTransform CreditTransform;

	// Use this for initialization
	void Start () {
        CreditTransform = CreditRigidbody.gameObject.GetComponent<RectTransform>();

        CreditRigidbody.velocity = new Vector3(0, speed);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (CreditTransform.anchoredPosition.y > CreditTransform.sizeDelta.y && CreditTransform.sizeDelta.y > 100)
        {
            CreditRigidbody.velocity = Vector3.zero;
        }

        if(InputManager.b_Button(1))
        {
            SceneManager.LoadScene("Menu");
        }
	}
}

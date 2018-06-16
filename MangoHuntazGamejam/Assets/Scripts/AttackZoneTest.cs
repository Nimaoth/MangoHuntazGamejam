using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZoneTest : MonoBehaviour {

    public BoxCollider2D attackZone;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    Vector3 getCenter(Vector3 leftBottom, Vector3 rightTop)
    {
        return (leftBottom + rightTop) / 2;
    }

    Vector2 getSize(Vector3 leftBottom, Vector3 rightTop)
    {
       return new Vector2(rightTop.x - leftBottom.x, rightTop.y - leftBottom.y);
    }
}

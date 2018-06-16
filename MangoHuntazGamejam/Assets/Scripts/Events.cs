using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class HasBeenHitEvent : UnityEvent<Move>
{
}


public class Events : MonoBehaviour {

    public void Start()
    {
    }



    //TODO
    public UnityEvent CeddosSupremeGeilerUltraTest = new UnityEvent();

    public HasBeenHitEvent Player1HasBeenHit = new HasBeenHitEvent();

    public UnityEvent Player1Whiffs = new UnityEvent();

}

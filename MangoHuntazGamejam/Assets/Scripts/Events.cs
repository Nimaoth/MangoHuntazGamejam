using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class HasBeenHitEvent : UnityEvent<Move>
{
}

public class PlayerHitEvent : System.EventArgs
{
    public DamageRumble rumbel { get; set; }
}

public class Events : MonoBehaviour {

    public void Start()
    {
    }

    public static event EventHandler<PlayerHitEvent> playerHasBeenHit;

    protected void HitPlayer(DamageRumble damageRumbel)
    {
        if(playerHasBeenHit != null)
        {
            playerHasBeenHit(this, new PlayerHitEvent() { rumbel = damageRumbel });
        }
    }

    //TODO
    public UnityEvent CeddosSupremeGeilerUltraTest = new UnityEvent();

    public HasBeenHitEvent Player1HasBeenHit = new HasBeenHitEvent();

    public Event PlayerWasHit = new Event();

    public UnityEvent Player1Whiffs = new UnityEvent();

}

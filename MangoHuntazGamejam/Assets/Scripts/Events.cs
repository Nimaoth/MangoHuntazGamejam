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

    public Events instance;
    public void Awake()
    {
        instance = this;
    }

    public static event EventHandler<PlayerHitEvent> playerHasBeenHit;

    protected void HitPlayer(DamageRumble damageRumbel)
    {
        if(playerHasBeenHit != null)
        {
            playerHasBeenHit(this, new PlayerHitEvent() { rumbel = damageRumbel });
        }
    }

   // public UnityEvent CeddosSupremeGeilerUltraTest = new UnityEvent();

   // public HasBeenHitEvent Player1HasBeenHit = new HasBeenHitEvent();

    //public UnityEvent PlayerWasHit = new UnityEvent();

    public UnityEvent Clown_Bite_Hit_Event = new UnityEvent();
    public UnityEvent Clown_Bite_Miss_Event = new UnityEvent();
    public UnityEvent Clown_Block_Event = new UnityEvent();
    public UnityEvent Clown_HammerHit_Event = new UnityEvent();
    public UnityEvent Clown_HammerMiss_Event = new UnityEvent();
    public UnityEvent Clown_HammerSwing_Event = new UnityEvent();
    public UnityEvent Clown_Laugh_Heavy_Event = new UnityEvent();
    public UnityEvent Clown_Laugh_Light_Event = new UnityEvent();
    //public UnityEvent Clown_HammerHit_Event = new UnityEvent();

}

using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class RumbleFeedback
{
    public static void beginRumble(int playerIndex, DamageRumble damageRumble)
    {
        if (damageRumble == null || ((GameManager.instance.healthPlayer1 <= 0 || GameManager.instance.healthPlayer2 <= 0) && !damageRumble.name.Equals("Death")))
            return;
        beginRumble(playerIndex, damageRumble.m_higherMotor, damageRumble.m_lowerMotor, damageRumble.m_duration);
    }

    public static void beginRumble(int playerIndex, float low, float high, float duration)
    {
        GamePad.SetVibration((PlayerIndex)(--playerIndex), low, high);
        Timing.RunCoroutine(stopVibrationAfterDuration(playerIndex, duration));
    }

    static IEnumerator<float> stopVibrationAfterDuration(int playerIndex, float duration)
    {
        yield return Timing.WaitForSeconds(duration); 
        GamePad.SetVibration((PlayerIndex)playerIndex, 0f, 0f);
    }
}

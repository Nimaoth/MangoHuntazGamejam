using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private bool animatedExists = false;
    public Player player;
    public Transform healthbarTransform;
    private Vector3 healthbarOrigin;
    [SerializeField]
    private  Image stars;
    [SerializeField]
    private Image beam;
    private float startime = 0.02f;
    private float beamtime;
    [SerializeField]
    private GameObject gold;
    public Transform chargebarTransform;
    private Vector3 chargebarOrigin;

    private void Start()
    {
        healthbarOrigin = healthbarTransform.position;

        chargebarOrigin = chargebarTransform.position;
        if (player.playerId == 2)
        {
            beamtime = 1f;
            startime = 1f;
        }
    }

    public void Update()
    {

        //Update UI
        var leftPlayer = player.playerId == 1;
        var health = leftPlayer ? GameManager.instance.healthPlayer1 : GameManager.instance.healthPlayer2;
        healthbarTransform.position = healthbarOrigin + new Vector3((float)(health - 100) / 100.0f * (leftPlayer ? 4 : -4) * 50.0f, 0);

        var charge = leftPlayer ? GameManager.instance.specialChargeP1 : GameManager.instance.specialChargeP2;
        var currentColor = chargebarTransform.GetComponent<Image>().color;
        Color newColor;
        if (player.playerId == 2)
        {
            newColor = new Color(currentColor.r, currentColor.g, currentColor.b, GameManager.instance.specialP2Active ? 1f : 0f);
        }
        else
        { newColor = new Color(currentColor.r, currentColor.g, currentColor.b, GameManager.instance.specialP1Active ? 1f : 0f); }

        chargebarTransform.position = chargebarOrigin + new Vector3((float)(charge - 100) / 100.0f * (leftPlayer ? 4 : -4) * 50.0f, 0);

        chargebarTransform.GetComponent<Image>().color = newColor;
        if (!animatedExists)
            StartCoroutine("Animate");
    }

    public IEnumerator Animate()
    {
        animatedExists = true;
        while (true)
        {
            if (player.playerId == 1)
            {
                beamtime += 0.01f;
                beamtime %= 1.0f;
                startime += 0.008f;
                startime %= 1.0f;
            }
            else
            {
                beamtime -= 0.01f;
                if (beamtime <= 0)
                    beamtime = 1f;
                startime -= 0.008f;
                if (startime <= 0)
                    startime = 1f;

            }

            beam.material.SetFloat("_GlowPos", beamtime);
            beam.material.SetFloat("_GlowWidth", 0.085f);
            stars.material.SetFloat("_GlowPos", startime);
            if (player.playerId == 1)
            {
                gold.SetActive(GameManager.instance.specialP1Active);
                stars.material.SetFloat("_GlowWidth", GameManager.instance.specialP1Active ? 0.14f : 0.0f);
            }
            else
            {
                gold.SetActive(GameManager.instance.specialP2Active);
                stars.material.SetFloat("_GlowWidth", GameManager.instance.specialP2Active ? 0.14f : 0.0f);
            }
            yield return null;
        }
    }
}

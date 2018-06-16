using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //Singleton
    public static GameManager instance;

    public Player player1;
    public Player player2;

    [SerializeField]
    GameObject Player1Spawn;
    [SerializeField]
    GameObject Player2Spawn;

    private Vector3 P1Pos;
    private Vector3 P2Pos;
    private float Distance;

    private static bool created = false;


    public int MAX_HEALTH;
    public int CHARGE_NECESSARY;
    public int healthPlayer1;
    public int healthPlayer2;
    private int specialChargeP1=0;
    private int specialChargeP2=0;
    private bool specialP1Active;
    private bool specialP2Active;

    //intro
    public int introFadeSteps;
    public float introFadeDuration;
    public Image fightIntroImage;
    public Image fightImage;
    public Image FightEndsImage;
    public Image SKWinsImage;
    public Image ClownWinsImage;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this);
            instance = this;
            created = true;
        }
        else
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start()
    {
        P1Pos = Player1Spawn.transform.position;
        P2Pos = Player2Spawn.transform.position;

        StartFight();
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Mathf.Abs(P2Pos.z - P1Pos.z);
    }


    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void StartFight()
    {
        player1.isControllable = false;
        player2.isControllable = false;

        P1Pos = Player1Spawn.transform.position;
        P2Pos = Player2Spawn.transform.position;
        healthPlayer1 = MAX_HEALTH;
        healthPlayer2 = MAX_HEALTH;

        StartCoroutine("FightIntro");
    }
    
    

    IEnumerator FightIntro()
    {
        yield return new WaitForSeconds(0.5f);
        float delta = introFadeDuration / introFadeSteps;

        int counter = 0;
        fightIntroImage.enabled = true;
        while (counter < introFadeSteps)
        {
            float alpha = (float)counter / introFadeSteps;
            fightIntroImage.color = new Color(fightIntroImage.color.r, fightIntroImage.color.g, fightIntroImage.color.b, alpha);
            counter++;
            yield return new WaitForSeconds(delta);
        }
        yield return new WaitForSeconds(0.25f);
        fightIntroImage.enabled = false;
        player1.isControllable = true;
        player2.isControllable = true;

        //play FightImage
        fightImage.enabled = true;
        yield return new WaitForSeconds(0.5f);
        fightImage.enabled = false;
        

        yield return null;
    }

    public void OnHit(int playerID, int damage, DamageRumble damageRumble)
    {
        if (playerID == 1)
        {
            healthPlayer1 -= damage;

            if (healthPlayer1 <= 0)
            {
                RumbleFeedback.beginRumble(playerID, damageRumble);
                EndGame(2);
                return;
            }
        }
        else
        {
            healthPlayer2 -= damage;
            if (healthPlayer2 <= 0)
            {
                RumbleFeedback.beginRumble(playerID, damageRumble);
                EndGame(1);
                return;
            }
        }
        RumbleFeedback.beginRumble(playerID,damageRumble);
        Debug.Log("p1: " + healthPlayer1 + ", p2: " + healthPlayer2);
    }
    //call from Player.cs when the corresponding player missed a hit/was blocked
    public void OnMiss(int playerID)
    {
        if (playerID == 1)
            specialChargeP2++;
        if (playerID == 2)
            specialChargeP1++;
        if (specialChargeP1 >= CHARGE_NECESSARY)
            specialP1Active = true;
        else
            specialP1Active = false;
        if (specialChargeP2 >= CHARGE_NECESSARY)
            specialP2Active = true;
        else
            specialP2Active = false;
    }

    void EndGame(int winnerID)
    {
        Debug.Log("Player " + winnerID + " wins!!!");
        player1.isControllable = false;
        player2.isControllable = false;
        StartCoroutine(FightOutro(winnerID));
    }

    IEnumerator FightOutro(int winnerID)
    {
        FightEndsImage.enabled = true;
        yield return new WaitForSeconds(0.5f);
        FightEndsImage.enabled = false;
        if(winnerID == 1)
        {
            //SKwins?
            SKWinsImage.enabled = true;
        }
        else
        {
            ClownWinsImage.enabled = true;
        }
        yield return new WaitForSeconds(1.0f);

        SKWinsImage.enabled = false;
        ClownWinsImage.enabled = false;
        NewRound();
    }


    void NewRound()
    {
        //todo
        Debug.Log("newround");
    }
    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);

        //TODO
        if(scene.name == "SampleScene")
        {
            Player1Spawn = GameObject.FindGameObjectWithTag("Player1Spawn");
            Player2Spawn = GameObject.FindGameObjectWithTag("Player2Spawn");
        }
    }

}

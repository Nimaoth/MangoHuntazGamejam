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


    public float MAX_HEALTH;
    public int CHARGE_NECESSARY;
    public float DAMAGE_REDUCTION_ON_BLOCK;
    public float healthPlayer1;
    public float healthPlayer2;
    public float specialChargeP1=0;
    public float specialChargeP2=0;
    public bool specialP1Active;
    public bool specialP2Active;
    public bool damageReductionP1, damageReductionP2;
    

    //intro
    public int introFadeSteps;
    public float introFadeDuration;
    public Image fightIntroImage;
    public Image fightImage;
    public Image FightEndsImage;
    public Image SKWinsImage;
    public Image ClownWinsImage;

    public MusicManager musicManager;

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

        player1.transform.position = P1Pos;
        player2.transform.position = P2Pos;

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
        musicManager.PlayTerror();
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
        musicManager.PlayHaunt();

        if(musicManager.mainLoopSource.isPlaying)
        {
            musicManager.AmpMainLoop();
        }

        fightImage.enabled = true;
        yield return new WaitForSeconds(0.5f);
        fightImage.enabled = false;
        

        yield return null;
    }

    public void OnHit(int playerID, float damage, DamageRumble damageRumble)
    {
        if (playerID == 1)
        {
            healthPlayer1 -= damage;

            if (healthPlayer1 <= 0)
            {
                RumbleFeedback.beginRumble(2, player1.m_deathRumbel);
                EndGame(2);
                return;
            }
        }
        else
        {
            healthPlayer2 -= damage;
            if (healthPlayer2 <= 0)
            {
                RumbleFeedback.beginRumble(1, player1.m_deathRumbel);
                EndGame(1);
                return;
            }
        }
        RumbleFeedback.beginRumble(playerID, damageRumble);
    }
    //call from Player.cs when the corresponding player missed a hit/was blocked
    public void OnMiss(int playerID)
    {
        if (playerID == 1)
        {
            specialChargeP1++;
            specialChargeP1 = Mathf.Clamp(specialChargeP1, 0, 100);
        }


        if (playerID == 2)
        {
            specialChargeP2++;
            specialChargeP2 = Mathf.Clamp(specialChargeP2, 0, 100);
        }
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
        musicManager.DeAmpMainLoop();
        musicManager.PlayNightmare();
        yield return new WaitForSeconds(0.3f);
        FightEndsImage.enabled = true;
        yield return new WaitForSeconds(2.2f);
        FightEndsImage.enabled = false;
        if(winnerID == 1)
        {
            musicManager.PlaySKWins();
            //SKwins?
            SKWinsImage.enabled = true;
        }
        else
        {
            musicManager.PlayClownWins();
            ClownWinsImage.enabled = true;
        }
        yield return new WaitForSeconds(2.5f);

        SKWinsImage.enabled = false;
        ClownWinsImage.enabled = false;
        yield return new WaitForSeconds(0.5f);
        NewRound();
    }


    void NewRound()
    {
        //todo
        StartFight();
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
            musicManager = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();
        }
    }

}

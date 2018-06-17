using UnityEngine;

public class AttackZone : MonoBehaviour
{

    private int enemyPlayerID;
    public Player player;
    private float enemyHealth;
    Player enemy;

    public void Awake()
    {
        if (player.playerId == 1)
        {
            enemyPlayerID = 2;
        }
        else
        {
            enemyPlayerID = 1;
        }
    }
    public void Start()
    {
        if (enemyPlayerID == 2)
        {
            enemy = GameManager.instance.player2;
        }
        else
        {
            enemy = GameManager.instance.player1;
        }
    }

    public void DoOnEnable()
    {
        if (enemyPlayerID == 1)
            enemyHealth = GameManager.instance.healthPlayer1;
        else if (enemyPlayerID == 2)
            enemyHealth = GameManager.instance.healthPlayer2;
    }
    public void DoOnDisable()
    {
        if (enemyPlayerID == 1)
        {
            if (GameManager.instance.healthPlayer1 == enemyHealth)
                GameManager.instance.OnMiss(player.playerId);
        }
        else if (enemyPlayerID == 2)
        {
            if (GameManager.instance.healthPlayer2 == enemyHealth)
                GameManager.instance.OnMiss(player.playerId);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Hit!: " + collision.gameObject.name);
        GetComponent<BoxCollider2D>().enabled = false;
        if (!enemy.currentMove.Equals(enemy.getBlockMove()))
        {
            RumbleFeedback.beginRumble(player.playerId, player.currentMove.m_damageRumble);
            GameManager.instance.OnHit(enemyPlayerID, player.currentMove.damage, player.currentMove.m_damageRumble);
        }
        else
        {
            RumbleFeedback.beginRumble(player.playerId, player.m_lightAttackRumbel);
            GameManager.instance.OnHit(enemyPlayerID, player.currentMove.damage * (1 - GameManager.instance.DAMAGE_REDUCTION_ON_BLOCK), player.m_blockRumbel);
            GameManager.instance.OnMiss(player.playerId);
            Debug.Log("Blocked!");

            if (enemyPlayerID == 1)
            {
                player.musicManager.PlaySound("SK_Block", enemy.transform.position);
            }
            if (enemyPlayerID == 2)
            {
                player.musicManager.PlaySound("Clown_Block", enemy.transform.position);
            }
        }
    }
}

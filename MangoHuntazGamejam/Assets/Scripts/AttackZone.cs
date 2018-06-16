using UnityEngine;

public class AttackZone : MonoBehaviour {

    private int enemyPlayerID;
    public Player player;

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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Hit!: " + collision.gameObject.name);
        GetComponent<BoxCollider2D>().enabled = false;
        GameManager.instance.OnHit(enemyPlayerID, player.currentMove.damage);
    }
}

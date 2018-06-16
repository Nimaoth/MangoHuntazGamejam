using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    public int playerId;

    public DynamicFloat m_hitBar;

    public Move currentMove;
    private Move idleMove;
    private Move blockMove;
    private Move staggerMove;

    private int currentFrame = 0;
    private int transitionTime = -1;
    private Move nextMove = null;

    private bool isStunned = false;

    public BoxCollider2D attackZone;

    private bool attackZoneActivated = false;

    private new Rigidbody2D rigidbody;

    public bool isControllable = false;


    void Awake()
    {
        animator = transform.GetComponentInChildren<Animator>();
        rigidbody = transform.GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        blockMove = new Move("Block", 30, 29, 5, 30, null, null, null, Vector2.zero, Vector2.zero, int.MaxValue, -1);


        var lightAttack3 = new Move("LightAttack3", 30, 15, 10, 30, null, null, blockMove, new Vector2(1, 1), new Vector2(2, 3), 10, 30) { damage = 3 };
        var lightAttack2 = new Move("LightAttack2", 20, 10, 5, 20, lightAttack3, null, blockMove, new Vector2(1, 1), new Vector2(1, 2), 10, 30) { damage = 2 };
        var lightAttack1 = new Move("LightAttack1", 20, 10, 5, 20, lightAttack2, null, blockMove, new Vector2(1, 1), new Vector2(0.5f, 1), 0, 20) { damage = 1 };

        lightAttack1.displacementStart = 1;
        lightAttack1.displacementEnd = 3;
        lightAttack1.displacement = 1.0f;

        lightAttack2.displacementStart = 1;
        lightAttack2.displacementEnd = 3;
        lightAttack2.displacement = 1.0f;

        lightAttack3.displacementStart = 1;
        lightAttack3.displacementEnd = 4;
        lightAttack3.displacement = 1.25f;

        staggerMove = new Move("Stagger", 15, 14, 10, 15, lightAttack1, null, blockMove, Vector2.zero, Vector2.zero, int.MaxValue, -1);

        idleMove = new Move("idle", -1, -1, -1, -1, lightAttack1, null, blockMove, Vector2.zero, Vector2.zero, int.MaxValue, -1);
        idleMove.loop = true;

        currentMove = idleMove;
    }

    void FixedUpdate()
    {
        currentFrame++;

        if (currentMove.displacement != 0 && currentFrame >= currentMove.displacementStart && currentFrame <= currentMove.displacementEnd)
        {
            var displacementDur = currentMove.displacementEnd - currentMove.displacementStart;
            var dis = currentMove.displacement / displacementDur;
            if (playerId == 2)
                dis = -dis;
            //transform.Translate(new Vector3(dis, 0));
            rigidbody.MovePosition(rigidbody.position + new Vector2(dis, 0));
        }

        if(!attackZoneActivated)
        {
            if((currentFrame >= currentMove.attackZoneStart))
            {
                //activate Hitbox
                attackZone.size = currentMove.attackZoneSize;
                attackZone.offset = currentMove.attackZoneCenter;
                attackZone.enabled = true;
                attackZoneActivated = true;
            }
        }
        else
        {
            if (currentFrame >= currentMove.attackZoneEnd)
            {
                attackZone.enabled = false;
            }
        }

    }

    private void OnAttack(Move next)
    {
        if (next == null)
            return;

        if (isStunned)
            return;
        
        if (!currentMove.loop && (currentFrame < currentMove.inputTimeStart || currentFrame > currentMove.inputTimeEnd))
            return;

        if (currentFrame > currentMove.cancelTime)
            transitionTime = currentFrame;
        else
            transitionTime = currentMove.cancelTime;

        nextMove = next;
    }

    private void SetIdle()
    {
        currentMove = idleMove;
        transitionTime = -1;
        nextMove = null;
        currentFrame = 0;
    }

    private void Stagger(int duration)
    {
        SetIdle();
        staggerMove.duration = duration;
        currentMove = staggerMove;

        animator.SetTrigger(staggerMove.name);
    }



    private bool CanMove()
    {
        if (!isControllable)
            return false;
        if (currentMove == idleMove)
            return true;

        if (currentMove.cancelTime >= 0 && currentFrame >= currentMove.cancelTime)
            return true;

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove())
        {
            var pos = new Vector2();
            pos.x = InputManager.horizontal(playerId);
            if (pos.x < 0)
                pos.x = -1;
            else if (pos.x > 0)
                pos.x = 1;
            
            rigidbody.MovePosition(rigidbody.position + pos * (Time.deltaTime * 20));

            var dir = pos.x > 0 ? 1 : (pos.x < 0 ? -1 : 0);
            if (playerId == 2)
                dir = -dir;
            
            animator.SetInteger("Direction", dir);
        }
        
        if (InputManager.x_Button_down(playerId))
            OnAttack(currentMove.onLightAttack);
        if (InputManager.b_Button_down(playerId))
            OnAttack(currentMove.onHeavyAttack);
        if (InputManager.rb_Button_down(playerId))
            OnAttack(currentMove.onBlock);

        //Debug TODO
        if (InputManager.y_Button_down(playerId))
            Stagger(30);


        if (currentFrame >= transitionTime && transitionTime >= 0)
        {
            animator.SetTrigger(nextMove.name);

            transitionTime = -1;
            currentMove = nextMove;        
            nextMove = null;
            currentFrame = 0;
            attackZoneActivated = false;
            attackZone.enabled = false;
        }

        if (currentMove.duration >= 0 && currentFrame > currentMove.duration)
            SetIdle();
    }

}

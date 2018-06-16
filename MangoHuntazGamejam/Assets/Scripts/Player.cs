﻿using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    public int playerId;

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

    public Transform healthbarTransform;
    private Vector3 healthbarOrigin;


    void Awake()
    {
        animator = transform.GetComponentInChildren<Animator>();
        rigidbody = transform.GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        blockMove = new Move("Block", 30, 29, 5, 30, null, null, null, Vector2.zero, Vector2.zero, int.MaxValue, -1, null);

        var heavyAttackBuildUp = new Move("HeavyAttack", 20, 15, 10, 20, null, null, blockMove, new Vector2(1.5f, 0.5f), new Vector2(3, 2.5f), 10, 15, null) { damage = 5 };
        var heavyAttackHit = new Move("HeavyAttack", 25, 20, 15, 25, null, null, blockMove);

        var lightAttack3 = new Move("LightAttack3", 30, 15, 10, 30, null, null, blockMove, new Vector2(1.5f, 0.75f), new Vector2(1.5f, 1), 20, 35, null) { damage = 3 };
        var lightAttack2 = new Move("LightAttack2", 20, 10, 5, 20, lightAttack3, heavyAttackHit, blockMove, new Vector2(1.75f, 0.75f), new Vector2(1.25f, 2.5f), 0, 15, null) { damage = 2 };
        var lightAttack1 = new Move("LightAttack1", 20, 10, 5, 20, lightAttack2, null, blockMove, new Vector2(1.5f, 0.5f), new Vector2(1, 2), 0, 15, null) { damage = 1 };
        
        lightAttack1.displacementStart = 1;
        lightAttack1.displacementEnd = 3;
        lightAttack1.displacement = 1.0f;

        lightAttack2.displacementStart = 1;
        lightAttack2.displacementEnd = 3;
        lightAttack2.displacement = 1.0f;

        lightAttack3.displacementStart = 1;
        lightAttack3.displacementEnd = 4;
        lightAttack3.displacement = 1.25f;

        staggerMove = new Move("Stagger", 15, 14, 10, 15, lightAttack1, null, blockMove, Vector2.zero, Vector2.zero, int.MaxValue, -1, null);

        idleMove = new Move("Idle", -1, -1, -1, -1, lightAttack1, heavyAttackBuildUp, blockMove, Vector2.zero, Vector2.zero, int.MaxValue, -1, null);
        idleMove.loop = true;

        lightAttack1.onNothing = idleMove;
        lightAttack2.onNothing = idleMove;
        lightAttack3.onNothing = idleMove;
        heavyAttackHit.onNothing = idleMove;
        heavyAttackBuildUp.onNothing = heavyAttackHit;

        currentMove = idleMove;

        healthbarOrigin = healthbarTransform.position;
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

    //private void SetIdle()
    //{
    //    animator.SetInteger("Direction", 0);
    //    animator.SetTrigger("Idle");

    //    transitionTime = -1;
    //    currentMove = idleMove;
    //    nextMove = null;
    //    currentFrame = 0;
    //}

    private void NextMove(Move m)
    {
        animator.SetTrigger(m.name);

        transitionTime = -1;
        currentMove = m;
        nextMove = null;
        currentFrame = 0;
        attackZoneActivated = false;
        attackZone.enabled = false;
    }

    private void Stagger(int duration)
    {
        //SetIdle();    
        NextMove(idleMove);
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
            if (dir != 0 && currentMove != idleMove)
                NextMove(idleMove);
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
        {
            //SetIdle();
            var next = nextMove;
            if (nextMove == null)
                next = idleMove;
            NextMove(next);
        }
        
        //Update UI
        var leftPlayer = playerId == 1;
        var health = leftPlayer ? GameManager.instance.healthPlayer1 : GameManager.instance.healthPlayer2;

        healthbarTransform.position = healthbarOrigin + new Vector3((float)(health - 100) / 100.0f * (leftPlayer ? 4 : -4), 0);
    }
}

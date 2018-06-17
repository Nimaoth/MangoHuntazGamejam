﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    public DamageRumble m_deathRumbel;
    public DamageRumble m_lightAttackRumbel;
    public DamageRumble m_strongAttackRumbel;
    public DamageRumble m_blockRumbel;

    void Awake()
    {
        animator = transform.GetComponentInChildren<Animator>();
        rigidbody = transform.GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        blockMove = new Move("Block", 30, 25, 5, 30, null, null, null, Vector2.zero, Vector2.zero, int.MaxValue, -1, m_blockRumbel);

        var heavyAttackShort = new Move("HeavyAttackShort", 21, 17, 15, 1000, null, null, blockMove,
            new Vector2(1.5f, 0.5f), new Vector2(3, 2.5f),
            4, 13, m_strongAttackRumbel)
        { damage = 5 };

        var heavyAttackLong = new Move("HeavyAttackLong", 32, 26, 20, 1000, null, null, blockMove,
            new Vector2(1.5f, 0.5f), new Vector2(3, 2.5f),
            14, 23, m_strongAttackRumbel)
        { damage = 5 };

        var lightAttack3 = new Move("LightAttack3", 15, 8, 5, 1000, null, null, blockMove,
            new Vector2(1.5f, 0.75f), new Vector2(1.5f, 1),
            5, 15, m_strongAttackRumbel)
        { damage = 3 };

        var lightAttack2 = new Move("LightAttack2", 15, 7, 5, 1000, lightAttack3, heavyAttackShort, blockMove,
            new Vector2(1.75f, 0.75f), new Vector2(1.25f, 2.5f),
            3, 7, m_lightAttackRumbel)
        { damage = 2 };

        var lightAttack1 = new Move("LightAttack1", 15, 7, 5, 1000, lightAttack2, null, blockMove,
            new Vector2(1.5f, 0.5f), new Vector2(1, 2),
            3, 7, m_lightAttackRumbel)
        { damage = 1 };

        lightAttack1.displacementStart = 1;
        lightAttack1.displacementEnd = 3;
        lightAttack1.displacement = 0.5f;

        lightAttack2.displacementStart = 1;
        lightAttack2.displacementEnd = 3;
        lightAttack2.displacement = 0.5f;

        lightAttack3.displacementStart = 1;
        lightAttack3.displacementEnd = 4;
        lightAttack3.displacement = 0.75f;

        staggerMove = new Move("Stagger", 15, 14, 0, 1000, lightAttack1, heavyAttackShort, blockMove, Vector2.zero, Vector2.zero, int.MaxValue, -1, m_blockRumbel);

        idleMove = new Move("Idle", -1, -1, -1, -1, lightAttack1, heavyAttackLong, blockMove, Vector2.zero, Vector2.zero, int.MaxValue, -1, null);
        idleMove.loop = true;

        currentMove = idleMove;
    }

    void FixedUpdate()
    {
        rigidbody.velocity = Vector2.zero;

        currentFrame++;

        if (currentMove.displacement != 0 && currentFrame >= currentMove.displacementStart && currentFrame <= currentMove.displacementEnd)
        {
            var displacementDur = currentMove.displacementEnd - currentMove.displacementStart;
            var dis = currentMove.displacement / displacementDur;
            if (playerId == 2)
                dis = -dis;
            //rigidbody.MovePosition(rigidbody.position + new Vector2(dis, 0));
        }

        if (!attackZoneActivated)
        {
            if ((currentFrame >= currentMove.attackZoneStart))
            {
                //activate Hitbox
                attackZone.size = currentMove.attackZoneSize;
                attackZone.offset = playerId == 1 ? currentMove.attackZoneCenter : -1 * currentMove.attackZoneCenter;
                attackZone.enabled = true;
                attackZoneActivated = true;
                attackZone.gameObject.GetComponent<AttackZone>().DoOnEnable();
            }
        }
        else
        {
            if (currentFrame >= currentMove.attackZoneEnd)
            {
                attackZone.enabled = false;
                attackZone.gameObject.GetComponent<AttackZone>().DoOnDisable();
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
        {
            transitionTime = currentMove.cancelTime;
        }

        nextMove = next;
    }

    private void NextMove(Move m)
    {
        if (m == null)
            m = idleMove;

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
        staggerMove.duration = duration;
        staggerMove.cancelTime = duration;
        NextMove(staggerMove);
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
            NextMove(nextMove);
        }

        if (currentMove.duration >= 0 && currentFrame > currentMove.duration)
        {
            var next = nextMove;
            if (next == null)
                next = idleMove;
            NextMove(next);
        }
    }
    public Move getBlockMove()
    {
        return blockMove;
    }
}

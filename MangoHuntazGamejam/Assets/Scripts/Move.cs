using UnityEngine;

public class Move {

    public string name;
    public int duration;

    public int cancelTime;
    public int inputTimeStart;
    public int inputTimeEnd;

    public Move onLightAttack;
    public Move onHeavyAttack;
    public Move onBlock;

    public int stunDuration;

    public bool loop = false;

    public bool trigger = true;

    public int damage = 0;
    public float knockback = 0;

    public int displacementStart = 0;
    public int displacementEnd = 0;
    public float displacement = 0;

    public Vector2 attackZoneCenter;
    public Vector2 attackZoneSize;
    public int attackZoneStart;
    public int attackZoneEnd;


    public int blockStart;
    public int blockEnd;
    public float blockFactor;

    public Move(string name, int duration, int cancelTime, int inputTimeStart, int inputTimeEnd, Move onLightAttack, Move onHeavyAttack, Move block, Vector2 attackZoneCenter, Vector2 attackZoneSize, int attackZoneStart, int attackZoneEnd)
    {
        this.name = name;
        this.duration = duration;
        this.cancelTime = cancelTime;
        this.inputTimeStart = inputTimeStart;
        this.inputTimeEnd = inputTimeEnd;
        this.onLightAttack = onLightAttack;
        this.onHeavyAttack = onHeavyAttack;
        this.onBlock = block;
        this.attackZoneCenter = attackZoneCenter;
        this.attackZoneSize = attackZoneSize;
        this.attackZoneStart = attackZoneStart;
        this.attackZoneEnd = attackZoneEnd;
    }
}

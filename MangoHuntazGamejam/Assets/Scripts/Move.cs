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
    public Move onNothing;

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
    public int attackZoneStart = -1;
    public int attackZoneEnd = -1;


    public int blockStart;
    public int blockEnd;
    public float blockFactor;

    public DamageRumble damageRumble;

    public string soundName;
    public string missName;

    public Move(string name, int duration, int cancelTime, int inputTimeStart, int inputTimeEnd, Move onLightAttack, Move onHeavyAttack, Move onBlock, Vector2 attackZoneCenter, Vector2 attackZoneSize, int attackZoneStart, int attackZoneEnd, DamageRumble damageRumble)
    {
        this.name = name;
        this.duration = duration;
        this.cancelTime = cancelTime;
        this.inputTimeStart = inputTimeStart;
        this.inputTimeEnd = inputTimeEnd;
        this.onLightAttack = onLightAttack;
        this.onHeavyAttack = onHeavyAttack;
        this.onBlock = onBlock;
        this.attackZoneCenter = attackZoneCenter;
        this.attackZoneSize = attackZoneSize;
        this.attackZoneStart = attackZoneStart;
        this.attackZoneEnd = attackZoneEnd;
        this.damageRumble = damageRumble;
    }

    public override string ToString()
    {
        return name;
    }
}

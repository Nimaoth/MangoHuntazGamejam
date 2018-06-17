using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCreator {

    public static void CreateMovesForClown(ref Move firstLightAttack, ref Move firstHeavyAttack, Move blockMove, DamageRumble strongRumble, DamageRumble lightRumble)
    {
        var heavyAttackShort = new Move("HeavyAttackShort", 21, 17, 15, 1000, null, null, blockMove,
            new Vector2(1.5f, 0.5f), new Vector2(3, 2.5f),
            4, 13, strongRumble)
        { damage = 5, soundName = "Clown_Hammer_Hit", missName = "Clown_Hammer_Miss" };

        var heavyAttackLong = new Move("HeavyAttackLong", 32, 26, 20, 1000, null, null, blockMove,
            new Vector2(1.5f, 0.5f), new Vector2(3, 2.5f),
            14, 23, strongRumble)
        { damage = 5, soundName = "Clown_Hammer_Hit", missName = "Clown_Hammer_Miss" };

        var lightAttack3 = new Move("LightAttack3", 18, 14, 5, 1000, null, null, blockMove,
            new Vector2(1.5f, 0.75f), new Vector2(1.5f, 1),
            9, 14, strongRumble)
        { damage = 3, soundName = "Clown_Bite_Hit", missName = "Clown_Bite_Miss" };

        var lightAttack2 = new Move("LightAttack2", 18, 14, 5, 1000, lightAttack3, heavyAttackShort, blockMove,
            new Vector2(1.75f, 0.75f), new Vector2(1.25f, 2.5f),
            9, 14, lightRumble)
        { damage = 2, soundName = "Clown_Bite_Hit", missName = "Clown_Bite_Miss" };

        var lightAttack1 = new Move("LightAttack1", 18, 14, 5, 1000, lightAttack2, null, blockMove,
            new Vector2(1.5f, 0.5f), new Vector2(1, 2),
            9, 14, lightRumble)
        { damage = 1, soundName = "Clown_Bite_Hit", missName = "Clown_Bite_Miss" };

        lightAttack1.displacementStart = 1;
        lightAttack1.displacementEnd = 3;
        lightAttack1.displacement = 0.5f;

        lightAttack2.displacementStart = 1;
        lightAttack2.displacementEnd = 3;
        lightAttack2.displacement = 0.5f;

        lightAttack3.displacementStart = 1;
        lightAttack3.displacementEnd = 4;
        lightAttack3.displacement = 0.75f;

        firstLightAttack = lightAttack1;
        firstHeavyAttack = heavyAttackLong;
    }

    public static void CreateMovesForSerialkiller(ref Move firstLightAttack, ref Move firstHeavyAttack, Move blockMove, DamageRumble strongRumble, DamageRumble lightRumble)
    {
        var heavyAttackShort = new Move("HeavyAttackShort", 21, 17, 15, 1000, null, null, blockMove,
            new Vector2(1.5f, 0.5f), new Vector2(3, 2.5f),
            4, 13, strongRumble)
        { damage = 5, soundName = "SK_Scythe_Hit", missName = "SK_Scythe_Miss" };

        var heavyAttackLong = new Move("HeavyAttackLong", 32, 26, 20, 1000, null, null, blockMove,
            new Vector2(1.5f, 0.5f), new Vector2(3, 2.5f),
            14, 23, strongRumble)
        { damage = 5, soundName = "SK_Scythe_Hit", missName = "SK_Scythe_Miss" };

        var lightAttack3 = new Move("LightAttack3", 15, 8, 5, 1000, null, null, blockMove,
            new Vector2(1.5f, 0.75f), new Vector2(1.5f, 1),
            5, 15, strongRumble)
        { damage = 3, soundName = "SK_Machete_Hit", missName = "SK_Machete_Miss" };

        var lightAttack2 = new Move("LightAttack2", 15, 7, 5, 1000, lightAttack3, heavyAttackShort, blockMove,
            new Vector2(1.75f, 0.75f), new Vector2(1.25f, 2.5f),
            3, 7, lightRumble)
        { damage = 2, soundName = "SK_Machete_Hit", missName = "SK_Machete_Miss" };

        var lightAttack1 = new Move("LightAttack1", 15, 7, 5, 1000, lightAttack2, null, blockMove,
            new Vector2(1.5f, 0.5f), new Vector2(1, 2),
            3, 7, lightRumble)
        { damage = 1, soundName = "SK_Machete_Hit", missName = "SK_Machete_Miss" };

        lightAttack1.displacementStart = 1;
        lightAttack1.displacementEnd = 3;
        lightAttack1.displacement = 0.5f;

        lightAttack2.displacementStart = 1;
        lightAttack2.displacementEnd = 3;
        lightAttack2.displacement = 0.5f;

        lightAttack3.displacementStart = 1;
        lightAttack3.displacementEnd = 4;
        lightAttack3.displacement = 0.75f;

        firstLightAttack = lightAttack1;
        firstHeavyAttack = heavyAttackLong;
    }

}

using UnityEngine;

public class MoveCreator {

    public static void CreateMovesForClown(ref Move firstLightAttack, ref Move firstHeavyAttack, Move blockMove, DamageRumble strongRumble, DamageRumble lightRumble)
    {
        var hammerRumble = new DamageRumble
        {
            m_duration = 0.35f,
            m_higherMotor = 1,
            m_lowerMotor = 1
        };

        var heavyAttackShort = new Move("HeavyAttackShort", 36, 26, 20, 1000, null, null, blockMove,
            new Vector2(2.5f, 0.0f), new Vector2(3.25f, 3.75f), 21, 27, hammerRumble)
        { damage = 6, soundName = "Clown_Hammer_Hit", missName = "Clown_Hammer_Swing", stunDuration = 10, knockback = 20 };

        var heavyAttackLong = new Move("HeavyAttackLong", 36, 26, 20, 1000, null, null, blockMove,
            new Vector2(2.5f, 0.0f), new Vector2(3.25f, 3.75f), 21, 27, hammerRumble)
        { damage = 6, soundName = "Clown_Hammer_Hit", missName = "Clown_Hammer_Swing", stunDuration = 10, knockback = 20 };

        var lightAttack3 = new Move("LightAttack3", 21, 14, 5, 1000, null, null, blockMove,
            new Vector2(1.0f, -0.8f), new Vector2(2, 1), 9, 14, strongRumble)
        { damage = 3, soundName = "Clown_Bite_Hit", missName = "Clown_Bite_Miss", stunDuration = 2, displacementStart = 1, displacementEnd = 4, displacement = 0.75f };

        var lightAttack2 = new Move("LightAttack2", 21, 14, 5, 1000, lightAttack3, heavyAttackShort, blockMove,
            new Vector2(1.0f, -0.8f), new Vector2(2, 1), 9, 14, lightRumble)
        { damage = 2, soundName = "Clown_Bite_Hit", missName = "Clown_Bite_Miss", stunDuration = 2, displacementStart = 1, displacementEnd = 3, displacement = 0.5f };

        var lightAttack1 = new Move("LightAttack1", 21, 14, 5, 1000, lightAttack2, null, blockMove,
            new Vector2(1.0f, -0.8f), new Vector2(2, 1), 9, 14, lightRumble)
        { damage = 1, soundName = "Clown_Bite_Hit", missName = "Clown_Bite_Miss", stunDuration = 2, displacementStart = 1, displacementEnd = 3, displacement = 0.5f };
        
        firstLightAttack = lightAttack1;
        firstHeavyAttack = heavyAttackLong;
    }

    public static void CreateMovesForSerialkiller(ref Move firstLightAttack, ref Move firstHeavyAttack, Move blockMove, DamageRumble strongRumble, DamageRumble lightRumble)
    {
        var heavyAttackShort = new Move("HeavyAttackShort", 21, 17, 15, 1000, null, null, blockMove,
            new Vector2(1.5f, 0.5f), new Vector2(3, 2.5f), 4, 13, strongRumble)
        { damage = 5, soundName = "SK_Scythe_Hit", missName = "SK_Scythe_Miss", stunDuration = 10, knockback = 50 };

        var heavyAttackLong = new Move("HeavyAttackLong", 32, 26, 20, 1000, null, null, blockMove,
            new Vector2(1.5f, 0.5f), new Vector2(3, 2.5f), 14, 23, strongRumble)
        { damage = 5, soundName = "SK_Scythe_Hit", missName = "SK_Scythe_Miss", stunDuration = 10, knockback = 50 };

        var lightAttack3 = new Move("LightAttack3", 15, 12, 5, 1000, null, null, blockMove,
            new Vector2(1.5f, 0.75f), new Vector2(1.5f, 1), 5, 15, strongRumble)
        { damage = 3, soundName = "SK_Machete_Hit", missName = "SK_Machete_Miss", stunDuration = 0, knockback = 50, displacementStart = 1, displacementEnd = 5, displacement = 1f };

        var lightAttack2 = new Move("LightAttack2", 15, 16, 5, 1000, lightAttack3, heavyAttackShort, blockMove,
            new Vector2(1.75f, 0.75f), new Vector2(1.25f, 2.5f), 3, 7, lightRumble)
        { damage = 2, soundName = "SK_Machete_Hit", missName = "SK_Machete_Miss", stunDuration = 0, knockback = 10, displacementStart = 1, displacementEnd = 2, displacement = 0.25f };

        var lightAttack1 = new Move("LightAttack1", 15, 16, 5, 1000, lightAttack2, null, blockMove,
            new Vector2(1.5f, 0.5f), new Vector2(1, 2), 3, 7, lightRumble)
        { damage = 1, soundName = "SK_Machete_Hit", missName = "SK_Machete_Miss", stunDuration = 0, knockback = 10, };
        
        firstLightAttack = lightAttack1;
        firstHeavyAttack = heavyAttackLong;
    }

}

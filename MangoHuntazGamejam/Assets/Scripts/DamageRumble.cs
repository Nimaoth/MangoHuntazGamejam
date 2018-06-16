using UnityEngine;

[CreateAssetMenu(fileName = "DamageRumble", menuName = "Data/DamageRumble", order = 1)]
public class DamageRumble : ScriptableObject
{
    public float m_higherMotor;
    public float m_lowerMotor;
    public float m_duration;
} 

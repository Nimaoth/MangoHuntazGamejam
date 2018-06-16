using UnityEngine;

[CreateAssetMenu(fileName = "DamageRumble", menuName = "Data/DamageRumble", order = 1)]
public class DamageRumble : ScriptableObject
{
    public float m_leftMotor;
    public float m_rightMotor;
    public float m_duration;
} 

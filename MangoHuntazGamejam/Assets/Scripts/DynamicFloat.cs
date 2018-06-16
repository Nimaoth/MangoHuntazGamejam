using UnityEngine;

[CreateAssetMenu(fileName = "DynamicFloat", menuName = "Data/DynamicFloat", order = 1)]
public class DynamicFloat : ScriptableObject
{
    public float m_initialFloat;
    public float m_dynamicFloat;

    private void OnEnable()
    {
        m_dynamicFloat = m_initialFloat;
    }
}

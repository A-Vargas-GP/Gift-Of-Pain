using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyDamageScriptableObject", order = 1)]
public class EnemyDamageScriptableObject : ScriptableObject
{
    public string currentObjectName;
    public int damageValue;
}

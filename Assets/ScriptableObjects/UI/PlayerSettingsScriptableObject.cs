using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerSettingsScriptableObject", order = 1)]
public class PlayerSettingsScriptableObject : ScriptableObject
{
    public bool invertCamera;
    public bool enemySpawning;
}

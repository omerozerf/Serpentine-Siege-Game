using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelUpPowerUpSO))]
public class LevelUpPowerUpSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LevelUpPowerUpSO powerUp = (LevelUpPowerUpSO)target;

        // Default inspector fields
        EditorGUILayout.LabelField("Power-Up Info", EditorStyles.boldLabel);
        powerUp._powerUpHeader = EditorGUILayout.TextField("Header", powerUp._powerUpHeader);
        powerUp._powerUpDescription = EditorGUILayout.TextField("Description", powerUp._powerUpDescription);
        powerUp._powerUpSprite = (Sprite)EditorGUILayout.ObjectField("Sprite", powerUp._powerUpSprite, typeof(Sprite), false);
        powerUp._powerUpType = (LevelUpPowerUpType)EditorGUILayout.EnumPopup("Power-Up Type", powerUp._powerUpType);

        // Draw specific fields based on power-up type
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Power-Up Settings", EditorStyles.boldLabel);

        switch (powerUp.GetPowerUpType())
        {
            case LevelUpPowerUpType.FireRate:
                powerUp._fireRatePercentage = EditorGUILayout.FloatField("Fire Rate Percentage", powerUp._fireRatePercentage);
                break;
            case LevelUpPowerUpType.Damage:
                powerUp._damageAmount = EditorGUILayout.IntField("Damage Amount", powerUp._damageAmount);
                break;
            case LevelUpPowerUpType.MovementSpeed:
                powerUp._movementSpeedPercentage = EditorGUILayout.FloatField("Movement Speed Percentage", powerUp._movementSpeedPercentage);
                break;
            case LevelUpPowerUpType.EnemySpeed:
                powerUp._enemySpeedPercentage = EditorGUILayout.FloatField("Enemy Speed Percentage", powerUp._enemySpeedPercentage);
                break;
            case LevelUpPowerUpType.SoldierCount:
                powerUp._soldierCount = EditorGUILayout.IntField("Soldier Count", powerUp._soldierCount);
                break;
        }

        // Save the changes back to the object
        if (GUI.changed)
        {
            EditorUtility.SetDirty(powerUp);
        }
    }
}

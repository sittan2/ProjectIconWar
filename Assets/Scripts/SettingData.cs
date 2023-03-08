using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingData", menuName = "ICW/SettingData")]
public class SettingData : ScriptableObject
{
    [Header("Team Color")]
    public Color noneColor;
    public Color playerColor;
    public Color enemyColor;
}
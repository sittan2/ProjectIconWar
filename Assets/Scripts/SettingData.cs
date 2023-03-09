using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingData", menuName = "ICW/SettingData")]
public class SettingData : ScriptableObject
{
    [Header("Team Color")]
    public List<TeamColor> TeamColors = new List<TeamColor>();
}

[System.Serializable]
public struct TeamColor
{
    [SerializeField] ETeam _team;
    [SerializeField] Color _baseColor;
    [SerializeField] Color _backColor;
    [SerializeField] Color _backColor2;

    public ETeam Team => _team;
    public Color BaseColor => _baseColor;
    public Color BackColor => _backColor;
    public Color BackColor2 => _backColor2;
}
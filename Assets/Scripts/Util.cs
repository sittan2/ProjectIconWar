using System.Collections;
using UnityEngine;

public class Util
{
    public static Color GetColor(ETeam team)
    {
        switch (team)
        {
            default:
            case ETeam.None:
                return Managers.Setting.noneColor;

            case ETeam.Player:
                return Managers.Setting.playerColor;

            case ETeam.Enemy:
                return Managers.Setting.enemyColor;
        }
    }
}
using System.Collections;
using UnityEngine;

public class Util
{
    public static Color GetColor(ETeam team)
    {
        return Managers.Setting.TeamColors.Find(x => x.Team == team).BaseColor;
    }

    public static TeamColor GetTeamColor(ETeam team)
    {
        return Managers.Setting.TeamColors.Find(x => x.Team == team);
    }
}
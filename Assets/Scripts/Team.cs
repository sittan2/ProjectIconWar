using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Team : MonoBehaviour
{
    ETeam team;
    List<UnitBase> _bases = new List<UnitBase>();

    private void Start()
    {
        _bases = FindObjectsOfType<UnitBase>().ToList().FindAll(x => x.team == team);
    }
}
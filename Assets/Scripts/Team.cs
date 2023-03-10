using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Team : MonoBehaviour
{
    ETeam team;
    List<Building> _bases = new List<Building>();

    private void Start()
    {
        _bases = FindObjectsOfType<Building>().ToList().FindAll(x => x.team == team);
    }
}
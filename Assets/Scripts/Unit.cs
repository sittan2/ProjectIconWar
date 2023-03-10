using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public ETeam team = ETeam.None;
    public List<Sight> sights = new List<Sight>();

    protected virtual void Start()
    {
        Init();
    }

    public abstract void Init();
    protected abstract void SetColor();
    protected virtual void SetSight()
    {
        sights = GetComponentsInChildren<Sight>(true).ToList();
        sights.ForEach(sight => sight.Init(this));
    }
}
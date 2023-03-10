using System.Collections;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public void Init(Unit unit)
    {
        if (unit.team == ETeam.Player)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
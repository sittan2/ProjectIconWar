using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public ETeam currentControl;

    public void Init()
    {
        currentControl = ETeam.Player;
    }

    public void Clear()
    {

    }
}
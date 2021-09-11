using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passive_AI : Enemy
{
    public void AI()
    {
        Debug.Log(this.name + " takes its turn.");
        GameManager.GAME.EnemyTurnEnd();
    }
}

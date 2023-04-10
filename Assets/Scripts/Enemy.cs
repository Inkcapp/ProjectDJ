using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Battler
{
    public override void HandleTurn()
    {
        Attack(getRandomBattler(battle.party));
    }
}

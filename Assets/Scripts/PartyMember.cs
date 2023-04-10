using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : Battler {
    // Start is called before the first frame update
    public override void Update() {
        if (isTurn && turnUnhandled) {
            HandleTurn();
            
        }
        else if (!isTurn) {
            turnUnhandled = true;
        }
    }

    public override void HandleTurn()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Attack(getRandomBattler(battle.enemies));
            turnUnhandled = false;
        }

        
    }
}

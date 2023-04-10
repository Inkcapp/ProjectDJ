using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public static BattleManager GetInstance() {
        return instance;
    }
    public void Awake() {
        if (instance != null) {
            Destroy(instance);
        }
        instance = this;
    }
    [Header("DEBUG - TROOPS")]
    public Troop partyTroop;
    public Troop enemyTroop;

    [Header("Battler Lists")]
    public List<Battler> party;
    public List<Battler> enemies;

    public List<Battler> turnOrder;
    [Header("Positioning")]
    public Transform[] playerPositions;
    public Transform[] enemyPositions;

    void Start()    
    {
        InitializeBattle();
    }

    public void InitializeBattle() {
        InstantiateBattlers();

        //Initialize and Sort all combatants into turn order
        turnOrder = party.Concat(enemies).ToList();
        turnOrder.Sort();
    }

    public void InstantiateBattlers() {
        for(int i = 0; i < partyTroop.battlers.Length; i++) {
            /*if(partyTroop.battlers[i].GetComponent<Battler>() != null) {
                GameObject battlerGO = Instantiate(partyTroop.battlers[i]);
                battlerGO.transform.position = playerPositions[i].position;
                party.Add(battlerGO.GetComponent<Battler>());
            }*/

            InstantiateBattler(partyTroop.battlers[i], playerPositions[i], party);
        }

        for(int i = 0; i < enemyTroop.battlers.Length; i++) {
            /*if(enemyTroop.battlers[i].GetComponent<Battler>() != null) {
                GameObject battlerGO = Instantiate(enemyTroop.battlers[i]);
                battlerGO.transform.position = enemyPositions[i].position;
                enemies.Add(battlerGO.GetComponent<Battler>());
            }*/

            InstantiateBattler(enemyTroop.battlers[i], enemyPositions[i], enemies);
        }
    }

    void InstantiateBattler(GameObject go, Transform trans, List<Battler> side) {
        if(go.GetComponent<Battler>() == null) return;

        GameObject battlerGO = Instantiate(go);
        battlerGO.transform.position = trans.position;
        side.Add(battlerGO.GetComponent<Battler>());
        battlerGO.name = ("n " + UnityEngine.Random.Range(1, 9));
    }

    // Update is called once per frame
    void Update()
    {       
        while (turnOrder[0] == null) {
            DeleteFromLists(turnOrder[0]);
        }

        if (turnOrder[0].isTurn == false) turnOrder[0].isTurn = true;
    }

    public void EndTurn() {
        if (turnOrder[0].isTurn == true) turnOrder[0].isTurn = false;
        CycleTurns();
    }

    void CycleTurns() {
        Battler lastBattler = turnOrder[0];
        turnOrder.RemoveAt(0);
        turnOrder.Add(lastBattler);
    }

    public void DeleteFromLists(Battler battler) {
        if(party.Contains(battler)) party.Remove(battler);
        if(enemies.Contains(battler)) enemies.Remove(battler);
        if(turnOrder.Contains(battler)) turnOrder.Remove(battler);
    }
}

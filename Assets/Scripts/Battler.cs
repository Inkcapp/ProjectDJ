using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Battler : MonoBehaviour, IComparable
{
    [Header("References")]
    [SerializeField]
    private Healthbar healthbar;
    [SerializeField]
    private Animator anim;
    private AnimationSwitcher aSwitch;
    [Header("Data")]

    [SerializeField]
    public Stats stats;
    public Vector3 frontOffset;
    public float healthbarHeight;
    [HideInInspector]
    public float health;
    [HideInInspector]
    public BattleManager battle;
    [HideInInspector]
    public bool isTurn = false;
    [HideInInspector]
    public bool turnUnhandled = true;
    [HideInInspector]
    public Vector3 home;
    [Header("Animations")]
    public string idleAnimation;
    public string attackAnimation;
    public string approachAnimation;
    public string retreatAnimation;

    
    public virtual void Start() {
        Initialize();
    }

    public void Initialize() {
        battle = BattleManager.GetInstance();
        aSwitch = gameObject.AddComponent<AnimationSwitcher>() as AnimationSwitcher;

        isTurn = false;
        home = transform.position;
        health = stats.maxHP;

        RefreshHealthbar();

        SetAnimation(idleAnimation);
    }

    public virtual void Update() {
        if (isTurn && turnUnhandled) {
            HandleTurn();
            turnUnhandled = false;
        }
        else if (!isTurn) {
            turnUnhandled = true;
        }
    }

    public virtual void HandleTurn() {
        Attack(getRandomBattler(battle.turnOrder));
    }

    public void Attack(Battler target) {
        SetAnimation(approachAnimation);

        transform.DOMove(target.transform.position - frontOffset, 1)
            .OnComplete(() => {
                target.Damage(stats);
                if (anim != null) {
                    a2();
                } else {
                    Return();
                }
                
                }
            );
    }

    private void a2() {
        aSwitch.ChangeAnimationState(attackAnimation, true, Return);
    }

    public void Return() {
        SetAnimation(retreatAnimation);
        transform.DOMove(home, 1)
            .OnComplete(battle.EndTurn);
            SetAnimation(idleAnimation);
    }

    public int CompareTo(System.Object obj){
        var a = this;
        var b = obj as Battler;
     
        if (a.stats.speed < b.stats.speed)
            return -1;
     
        if (a.stats.speed > b.stats.speed)
            return 1;
 
        return 0;
    }

    public Battler getRandomBattler(List<Battler> battlerList) {
        return battlerList[UnityEngine.Random.Range(0, battlerList.Count)];
    }

    public void Damage(Stats s) {
        Damage(((float)s.power * 2) - (float)stats.moxie);
    }

    public void Damage(float damage) {
        health -= damage;
        RefreshHealthbar();

        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        battle.DeleteFromLists(this);
        Destroy(gameObject);
    }

    private void RefreshHealthbar() {
        if (healthbar != null) healthbar.Refresh(stats.maxHP, health);
    }

    private void SetAnimation(string animation) {
        if (anim == null) return;

        aSwitch.anim = anim;
        aSwitch.ChangeAnimationState(animation);
    }
}

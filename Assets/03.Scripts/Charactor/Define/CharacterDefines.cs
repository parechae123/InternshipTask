using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackModule
{
    private float attackRange;
    private float attackDelay;
    private float currAttackDelay;
    private float damage;
    public abstract void OnAttack();
    public abstract bool CheckAttackDelay();
}
public class ProjectileAttackModule : AttackModule
{
    public override bool CheckAttackDelay()
    {
        throw new System.NotImplementedException();
    }

    public override void OnAttack()
    {
        throw new System.NotImplementedException();
    }
}


public enum AttackModuleType
{
    projectile,melee,summon
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackModule
{
    protected float attackRange;
    public float GetAttackRange { get { return attackRange; } }
    protected float attackDelay;
    protected float currAttackDelay;
    protected float damage;
    public abstract void OnAttack();
    public bool CheckAttackDelay()
    {
        currAttackDelay += Time.deltaTime;
        if (attackDelay <= currAttackDelay)
        {
            return true;
        }
        return false;
    }
    public static AttackModule Factory(ParsingData.UnitData data)
    {
        switch (data.attackmodule)
        {
            case AttackModuleType.projectile:
                return new ProjectileAttackModule(data);
            case AttackModuleType.melee:
                return new MeleeAttackModule(data);
            case AttackModuleType.summon:
                return new SummonAttackModule(data);
            default:
                return null;
        }
    }
}
public class ProjectileAttackModule : AttackModule
{
    public ProjectileAttackModule(ParsingData.UnitData data)
    {
        attackRange = data.attackrange;
        attackDelay = data.attackdelay;
        currAttackDelay = 0f;
        damage = data.damage;

    }
    public override void OnAttack()
    {
        
    }
}
public class MeleeAttackModule : AttackModule
{

    public MeleeAttackModule(ParsingData.UnitData data)
    {
        attackRange = data.attackrange;
        attackDelay = data.attackdelay;
        currAttackDelay = 0f;
        damage = data.damage;

    }
    public override void OnAttack()
    {
        
    }
}
public class SummonAttackModule : AttackModule
{
    private float summonduration;
    private float tickdelay;
    public SummonAttackModule(ParsingData.UnitData data)
    {
        attackRange = data.attackrange;
        attackDelay = data.attackdelay;
        currAttackDelay = 0f;
        damage = data.damage;
        summonduration = data.summonduration;
        tickdelay = data.tickdelay;
    }
    public override void OnAttack()
    {
        
    }
}


public enum AttackModuleType
{
    projectile,melee,summon
}
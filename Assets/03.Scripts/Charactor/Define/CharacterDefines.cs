using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackModule
{
    protected float attackSEDRange;
    protected float attackRange;
    protected float attackDelay;
    protected float currAttackDelay;
    protected float damage;
    protected Transform tr;
    protected Collider2D targetCollider;
    public abstract void OnAttack();
    public virtual void ReadyAttack()
    {
        if (CheckAttackDelay())
        {
            GetTarget();
            if (targetCollider != null)
            {
                currAttackDelay = 0f;
                OnAttack();
            }
        }
    }
    protected virtual void GetTarget()
    {
        if (targetCollider != null)
        {
            //»ç¸ÁÃ¼Å© ¹× °Å¸®Ã¼Å©
            if (!targetCollider.gameObject.activeSelf || GetSED(tr.position, targetCollider.transform.position) > attackSEDRange)
            {
                targetCollider = null;
            }
            else
            {
                return;
            }
        }
        targetCollider = Physics2D.OverlapCircle(tr.position, attackRange,1<<3);
    }

    /// <summary>
    /// Squared Euclidean Distance
    /// </summary>
    /// <param name="a">Å¸°Ù a ÀÇ position</param>
    /// <param name="b">Å¸°Ù b ÀÇ position</param>
    protected float GetSED(Vector2 a, Vector2 b)
    {
        float relX = a.x - b.x;
        float relY = a.y - b.y;
        return (relX * relX) + (relY * relY);
    }
    public bool CheckAttackDelay()
    {
        currAttackDelay += Time.deltaTime;
        if (attackDelay <= currAttackDelay)
        {
            return true;
        }
        return false;
    }
    public static AttackModule Factory(ParsingData.UnitData data,Transform tr)
    {
        switch (data.attackmodule)
        {
            case AttackModuleType.projectile:
                return new ProjectileAttackModule<TowerProjectile>(data,tr);
            case AttackModuleType.melee:
                return new MeleeAttackModule(data,tr);
            case AttackModuleType.summon:
                return new SummonAttackModule(data,tr);
            case AttackModuleType.penetrateProjectile:
                return new ProjectileAttackModule<PenetrateProjectile>(data, tr);
            default:
                return null;
        }
    }
}
public class ProjectileAttackModule<T> : AttackModule where T : TowerProjectile
{
    public static ResourceManaging.Pool<T> projPool;
    public ProjectileAttackModule(ParsingData.UnitData data,Transform tr)
    {
        if (projPool == null) projPool=new ResourceManaging.Pool<T>(typeof(T).Name);
        attackSEDRange = data.attackrange* data.attackrange;
        attackRange = data.attackrange;
        attackDelay = data.attackdelay;
        currAttackDelay = 0f;
        damage = data.damage;
        this.tr = tr;
    }
    public override void OnAttack()
    {
        projPool.DeQueue().Init(targetCollider, tr.position, damage);
    }
}
public class MeleeAttackModule : AttackModule
{
    public static ResourceManaging.Pool<MeleeSpriteEffect> effectPool;
    public MeleeAttackModule(ParsingData.UnitData data,Transform tr)
    {
        if(effectPool == null) effectPool = new ResourceManaging.Pool<MeleeSpriteEffect>("MeleeDamageEffect");
        attackSEDRange = data.attackrange * data.attackrange;
        attackRange = data.attackrange;
        attackDelay = data.attackdelay;
        currAttackDelay = 0f;
        damage = data.damage;
        this.tr = tr;
    }
    public override void OnAttack()
    {
        effectPool.DeQueue().Init(targetCollider.transform.position);
        GameManager.GetInstance.AttackEnemy(targetCollider, damage);
    }
}
public class SummonAttackModule : AttackModule
{
    private float summonduration;
    private float tickdelay;
    public static ResourceManaging.Pool<SummonOBJ> summonPool;
    public SummonAttackModule(ParsingData.UnitData data,Transform tr)
    {
        attackSEDRange = data.attackrange * data.attackrange;
        attackRange = data.attackrange;
        attackDelay = data.attackdelay;
        currAttackDelay = 0f;
        if(summonPool == null) summonPool = new ResourceManaging.Pool<SummonOBJ>("StormEffect");
        damage = data.damage;
        summonduration = data.summonduration;
        tickdelay = data.tickdelay;
        this.tr = tr;
    }
    public override void OnAttack()
    {
        Vector2 relPos = targetCollider.transform.position - tr.position;
        
        summonPool.DeQueue().Init(targetCollider.transform.position,summonduration,damage,tickdelay, (Mathf.Rad2Deg * Mathf.Atan2(relPos.y, relPos.x)+90f));
    }
}


public enum AttackModuleType
{
    projectile,melee,summon,penetrateProjectile
}
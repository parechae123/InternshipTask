using UnityEngine;
using System.Collections.Generic;
public class PenetrateProjectile : TowerProjectile
{
    HashSet<Collider2D> attackedCol = new HashSet<Collider2D>();
    public override void Init(Collider2D target, Vector3 shooterPos, float damage)
    {
        transform.position = shooterPos;
        relPos = target.transform.position - shooterPos;
        relPos = relPos.normalized;
        goalTime = 5f;
        this.damage = damage;
        currTime = 0f;
        attackedCol.Clear();
    }
    private void Update()
    {
        float currDelta = Time.deltaTime;
        currTime += currDelta;
        transform.position += (relPos * bulletSpeed) * currDelta;

        Collider2D[] tempColl = Physics2D.OverlapCircleAll(transform.position, 0.5f, 1 << 3);

        if (tempColl != null && tempColl.Length >= 0)
        {
            for (int i = 0; i < tempColl.Length; i++)
            {
                if (attackedCol.Contains(tempColl[i]))
                {
                    continue;
                }
                else
                {
                    GameManager.GetInstance.AttackEnemy(tempColl[i], damage);
                    attackedCol.Add(tempColl[i]);
                }
            }
        }

        if (currTime >= goalTime)
        {

            ProjectileAttackModule<PenetrateProjectile>.projPool.EnQueue(this);
        }
    }
}
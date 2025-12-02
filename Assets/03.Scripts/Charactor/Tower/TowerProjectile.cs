using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    protected float damage;
    protected Vector3 relPos;
    protected float currTime;
    protected float goalTime = 100f;
    protected float bulletSpeed = 10f;
    private Collider2D targetCol;
    public virtual void Init(Collider2D target, Vector3 shooterPos, float damage)
    {
        transform.position = shooterPos;
        targetCol = target;
        relPos = target.transform.position - shooterPos;
        relPos = relPos.normalized;
        goalTime = 100f;//update에서 최신화
        this.damage = damage;
        currTime = 0f;
    }
    private void Update()
    {
        float currDelta = Time.deltaTime;
        currTime += currDelta;
        if (targetCol != null)
        {
            if (targetCol.gameObject.activeSelf)
            {
                relPos = (targetCol.transform.position - transform.position).normalized;//방향 틀어져도 추적
                goalTime = Vector2.Distance(targetCol.transform.position, transform.position) / (bulletSpeed*currDelta);
            }
            else
            {
                targetCol = null;
                goalTime = 0f;
            }
        }
        transform.position += (relPos * bulletSpeed)* currDelta;
        if (currTime>= goalTime)
        {
            if (targetCol != null)
            {
                GameManager.GetInstance.AttackEnemy(targetCol, damage);
            }
            else
            {
                Collider2D tempColl = Physics2D.OverlapCircle(transform.position, 0.3f,1<<3);
                if (tempColl)
                {
                    GameManager.GetInstance.AttackEnemy(tempColl, damage);
                }
                else
                {
                    if(currTime >= 20f) ProjectileAttackModule<TowerProjectile>.projPool.EnQueue(this);
                    return;
                }

            }
            ProjectileAttackModule<TowerProjectile>.projPool.EnQueue(this);
        }
    }
}

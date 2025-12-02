using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BossEntity : EnemyEntity
{
    public override void Init(Vector3[] wayPoints)
    {
        //시간 = 거리/속도
        transform.parent = null;
        transform.position = wayPoints[0];
        gameObject.SetActive(true);
        this.wayPoints = new Queue<Vector3>(wayPoints);
        GameManager.GetInstance.RegistEnemy(col, OnDamaged);
        maxHP = 1000 + (GameManager.GetInstance.currRound * 100);
        currHP = maxHP;
        SetNextPoint();
    }
    protected override void OnDie()
    {
        BossWaveTurnState.pool.EnQueue(this);
        GameManager.GetInstance.CurrGold += 100;
        GameManager.GetInstance.ReleaseEnemy(col, OnDamaged);

    }
    protected override void OnDestroy()
    {
        GameManager.GetInstance.ReleaseEnemy(col, OnDamaged);
    }
}

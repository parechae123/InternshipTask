using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BountyEnemyEntity : EnemyEntity
{
    BountyPopManaging managing;
    ushort benefit;
    bool isGold;
    public void Init(Vector3[] wayPoints,BountyData data, BountyPopManaging managing,MonsterHPBar hpBar)
    {
        if (managing == null) this.managing = managing;
        //시간 = 거리/속도
        transform.parent = null;
        transform.position = wayPoints[0];


        this.isGold = data.isGold;
        this.benefit = data.benefit;
        atk = data.damage;
        this.managing = managing;
        this.maxHP = data.maxHP;

        sr.color = data.color;

        currHP = maxHP;
        gameObject.SetActive(true);
        this.wayPoints = new Queue<Vector3>(wayPoints);
        GameManager.GetInstance.RegistEnemy(col, OnDamaged);

        this.hpBar = hpBar;
        hpBar.Init(transform);
        hpBar.SetMaxHP(maxHP);

        SetNextPoint();
    }


    protected override void SetNextPoint()
    {
        if (wayPoints.Count == 0)
        {
            GameManager.GetInstance.atkCommender?.Invoke(atk);
            transform.position = Vector3.one * 100; ;
            transform.DOKill(false);
            sr.DOKill(true);
            sr.color = Color.black;
            managing.pool.EnQueue(this);
            GameManager.GetInstance.ReleaseEnemy(col, OnDamaged);
        }
        if (wayPoints.TryDequeue(out Vector3 next))
        {
            transform.DOMove(next, Vector3.Distance(next, transform.position) / moveSpeed).OnComplete(() => { SetNextPoint(); }).SetEase(Ease.Linear);
        }
    }
    protected override void OnDie()
    {
        transform.position = Vector3.one * 100; ; 
        managing.pool.EnQueue(this);
        transform.DOKill(false);
        sr.DOKill(true);
        sr.color = Color.black;
        hpBar.Release();
        GameManager.GetInstance.ReleaseEnemy(col, OnDamaged);
        if (isGold)
        {
            GameManager.GetInstance.CurrGold += benefit;
        }
        else
        {
            GameManager.GetInstance.CurrMineral += benefit;

        }
    }
}
[System.Serializable]
public class BountyData
{
    public float maxHP;
    public bool isGold;
    public ushort benefit;
    public float damage;
    public Color color;
}
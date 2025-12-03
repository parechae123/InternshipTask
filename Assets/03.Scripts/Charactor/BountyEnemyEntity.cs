using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BountyEnemyEntity : EnemyEntity
{
    protected float atk = 10f;
    protected float atkDelay = 3f;
    protected float currTimer = 0f;

    protected float moveSpeed = 2f;
    protected float maxHP = 100;
    protected float currHP;
    protected Queue<Vector3> wayPoints;
    [SerializeField] protected SpriteRenderer sr;
    [SerializeField] protected Collider2D col;
    private bool arrive = false;
    public virtual void Init(Vector3[] wayPoints)
    {
        //시간 = 거리/속도
        transform.parent = null;
        transform.position = wayPoints[0];

        atk = 10f + GameManager.GetInstance.currRound;
        maxHP = 300 + (10f * GameManager.GetInstance.currRound);
        currTimer = 0f;

        currHP = maxHP;
        gameObject.SetActive(true);
        arrive = false;
        this.wayPoints = new Queue<Vector3>(wayPoints);
        GameManager.GetInstance.RegistEnemy(col, OnDamaged);

        SetNextPoint();
    }

    public void LateUpdate()
    {
        if (arrive)
        {
            currTimer += Time.deltaTime;
            if (currTimer >= atkDelay)
            {
                currTimer -= atkDelay;//프레임으로 인한 DPS손실방지
                GameManager.GetInstance.atkCommender?.Invoke(atk);
            }
        }
    }

    protected void SetNextPoint()
    {
        if (wayPoints.Count == 0) arrive = true;
        if (wayPoints.TryDequeue(out Vector3 next))
        {
            transform.DOMove(next, Vector3.Distance(next, transform.position) / moveSpeed).OnComplete(() => { SetNextPoint(); }).SetEase(Ease.Linear);
        }
    }
    public void OnDamaged(float dmg)
    {
        currHP -= dmg;
        if (currHP <= 0)
        {
            OnDie();
            return;
        }
        sr.DOKill(true);
        sr.DOColor(Color.red, 0.05f).OnComplete(()=> sr.color = Color.black);
    }
    protected virtual void OnDie()
    {
        transform.position = Vector3.one * 100;
        arrive = false;
        EnemyWaveTurnState.enemyPool.EnQueue(this);
        transform.DOKill(false);
        sr.DOKill(true);
        sr.color = Color.black;
        GameManager.GetInstance.ReleaseEnemy(col, OnDamaged);
        GameManager.GetInstance.CurrGold += 1;
    }
    protected virtual void OnDestroy()
    {
        GameManager.GetInstance.ReleaseEnemy(col, OnDamaged);
        transform.DOKill(false);
        sr.DOKill(false);
    }
}

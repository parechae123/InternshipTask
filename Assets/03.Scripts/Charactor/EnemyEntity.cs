using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyEntity : MonoBehaviour
{
    float moveSpeed = 10f;
    Queue<Vector3> wayPoints;
    public void Init(Vector3[] wayPoints)
    {
        //시간 = 거리/속도
        this.wayPoints = new Queue<Vector3>(wayPoints);
        SetNextPoint();
    }
    private void SetNextPoint()
    {
        if (wayPoints.TryDequeue(out Vector3 next))
        {
            transform.DOMove(next, Vector3.Distance(next, transform.position) / moveSpeed).OnComplete(() => { SetNextPoint(); });
        }
    }
}

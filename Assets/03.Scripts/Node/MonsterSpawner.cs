using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Vector3[] waypoints;
    bool isLoadDone =false;
    public ResourceManaging.Pool<MonsterHPBar> hpPool;
    private void Awake()
    {
        GameManager.GetInstance.spawner = this;
    }
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => ResourceManager.GetInstance.loadDone && GameManager.GetInstance.turnStateMachine != null);
        hpPool = new ResourceManaging.Pool<MonsterHPBar>("MonsterHPBar");
        isLoadDone = true;
    }
    public void SpawnBoss()
    {
        BossWaveTurnState.pool.DeQueue().Init(waypoints,hpPool.DeQueue());
    }
    public void SpawnBountyEnemy(ResourceManaging.Pool<BountyEnemyEntity> pool, BountyData data,BountyPopManaging bm)
    {
        pool.DeQueue().Init(waypoints, data, bm, hpPool.DeQueue());
    }
    public void SpawnEnemy()
    {
        EnemyWaveTurnState.enemyPool.DeQueue().Init(waypoints, hpPool.DeQueue());
    }
    private void LateUpdate()
    {
        if (isLoadDone)
        {
            GameManager.GetInstance.turnStateMachine.OnExecute();
        }
    }
    public void OnDrawGizmos()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.DrawCube(waypoints[i], Vector3.one);
        }
    }
}

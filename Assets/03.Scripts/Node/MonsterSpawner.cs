using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Vector3[] waypoints;

    bool isLoadDone =false;
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => ResourceManager.GetInstance.loadDone && GameManager.GetInstance.turnStateMachine != null);
        GameManager.GetInstance.spawner = this;
        isLoadDone = true;
    }
    public void Spawn()
    {
        GameManager.GetInstance.enemyPool.DeQueue().Init(waypoints);
    }
    private void Update()
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

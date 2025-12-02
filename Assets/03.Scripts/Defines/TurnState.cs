using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public enum TurnType
{
    waitWave, spawnWave, bossWave
}
public class WaitTurnState : IState<TurnType>
{
    public StateMachine<TurnType> stateMachine { get; set; }

    float goalTime = 3f;
    float currTime = 0f;
    TurnType IState<TurnType>.GetType { get { return TurnType.spawnWave; } }
    bool ProgressEnter = true;

    public WaitTurnState()
    {
        

    }
    public void Enter()
    {
        currTime = 0f;
        GameManager.GetInstance.currRound += 1;
        UIManager.GetInstance.roundText.text = $"라운드 : {GameManager.GetInstance.currRound}\n쉬는 시간";
        ProgressEnter = false;
    }

    public void Execute()
    {
        if (ProgressEnter == true) return;
        currTime += Time.deltaTime; 
        UIManager.GetInstance.timerText.text = (goalTime - currTime).ToString("00");

        if (goalTime <= currTime)
        {
            stateMachine.ChageState(TurnType.spawnWave);
        }
    }

    public void Exit()
    {
        ProgressEnter = true;
    }
}
public class EnemyWaveTurnState : IState<TurnType>
{
    public static ResourceManaging.Pool<EnemyEntity> enemyPool;
    public StateMachine<TurnType> stateMachine { get; set; }
    float goalTime = 3f;
    float currTime = 0f;
    float spawnDelay = 0.5f;
    float currSpawn = 0f;
    bool ProgressEnter = true;
    TurnType IState<TurnType>.GetType { get { return TurnType.spawnWave; } }
    public EnemyWaveTurnState()
    {
        if (enemyPool == null)enemyPool = new ResourceManaging.Pool<EnemyEntity>("EnemyEntity");

    }
    public void Enter()
    {
        if(GameManager.GetInstance.currRound%10 == 0)
        {
            stateMachine.ChageState(TurnType.bossWave);
            return;
        }
        currTime = 0f;
        currSpawn = 0f;

        UIManager.GetInstance.roundText.text = $"라운드 : {GameManager.GetInstance.currRound}\n몬스터 웨이브";
        ProgressEnter = false;
    }

    public void Execute()
    {
        if (ProgressEnter == true) return;

        currTime += Time.deltaTime;
        currSpawn += Time.deltaTime;

        UIManager.GetInstance.timerText.text = (goalTime-currTime).ToString("00");
        if (goalTime <= currTime)
        {
            stateMachine.ChageState(TurnType.waitWave);
        }
        if (spawnDelay <= currSpawn)
        {
            currSpawn -= spawnDelay;
            GameManager.GetInstance.spawner.SpawnEnemy();
        }
    }

    public void Exit()
    {
        ProgressEnter = true;

    }
}
public class BossWaveTurnState : IState<TurnType>
{
    public StateMachine<TurnType> stateMachine { get; set; }
    TurnType IState<TurnType>.GetType { get { return TurnType.bossWave; } }

    bool ProgressEnter = true;

    public static ResourceManaging.Pool<BossEntity> pool;
    public BossWaveTurnState()
    {
        if (pool == null) pool = new ResourceManaging.Pool<BossEntity>("BossEntity");

    }
    public void Enter()
    {
        //TODO 보스몬스터
        GameManager.GetInstance.spawner.SpawnBoss();
        UIManager.GetInstance.roundText.text = $"라운드 : {GameManager.GetInstance.currRound}\n보스 웨이브";
        ProgressEnter = false;
        UIManager.GetInstance.timerText.text = string.Empty;
    }

    public void Execute()
    {
        if (ProgressEnter == true) return;
        if (pool.GetCount() != 0)
        {
            stateMachine.ChageState(TurnType.waitWave);
        }
    }

    public void Exit()
    {
        ProgressEnter = true;
    }
}
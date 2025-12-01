using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public enum TurnType
{
    waitWave, spawnWave
}
public class WaitTurnState : IState<TurnType>
{
    public StateMachine<TurnType> stateMachine { get; set; }

    float goalTime = 3f;
    float currTime = 0f;
    TurnType IState<TurnType>.GetType { get { return TurnType.spawnWave; } }
    public WaitTurnState(StateMachine<TurnType> stateMachine)
    {
        

    }
    public void Enter()
    {
        currTime = 0f;
    }

    public void Execute()
    {
        currTime += Time.deltaTime;
        if (goalTime <= currTime)
        {
            Exit();
            stateMachine.ChageState(TurnType.spawnWave);
        }
    }

    public void Exit()
    {
        
    }
}
public class SpawnWaveTurnState : IState<TurnType>
{
    public StateMachine<TurnType> stateMachine { get; set; }
    float goalTime = 30f;
    float currTime = 0f;
    float spawnDelay = 1f;
    float currSpawn = 0f;
    TurnType IState<TurnType>.GetType { get { return TurnType.spawnWave; } }
    public SpawnWaveTurnState(StateMachine<TurnType> stateMachine)
    {
        

    }
    public void Enter()
    {
        currTime = 0f;
        currSpawn = 0f;
    }

    public void Execute()
    {
        currTime += Time.deltaTime;
        currSpawn += Time.deltaTime;
        if (goalTime <= currTime)
        {
            Exit();
            stateMachine.ChageState(TurnType.waitWave);
        }
        if (spawnDelay <= currSpawn)
        {
            currSpawn -= spawnDelay;
            GameManager.GetInstance.spawner.Spawn();
        }
    }

    public void Exit()
    {
        
    }
}
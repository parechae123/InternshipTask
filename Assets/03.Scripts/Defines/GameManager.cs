using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using ParsingData;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
public class GameManager : SingleTon<GameManager>
{
    Dictionary<string, List<TowerBase>> towers;
    public NodeBase selectedNode;
    
    public LevelData[] totalLevelData;
    public LevelData currLevel;


    public StateMachine<TurnType> turnStateMachine;
    public MonsterSpawner spawner;

    public Dictionary<string,UnitData> totalUnit;
    public Dictionary<CharacterGrade, UnitData[]> gradeDict;
    public ResourceManaging.Pool<TowerEntity> towerPool;
    public ResourceManaging.Pool<EnemyEntity> enemyPool;
    public ushort currMineral = 10000;
    public ushort currGold = 10000;
    public ushort fixPrice = 10;


    protected override void Init()
    {
        towers = new Dictionary<string, List<TowerBase>>();
        LoadWait().Forget();
    }

    async UniTaskVoid LoadWait()
    {
        await UniTask.WaitUntil(() => ResourceManager.GetInstance.loadDone);
        totalLevelData = JsonConvert.DeserializeObject<LevelData[]>(((TextAsset)ResourceManager.GetInstance.preLoaded["LevelData"]).text);
        currLevel = totalLevelData[0];
        UnitData[] tempData = JsonConvert.DeserializeObject<UnitData[]>(((TextAsset)ResourceManager.GetInstance.preLoaded["UnitData"]).text);
        totalUnit = new Dictionary<string, UnitData>();
        turnStateMachine = new StateMachine<TurnType>
            (new (TurnType,IState<TurnType>)[]{(TurnType.waitWave,new WaitTurnState(null)), (TurnType.spawnWave, new SpawnWaveTurnState(null)) },TurnType.waitWave);
        towerPool = new ResourceManaging.Pool<TowerEntity>("TowerEntity");
        enemyPool = new ResourceManaging.Pool<EnemyEntity>("EnemyEntity");
        for (int i = 0; i < tempData.Length; i++)
        {
            totalUnit.Add(tempData[i].grade + tempData[i].dataname, tempData[i]);
        }

        gradeDict = new Dictionary<CharacterGrade, UnitData[]>();
        for (int i = 0; i < Enum.GetValues(typeof(CharacterGrade)).Length; i++)
        {
            CharacterGrade currGrade = (CharacterGrade)i;
            UnitData[] currData = tempData.Where((x) => x.grade == currGrade).ToArray();
            gradeDict.Add((CharacterGrade)i, currData);
        }

    }

    public override void Reset()
    {
        towers.Clear();
        currMineral = 10000;
        currGold = 10000;
        fixPrice = 10;
    }
    public void RegistTower(TowerBase tower)
    {
        string key = tower.GetCodeName;
        if (towers.ContainsKey(key))
        {
            towers[key].Add(tower);
        }
        else
        {
            towers.Add(key, new List<TowerBase>() {tower });
        }
    }
    public void RemoveTower(TowerBase tower)
    {
        if (tower == null) return;
        string key = tower.GetCodeName;
        if (towers.ContainsKey(key))
        {
            towers[key].Remove(tower);
            tower.TowerDelete();
            if (towers[key] != null &&towers[key].Count <= 0) towers[key].Clear();//불필요한 더블링 제거용
        }
        else
        {
            Debug.LogError($"제거에 실패하였습니다. TowerName : {key}");
        }
    }
    public void SummonTower(NodeBase node,CharacterGrade grade)
    {
        if (gradeDict.TryGetValue(grade,out UnitData[] result))
        {
            node.SummonTowerOnTile(result[UnityEngine.Random.Range(0,result.Length)]);

        }
    }
    //데이터시트가 배열형이면 While문으로 CharacterGrade의 int값을 매핑해서 리펙토링 해도 될듯
    public CharacterGrade GetSummonGrade()
    {
        float rate = UnityEngine.Random.Range(0f, 100f);
        if (rate <= currLevel.common)
        {
            return CharacterGrade.common;
        }
        rate -= currLevel.common;
        if (rate <= currLevel.unCommon)
        {
            return CharacterGrade.unCommon;
        }
        rate -= currLevel.unCommon;
        if (rate <= currLevel.rare)
        {
            return CharacterGrade.rare;
        }
        return CharacterGrade.superRare;
    }
    public void UpGradeTower(NodeBase target)
    {
        string key = target.Builded.GetCodeName;
        TowerBase tempTower = SearchDuplicateTower(target.Builded);
        if (tempTower != null )
        {
            RemoveTower(tempTower);
            SummonTower(target, target.Builded.grade + 1);
            return;
        }
    }
    public TowerBase SearchDuplicateTower(TowerBase tower)
    {
        if(towers.TryGetValue(tower.GetCodeName, out List<TowerBase> result))
        {
            foreach (TowerBase item in result)
            {
                if (item == tower)
                {
                    continue;
                }
                else
                {
                    return item;
                }
            }
        }
        return null;
    }
}

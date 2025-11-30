using System.Collections;
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
    public Dictionary<string,UnitData> totalUnit;
    protected override void Init()
    {
        towers = new Dictionary<string, List<TowerBase>>();
        LoadWait().Forget();
    }

    async UniTaskVoid LoadWait()
    {
        await UniTask.WaitUntil(() => ResourceManager.GetInstance.loadDone);
        totalLevelData = JsonConvert.DeserializeObject<LevelData[]>(((TextAsset)ResourceManager.GetInstance.preLoaded["LevelData"]).text);
        UnitData[] tempData = JsonConvert.DeserializeObject<UnitData[]>(((TextAsset)ResourceManager.GetInstance.preLoaded["UnitData"]).text);
        totalUnit = new Dictionary<string, UnitData>();
        foreach (UnitData item in tempData)
        {
            totalUnit.Add(item.grade + item.dataname, item);
        }
    }

    public override void Reset()
    {
        towers.Clear();
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
        string key = tower.GetCodeName;
        if (towers.TryGetValue(key,out List<TowerBase> towerList))
        {
            towerList.Remove(tower);
            tower.TowerDelete();
            if (towerList.Count <= 0) towerList.Clear();//불필요한 더블링 제거용
        }
        else
        {
            Debug.LogError($"제거에 실패하였습니다. TowerName : {key}");
        }
    }
    public void SummonTower(NodeBase node,CharactorGrade grade)
    {
        node.SummonTowerOnTile(new TowerBase());
    }
    public void UpGradeTower(NodeBase target)
    {
        string key = target.Builded.GetCodeName;
        if (towers.TryGetValue(key,out List<TowerBase> towerList))
        {
            foreach (TowerBase item in towerList)
            {
                if (item == target.Builded)
                {
                    continue;
                }
                else
                {
                    RemoveTower(item);
                    SummonTower(target, target.Builded.grade + 1);
                    return;
                }
            }
        }
    }
}

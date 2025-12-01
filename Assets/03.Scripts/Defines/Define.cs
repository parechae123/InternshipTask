using UnityEngine;
using System.Collections.Generic;
using System;
namespace Singleton
{
    public class SingleTon<T> where T : SingleTon<T>, new()
    {
        private static T instance;
        public static T GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                    instance.Init();
                    //YOON : 추후 씬 전환 이벤트 등록 Reset함수
                }
                return instance;
            }
        }
        /// <summary>
        /// 초기세팅
        /// </summary>
        protected virtual void Init()
        {

        }
        /// <summary>
        /// 씬 전환시 초기화될것들
        /// </summary>
        public virtual void Reset()
        {

        }
    }
    public class MonoSingleTon<T> : MonoBehaviour where T : MonoSingleTon<T>, new()
    {
        private static T instance;
        public static T GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject(typeof(T).Name).AddComponent<T>();
                    DontDestroyOnLoad(instance.gameObject);
                    instance.Init();
                    //YOON : 추후 씬 전환 이벤트 등록 Reset함수
                }
                return instance;
            }
        }
        /// <summary>
        /// 초기세팅
        /// </summary>
        protected virtual void Init()
        {

        }
        /// <summary>
        /// 씬 전환시 초기화될것들
        /// </summary>
        public virtual void OnSceneChange()
        {

        }
    }
}
namespace ParsingData
{
    [System.Serializable]
    public class UnitData
    {
        public string dataname;
        public string uiname;
        public CharacterGrade grade;
        public string spriteName;
        public AttackModuleType attackmodule;
        public float attackrange;
        public float attackdelay;
        public float damage;
        public float summonduration;
        public float tickdelay;
    }
    [System.Serializable]
    public class LevelData
    {
        public int level;
        public float common;
        public float unCommon;
        public float rare;
        public float superRare;
    }
    [System.Serializable]
    public class Wrapper<T>
    {
        public List<T> Sheet;
    }
}
namespace ResourceManaging
{
    public class Pool<T> where T : MonoBehaviour
    {
        private Queue<T> pool;
        private string key;
        private Transform folder;
        public Pool(string key)
        {
            this.key = key;
            pool = new Queue<T>();
            folder = new GameObject($"{key}Pool").transform;
        }
        /// <summary>
        /// 씬 변경 등 캐싱되어있는 정보를 잃어버릴때 실행
        /// </summary>
        public void Reset()
        {
            pool.Clear();
        }
        public void EnQueue(T obj)
        {
            pool.Enqueue(obj);
            obj.transform.parent = folder;
            obj.gameObject.SetActive(false);
        }
        public T DeQueue()
        {
            if (pool.TryDequeue(out T result))
            {
                result.gameObject.SetActive(true);
                return result;
            }
            else
            {
                if (ResourceManager.GetInstance.preLoaded.TryGetValue(key,out object prefab))
                {
                    GameObject obj = GameObject.Instantiate((GameObject)prefab, folder);
                    return obj.GetComponent<T>();
                }
                return null;
            }
        }
    }
}
public interface State<T> where T : Enum
{
    StateMachine<T, State<T>> stateMachine { get; }
    public void Enter();
    public void Execute();
    public void Exit();
}
public class StateMachine<T, V> where V : State<T> where T : Enum
{
    V curr;
    Dictionary<T, V> states;
    public StateMachine((T,V)[] states,T defaultState)
    {
        for (int i = 0; i < states.Length; i++)
        {
            this.states.Add(states[i].Item1, states[i].Item2);
        }
        curr = this.states[defaultState];
    }
    public void ChageState(T type)
    {
        curr.Exit();
        curr = states[type];
        curr.Enter();
    }
    public void OnExecute()
    {
        curr.Execute();
    }

}
using UnityEngine;
using System.Collections.Generic;
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
        public CharactorGrade grade;
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
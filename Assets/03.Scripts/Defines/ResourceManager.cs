using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Singleton;
public class ResourceManager : SingleTon<ResourceManager>
{
    public Dictionary<string, object> preLoaded;
    public bool loadDone { get { return currLoad == goalLoadCount; } }
    private int currLoad = 0;
    private int goalLoadCount = 0;
    protected override void Init()
    {
        preLoaded = new Dictionary<string, object>();
        LoadToRegist<object>("Preload", preLoaded);
    }
    public void LoadToRegist<T>(string label, Dictionary<string,T> dict)
    {
        LoadAsyncAll<object>(label,
        (s) =>
        {
            foreach (var result in s)
            {
                T parsedResult = (T)result.Item2;
                if (parsedResult == null) continue;
                if (dict.TryAdd(result.Item1, parsedResult))
                {
                    Debug.Log(result.Item1 + "등록 성공");
                }
                else
                {
                    Debug.LogError(result.Item1 + "에 해당하는 리소스가 이미 존재합니다.");
                }
            }
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">받아올 오브젝트의 타입</typeparam>
    /// <param name="key">어드레서블 내 키값</param>
    /// <param name="callback">EX) (obj)=>{targetInstance = obj}</param>
    private void LoadAsync<T>(string key, Action<T> callback, bool isCaching = false)
    {
        AsyncOperationHandle<T> infoAsyncOP = Addressables.LoadAssetAsync<T>(key);
        infoAsyncOP.Completed += (op) =>
        {

            callback?.Invoke(infoAsyncOP.Result);
            if (isCaching) Addressables.Release(infoAsyncOP);
        };
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">����Ÿ��</typeparam>
    /// <param name="label">Ÿ�� Ű��</param>
    /// <param name="callback">(obj)=>{targetInstance = obj}</param>
    private void LoadAsyncAll<T>(string label, Action<(string, T)[]> callback, bool isCaching = false)
    {
        var labelKeys = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        labelKeys.WaitForCompletion();

        Debug.Log(labelKeys.Result);
        if (labelKeys.Result.Count == 0) { Debug.LogError($"{label}���� ����ֽ��ϴ�."); callback.Invoke(null); }
        goalLoadCount += labelKeys.Result.Count;
        int doneCount = 0;

        (string, T)[] tempT = new (string, T)[labelKeys.Result.Count];
        for (int i = 0; i < tempT.Length; i++)
        {
            int curIndex = i;
            string curKey = labelKeys.Result[i].PrimaryKey;
            LoadAsync<T>(labelKeys.Result[i].PrimaryKey, (result) =>
            {
                if (curKey.Contains(".") || curKey.Contains("/"))
                {

                    int slashIndex = curKey.LastIndexOf('/') + 1;
                    int pointIndex = curKey.LastIndexOf('.');


                    int keyLen = pointIndex != -1 ? pointIndex - slashIndex : curKey.Length - slashIndex;
                    curKey = curKey.Substring(slashIndex, keyLen);
                }

                tempT[curIndex].Item1 = curKey;
                tempT[curIndex].Item2 = result;

                doneCount++;
                currLoad++;
                if (doneCount == labelKeys.Result.Count)
                {
                    callback?.Invoke(tempT);
                }
            }, isCaching);
        }
    }
}
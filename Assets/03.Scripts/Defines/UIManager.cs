using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Singleton;
/// <summary>
/// ResourceManager가 
/// </summary>
public class UIManager : SingleTon<UIManager>
{
    public Canvas staticCanvas;
    public TradeBTN upgradeBTN;
    public TradeBTN summonBTN;
    public TradeBTN shiftBTN;
    public TradeBTN fixBTN;

    protected override void Init()
    {
        LoadWait().Forget();
    }
    async UniTaskVoid LoadWait()
    {
        await UniTask.WaitUntil(() => ResourceManager.GetInstance.loadDone);
        staticCanvas = new GameObject("StaticCanvas").AddComponent<Canvas>();
        staticCanvas.gameObject.AddComponent<GraphicRaycaster>();
        staticCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        GameObject.DontDestroyOnLoad(staticCanvas.gameObject);
        CreateBTN<TradeBTN>(ref upgradeBTN, "UpGradeBTN");
        CreateBTN<TradeBTN>(ref summonBTN, "SummonBTN");
        CreateBTN<TradeBTN>(ref shiftBTN, "ShiftBTN");
        CreateBTN<TradeBTN>(ref fixBTN, "FixBTN");
        Debug.Log("로드 완료, UI 캐싱 작업 진행");

    }
    private void CreateBTN<T>(ref T targetInstance,string key) where T : MonoBehaviour
    {
        GameObject tempOBJ = GameObject.Instantiate((GameObject)ResourceManager.GetInstance.preLoaded[key]);
        targetInstance = tempOBJ.GetComponent<T>();
        tempOBJ.transform.parent = staticCanvas.transform;
    }
    public void TradeButtonReset()
    {
        shiftBTN.gameObject.SetActive(false);
        fixBTN.gameObject.SetActive(false);
        summonBTN.gameObject.SetActive(false);
        upgradeBTN.gameObject.SetActive(false);
    }

}
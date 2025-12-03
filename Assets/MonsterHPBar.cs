using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHPBar : MonoBehaviour
{
    Transform tr;
    [SerializeField] Slider slider;
    // Update is called once per frame
    public Slider Init(Transform targetTr)
    {
        this.tr = targetTr;
        transform.parent = UIManager.GetInstance.staticCanvas.transform;
        return slider;
    }
    public void Release()
    {
        tr = null;
        GameManager.GetInstance.spawner.hpPool.EnQueue(this);
    }
    public void SetMaxHP(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }
    public void SetValue(float curr)
    {
        slider.value = curr;
    }
    void Update()
    {
        if (tr == null) return;
        transform.position = Camera.main.WorldToScreenPoint(tr.position);
    }
}

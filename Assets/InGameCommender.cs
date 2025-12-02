using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InGameCommender : MonoBehaviour
{
    private float maxHP = 500;
    private float currHP = 500;
    [SerializeField]private Slider slider;
    AttackModule atkModule;
    [SerializeField] SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetInstance.atkCommender += OnDamage;

        slider.maxValue = maxHP;
        slider.value = currHP;

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDamage(float dmg)
    {
        currHP -= dmg;
        slider.value = currHP;

        if (currHP <= 0f)
        {
            OnDie();
        }
        sr.DOKill(true);
        sr.DOColor(Color.red, 0.1f).OnComplete(()=> sr.color = Color.white);
    }
    private void OnDie()
    {
        //게임오버처리
        GameManager.GetInstance.atkCommender -= OnDamage;
        UIManager.GetInstance.gameOverPannel.SetActive(true);
    }
}

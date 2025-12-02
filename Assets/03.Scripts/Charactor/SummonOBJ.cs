using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonOBJ : MonoBehaviour
{
    //소멸 시간
    float goalTime;
    float currTime;

    //틱당 데미지
    float tickDamage;

    //공격 주기
    float currTick;
    float goalTick;
    [SerializeField] BoxCollider2D bc;
    public void Init(Vector3 pos,float goalTime,float tickDamage, float goalTick,float zAngle)
    {
        transform.position = pos;
        this.currTick = 0f;
        this.currTime = 0f;

        this.goalTick = goalTick;
        this.tickDamage = tickDamage;
        this.goalTime = goalTime;
        transform.eulerAngles = new Vector3(0, 0, zAngle);
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;
        currTime += deltaTime;
        currTick += deltaTime;
        if (currTick >= goalTick)
        {
            //프레임으로 인한 틱 로스 최소화
            currTick -= goalTick;
            Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position, new Vector2(3.1f, 1.3f), transform.eulerAngles.z, 1 << 3);
            if (cols != null &&cols.Length > 0)
            {
                for (int i = 0; i < cols.Length; i++)
                {
                    GameManager.GetInstance.AttackEnemy(cols[i], tickDamage);
                }
            }
        }
        if (goalTime <= currTime)
        {
            SummonAttackModule.summonPool.EnQueue(this);
        }
    }
}

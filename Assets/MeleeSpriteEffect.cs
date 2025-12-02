using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MeleeSpriteEffect : MonoBehaviour
{
    [SerializeField]Animator anim;
    Transform target;
    float goalTime;
    float currTime;
    public void Init(Vector3 enemyPos)
    {
        transform.position = enemyPos;
        currTime = 0f;
        goalTime = anim.runtimeAnimatorController.animationClips.First().length;//어차피 상태값하나뿐
    }
    private void Update()
    {
        currTime += Time.deltaTime;
        if (currTime >= goalTime)
        {
            MeleeAttackModule.effectPool.EnQueue(this);
        }
    }
}

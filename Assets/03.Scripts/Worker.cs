using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject handle;
    bool hasMineral;
    [SerializeField]bool isWorkOnRight = true;
    public void Init(bool isWorkOnRight,Vector3 pos)
    {
        this.isWorkOnRight = isWorkOnRight;
        transform.position = pos;
        transform.localScale = isWorkOnRight ? Vector3.one : new Vector3(-1f, 1f, 1f);
    }
    private void Update()
    {
        rb.velocity = (isWorkOnRight ? Vector2.right : Vector2.left) * (hasMineral? -1f: 1f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasMineral = collision.gameObject.name == "Mineral";
        handle.SetActive(hasMineral);
        if (isWorkOnRight)
            transform.localScale = hasMineral ? new Vector3(-1f, 1f, 1f) : Vector3.one;
        else
            transform.localScale = !hasMineral ? new Vector3(-1f, 1f, 1f) : Vector3.one;

        if (!hasMineral)
        {
            GameManager.GetInstance.CurrMineral += 1;
        }
    }
}

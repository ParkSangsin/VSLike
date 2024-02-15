using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir) // per: 관통 수치
    {
        this.damage = damage;
        this.per = per;

        // 근접 무기가 아니라면
        if (per > -1)
        {
            rigid.velocity = dir * 15f; // dir 방향으로 15 속력을 갖음
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1) return;

        per--;

        if (per == -1)
        {
            rigid.velocity = Vector3.zero; // 물리속도 미리 초기화 (재활용을 위해)
            gameObject.SetActive(false);
        }
    }
}

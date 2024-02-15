using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health; // ���� ü��
    public float maxHealth; // �ִ� ü��
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    private void FixedUpdate()
    {
        // Hit ���ϸ��̼��� �����ϰ� ���� ���� �̵� X
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;
        
        // dirVec: Enemy�� �̵��� ����
        Vector2 dirVec = target.position - rigid.position;
        dirVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + dirVec);
        rigid.velocity = Vector2.zero; // �ӵ��� �̵��� ������ ���� �ʵ��� ����
    }

    private void LateUpdate()
    {
        spriter.flipX = target.position.x < rigid.position.x;
    }

    // ��ũ��Ʈ�� Ȱ��ȭ�� ��, ȣ��
    private void OnEnable()
    {
        // ���� �������� �ʾ� �������� ���� target�� �ʱ�ȭ (���� ������ �巡�׷δ� �Ұ�)
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2; 

        // �״� ���ϸ��̼����� ��ȯ
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    // �� ���� �ʱ�ȭ �Լ�
    public void Init(SpawnData data)
    {
        // ���� ��ü�� �ִϸ��̼� ����
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // isLive: ��� ������ ���޾� ����Ǵ� �� ����
        if (!collision.CompareTag("Bullet") || !isLive) return;

        health -= collision.GetComponent<Bullet>().damage; // damage ũ�⸸ŭ health ����
        StartCoroutine(KnockBack()); // �ڷ�ƾ �Լ� ȣ�� �� StartCoroutine () �ȿ� �־� ȣ��

        if (health > 0) // �°� ������� ���
        {
            // �ǰ� ���ϸ��̼� ����
            anim.SetTrigger("Hit");
        }
        else // �°� ���� ���
        {
            isLive = false;
            coll.enabled = false; // collider ��Ȱ��ȭ (�浹 x)
            rigid.simulated = false; // rigidBody ��Ȱ��ȭ
            spriter.sortingOrder = 1; // ���ϸ��̼��� ���� �ö���� �ʵ���
            
            // �״� ���ϸ��̼����� ��ȯ
            anim.SetBool("Dead", true);

            // ų, ����ġ ����
            GameManager.Instance.kill++;
            GameManager.Instance.GetExp();
        }
    }

    // �ڷ�ƾ: ���� �ֱ�� �񵿱�ó�� ����Ǵ� �Լ�
    // IEnumerator: �ڷ�ƾ���� ��ȯ�� �������̽�
    // yield: �ڷ�ƾ�� ��ȯ Ű����
    IEnumerator KnockBack()
    {
        yield return wait; // ���� �ϳ��� ���� ������ ������
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos; // Enemy�� ���� ���⺤��
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }


    void Dead()
    {
        gameObject.SetActive(false); // Enemy ��Ȱ��ȭ
    }
}

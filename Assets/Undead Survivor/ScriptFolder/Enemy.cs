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
    Animator anim;
    SpriteRenderer spriter;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!isLive) return;
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
        if (!collision.CompareTag("Bullet")) return;

        health -= collision.GetComponent<Bullet>().damage; // damage ũ�⸸ŭ health ����

        if (health > 0) // Bullet�� �°� ������� ���
        {

        }
        else // Bullet�� �°� ���� ���
        {
            Dead();
        }
    }

    void Dead()
    {
        gameObject.SetActive(false); // Enemy ��Ȱ��ȭ
    }
}

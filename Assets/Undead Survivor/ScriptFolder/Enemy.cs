using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health; // 현재 체력
    public float maxHealth; // 최대 체력
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
        // dirVec: Enemy가 이동할 벡터
        Vector2 dirVec = target.position - rigid.position;
        dirVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + dirVec);
        rigid.velocity = Vector2.zero; // 속도가 이동에 영향을 주지 않도록 제거
    }

    private void LateUpdate()
    {
        spriter.flipX = target.position.x < rigid.position.x;
    }

    // 스크립트가 활성화될 때, 호출
    private void OnEnable()
    {
        // 아직 존재하지 않아 설정하지 못한 target을 초기화 (없기 때문에 드래그로는 불가)
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    // 적 상태 초기화 함수
    public void Init(SpawnData data)
    {
        // 현재 객체의 애니메이션 설정
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) return;

        health -= collision.GetComponent<Bullet>().damage; // damage 크기만큼 health 감소

        if (health > 0) // Bullet에 맞고 살아있을 경우
        {

        }
        else // Bullet에 맞고 죽은 경우
        {
            Dead();
        }
    }

    void Dead()
    {
        gameObject.SetActive(false); // Enemy 비활성화
    }
}

using System.Collections;
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
        // Hit 에니메이션이 동작하고 있을 때는 이동 X
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;
        
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
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2; 

        // 죽는 에니메이션으로 전환
        anim.SetBool("Dead", false);
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
        // isLive: 사망 로직이 연달아 실행되는 것 방지
        if (!collision.CompareTag("Bullet") || !isLive) return;

        health -= collision.GetComponent<Bullet>().damage; // damage 크기만큼 health 감소
        StartCoroutine(KnockBack()); // 코루틴 함수 호출 시 StartCoroutine () 안에 넣어 호출

        if (health > 0) // 맞고 살아있을 경우
        {
            // 피격 에니메이션 실행
            anim.SetTrigger("Hit");
        }
        else // 맞고 죽은 경우
        {
            isLive = false;
            coll.enabled = false; // collider 비활성화 (충돌 x)
            rigid.simulated = false; // rigidBody 비활성화
            spriter.sortingOrder = 1; // 에니메이션이 위로 올라오지 않도록
            
            // 죽는 에니메이션으로 전환
            anim.SetBool("Dead", true);

            // 킬, 경험치 증가
            GameManager.Instance.kill++;
            GameManager.Instance.GetExp();
        }
    }

    // 코루틴: 생명 주기와 비동기처럼 실행되는 함수
    // IEnumerator: 코루틴만의 반환형 인터페이스
    // yield: 코루틴의 반환 키워드
    IEnumerator KnockBack()
    {
        yield return wait; // 다음 하나의 물리 프레임 딜레이
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos; // Enemy를 향한 방향벡터
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }


    void Dead()
    {
        gameObject.SetActive(false); // Enemy 비활성화
    }
}

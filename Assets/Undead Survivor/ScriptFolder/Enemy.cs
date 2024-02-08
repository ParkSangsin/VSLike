using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    bool isLive = true;

    Rigidbody2D rigid;

    SpriteRenderer spriter;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
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
    }
}

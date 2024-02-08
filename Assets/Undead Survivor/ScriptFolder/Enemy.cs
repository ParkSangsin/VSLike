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
    }
}

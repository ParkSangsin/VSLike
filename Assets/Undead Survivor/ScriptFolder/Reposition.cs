using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Reposition : MonoBehaviour
{
    Collider2D coll;
    private void Awake()
    {
        coll = GetComponent<Collider2D>(); // ��ü�� �浿���� �ʵ��� ������ ����
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Ÿ���� ��� ���� ����
        if (!collision.CompareTag("Area")) return;

        Vector3 playerPos = GameManager.Instance.player.transform.position; // player�� ��ġ
        Vector3 myPos = transform.position; // �� Ground�� (�߽�) ��ġ

        float dirX = playerPos.x - myPos.x;
        float dirY = playerPos.y - myPos.y;

        float diffX = Mathf.Abs(dirX);
        float diffY = Mathf.Abs(dirY);

        dirX = dirX > 0 ? 1 : -1; // Ÿ���� �̵��� ���� (����: -1, ������: 1)
        dirY = dirY > 0 ? 1 : -1; // Ÿ���� �̵��� ���� (�Ʒ���: -1, ����: 1)

        switch (transform.tag)
        {
            case "Ground":
                // �� Ground �߽ɿ��� player���� �Ÿ��� x���� �� �ָ�
                if (diffX > diffY)
                {
                    // Ground�� ��ġ �̵� 
                    transform.Translate(Vector3.right * dirX * 40); // 40:���̿� �ִ� Area�� �ǳ� �ٱ� ����
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                else
                {
                    transform.Translate(Vector3.right * dirX * 40);
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;

            case "Enemy":
                if (coll.enabled) // �浹 �����ϴٸ� (= ���� ��� �ִٸ�)
                {
                    Vector3 playerDir = GameManager.Instance.player.inputVec;
                    // �÷��̾� �̵� �ݴ� ������ ȭ�� �� ���� ��ġ���� �����
                    transform.Translate(playerDir * 25 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                }
                break;
        }
    }
}

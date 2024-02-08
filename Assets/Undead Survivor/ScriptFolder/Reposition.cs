using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Reposition : MonoBehaviour
{
    Collider2D coll;
    private void Awake()
    {
        coll = GetComponent<Collider2D>(); // 시체는 충동하지 않도록 설정을 위해
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 타일을 벗어날 때만 실행
        if (!collision.CompareTag("Area")) return;

        Vector3 playerPos = GameManager.Instance.player.transform.position; // player의 위치
        Vector3 myPos = transform.position; // 각 Ground의 (중심) 위치

        float dirX = playerPos.x - myPos.x;
        float dirY = playerPos.y - myPos.y;

        float diffX = Mathf.Abs(dirX);
        float diffY = Mathf.Abs(dirY);

        dirX = dirX > 0 ? 1 : -1; // 타일이 이동할 방향 (왼쪽: -1, 오른쪽: 1)
        dirY = dirY > 0 ? 1 : -1; // 타일이 이동할 방향 (아래쪽: -1, 위쪽: 1)

        switch (transform.tag)
        {
            case "Ground":
                // 각 Ground 중심에서 player까지 거리가 x축이 더 멀면
                if (diffX > diffY)
                {
                    // Ground의 위치 이동 
                    transform.Translate(Vector3.right * dirX * 40); // 40:사이에 있는 Area를 건너 뛰기 위해
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
                if (coll.enabled) // 충돌 가능하다면 (= 적이 살아 있다면)
                {
                    Vector3 playerDir = GameManager.Instance.player.inputVec;
                    // 플레이어 이동 반대 방향의 화면 밖 랜덤 위치에서 재생성
                    transform.Translate(playerDir * 25 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                }
                break;
        }
    }
}

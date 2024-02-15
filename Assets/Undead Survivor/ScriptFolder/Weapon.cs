using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id; // 무기 id
    public int prefabId; // 프리펩 번호
    public float damage; // 데미지
    public int count; // 무기 개수 or 관통력
    public float speed; // 회전 속도 or 발사 속도

    float timer; // 발사 간격
    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>(); // 부모 오브젝트에 있는 컴포넌트
    }

    private void Start()
    {
        Init(); // 무기 생성
    }

    void Update()
    {
        // 무기 별 움직임 조정
        switch (id)
        {
            // 0번 무기일 경우
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime); // 시계방향으로 회전
                break;
            default:
                timer += Time.deltaTime;
                if (timer > speed) // speed초마다 발사 (적을 수록 많이 발사)
                {
                    timer = 0;
                    Fire();
                }
                break;
        }

        // Level Up Test
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
        }
    }

    // 레벨업 시 이 함수가 호출됨. 인자 값으로 변경.
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0) Batch(); // 레벨업 했으면 다시 배치
    }

    public void Init()
    {
        // 무기 별 생성
        switch (id)
        {
            case 0:
                speed = 150; // 시계 방향 회전
                Batch(); // 무기 배치
                break;
            default:
                speed = 0.3f;
                break;
        }
    }

    // 생성된 무기를 배치하는 함수 
    void Batch() 
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;
            
            if (index < transform.childCount) // weapon 자식 개수보다 적게 필요하다면
            {
                bullet = transform.GetChild(index); // 기존 오브젝트를 먼저 활용
            }
            else // 모자라면
            {
                bullet = GameManager.Instance.pool.Get(prefabId).transform; // 풀링에서 가져옴
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero; // 무기 위치를 플레이어의 위치로 초기화
            bullet.localRotation = Quaternion.identity; // 무기 회전값 초기화

            // 각 무기들을 캐릭터를 중심으로 회전 시키고, 자신이 향한 쪽으로 이동
            Vector3 rotVec = Vector3.forward * 360 / count * index;
            bullet.Rotate(rotVec);
            // Translate는 기본적으로 로컬좌표계에서 이동, 따라서 로컬좌표계에서 본 벡터(up)를 월드좌표계에서 이동
            bullet.Translate(bullet.up * 1.5f, Space.World);
            Debug.Log(bullet.up);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // 근접무기는 관통 수치 -1 (무한)
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget) return; // 스캐너에 캐스팅된 대상이 없다면 사격 X

        Vector3 targetPos = player.scanner.nearestTarget.position; // 타겟 위치

        Vector3 dir = targetPos - transform.position; // 타겟을 향한 방향 벡터
        dir = dir.normalized; // 크기를 0으로 정규화

        Transform bullet = GameManager.Instance.pool.Get(prefabId).transform; // 프리펩이 저장된 pool에서 가져옴
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); // 지정된 축(인자)을 기준으로 벡터(인자)를 향해 회전
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }

}

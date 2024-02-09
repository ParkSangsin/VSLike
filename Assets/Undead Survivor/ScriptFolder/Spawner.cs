using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData; // 레벨마다 다른 데이터 저장

    float timer; // 소환 타이머를 위한 변수
    int level; // (스테이지) 레벨

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        // spawnPoint[0]은 Spawner 자신의 위치임에 주의!
    }
    void Update()
    {
        // 1초에 5마리씩 생성
        timer += Time.deltaTime;

        // 10초마다 level이 0, 1로 변화 (게임 시간이 20초가 지나도 고정)
        level = Mathf.FloorToInt(GameManager.Instance.gameTime / 10); // 소수점을 버리고 Int형으로 변환
        if (timer > spawnData[level].spawnTime) 
        {
            timer = 0;
            Spawn();  
        }
    }

    void Spawn()
    {
        // Enemy 생성 (초기 위치: PoolManager) 
        GameObject enemy = GameManager.Instance.pool.Get(0); 
        // 생성된 enemy 위치 이동
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        // 생성된 enemy의 Enemy 스크립트 내 Init 함수 호출 -> 초기화 (현재 레벨에 해당하는 값으로)
        enemy.GetComponent<Enemy>().Init(spawnData[level]); 
    }
}

[System.Serializable] // 임의로 생성한 클래스는 직렬화를 통해 외부에서 볼 수 있다.
// 소환할 몬스터의 데이터
public class SpawnData
{
    public float spawnTime; // 소환 시간
    public int spriteType; // 0: 해골, 1: 좀비
    public int health; // 체력
    public float speed; // 속도

}

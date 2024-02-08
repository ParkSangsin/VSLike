using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    float timer; // 소환 타이머를 위한 변수

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        // spawnPoint[0]은 Spawner 자신의 위치임에 주의!
    }
    void Update()
    {
        // 1초에 5마리씩 생성
        timer += Time.deltaTime;
        if (timer > 0.2f)
        {
            timer = 0;
            Spawn();  
        }
    }

    void Spawn()
    {
        // 랜덤 Enemy 생성 (초기 위치: PoolManager)
        GameObject enemy = GameManager.Instance.pool.Get(Random.Range(0, 2)); 
        // 생성된 enemy 위치 이동
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
}

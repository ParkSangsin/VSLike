using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    float timer; // ��ȯ Ÿ�̸Ӹ� ���� ����

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        // spawnPoint[0]�� Spawner �ڽ��� ��ġ�ӿ� ����!
    }
    void Update()
    {
        // 1�ʿ� 5������ ����
        timer += Time.deltaTime;
        if (timer > 0.2f)
        {
            timer = 0;
            Spawn();  
        }
    }

    void Spawn()
    {
        // ���� Enemy ���� (�ʱ� ��ġ: PoolManager)
        GameObject enemy = GameManager.Instance.pool.Get(Random.Range(0, 2)); 
        // ������ enemy ��ġ �̵�
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
}

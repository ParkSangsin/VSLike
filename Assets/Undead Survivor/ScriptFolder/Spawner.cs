using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData; // �������� �ٸ� ������ ����

    float timer; // ��ȯ Ÿ�̸Ӹ� ���� ����
    int level; // (��������) ����

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        // spawnPoint[0]�� Spawner �ڽ��� ��ġ�ӿ� ����!
    }
    void Update()
    {
        // 1�ʿ� 5������ ����
        timer += Time.deltaTime;

        // 10�ʸ��� level�� 0, 1�� ��ȭ (���� �ð��� 20�ʰ� ������ ����)
        level = Mathf.FloorToInt(GameManager.Instance.gameTime / 10); // �Ҽ����� ������ Int������ ��ȯ
        if (timer > spawnData[level].spawnTime) 
        {
            timer = 0;
            Spawn();  
        }
    }

    void Spawn()
    {
        // Enemy ���� (�ʱ� ��ġ: PoolManager) 
        GameObject enemy = GameManager.Instance.pool.Get(0); 
        // ������ enemy ��ġ �̵�
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        // ������ enemy�� Enemy ��ũ��Ʈ �� Init �Լ� ȣ�� -> �ʱ�ȭ (���� ������ �ش��ϴ� ������)
        enemy.GetComponent<Enemy>().Init(spawnData[level]); 
    }
}

[System.Serializable] // ���Ƿ� ������ Ŭ������ ����ȭ�� ���� �ܺο��� �� �� �ִ�.
// ��ȯ�� ������ ������
public class SpawnData
{
    public float spawnTime; // ��ȯ �ð�
    public int spriteType; // 0: �ذ�, 1: ����
    public int health; // ü��
    public float speed; // �ӵ�

}

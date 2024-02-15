using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Header: �ν������� �Ӽ����� �̻ڰ� ���н�Ű�� Ÿ��Ʋ
    [Header("# Game Control")]
    public float gameTime; // �� ���������� ���� ���� �ð�
    public float maxGameTime = 2 * 10f; // �� ���������� ���� �ִ� �ð�

    [Header("# Plater Info")]
    public int health;
    public int maxHealth = 100;
    public int level; // �������� �ܰ�
    public int kill; // ų ��
    public int exp; // ����ġ
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 }; // �� ������ �ʿ� ����ġ�� �����ϴ� �迭

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;

   

    private void Awake()
    {
        Instance = this; // �ڱ� �ڽ�
    }

    private void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        // �� ���������� ���� �ð� ����
        gameTime += Time.deltaTime;
        if (gameTime > maxGameTime)
        {
            gameTime = 0;
        }
    }

    // ����ġ ȹ�� �Լ�
    public void GetExp()
    {
        exp++;
        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}

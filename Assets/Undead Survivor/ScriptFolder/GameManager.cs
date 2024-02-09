using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PoolManager pool;

    public float gameTime; // �� ���������� ���� ���� �ð�
    public float maxGameTime = 2 * 10f; // �� ���������� ���� �ִ� �ð�

    public Player player;

    private void Awake()
    {
        Instance = this; // �ڱ� �ڽ�
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
}

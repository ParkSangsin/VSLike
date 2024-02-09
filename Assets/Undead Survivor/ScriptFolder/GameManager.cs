using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PoolManager pool;

    public float gameTime; // 한 스테이지의 게임 진행 시간
    public float maxGameTime = 2 * 10f; // 한 스테이지의 게임 최대 시간

    public Player player;

    private void Awake()
    {
        Instance = this; // 자기 자신
    }

    void Update()
    {
        // 한 스테이지의 게임 시간 갱신
        gameTime += Time.deltaTime;
        if (gameTime > maxGameTime)
        {
            gameTime = 0;
        }
    }
}

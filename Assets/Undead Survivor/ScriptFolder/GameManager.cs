using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Header: 인스펙터의 속성들을 이쁘게 구분시키는 타이틀
    [Header("# Game Control")]
    public float gameTime; // 한 스테이지의 게임 진행 시간
    public float maxGameTime = 2 * 10f; // 한 스테이지의 게임 최대 시간

    [Header("# Plater Info")]
    public int health;
    public int maxHealth = 100;
    public int level; // 스테이지 단계
    public int kill; // 킬 수
    public int exp; // 경험치
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 }; // 각 레벨에 필요 경험치를 저장하는 배열

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;

   

    private void Awake()
    {
        Instance = this; // 자기 자신
    }

    private void Start()
    {
        health = maxHealth;
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

    // 경험치 획득 함수
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

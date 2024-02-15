using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    // 프레임마다 호출
    void Update()
    {
    }

    // 물리 연산 프레임마다 호출
    private void FixedUpdate()
    {
        // 위치를 이동 (현재 위치와 더해야 함)

        // normalized: 대각선으로 이동 시 루트 2만큼 가는 현상 방지 (새 인풋 시스템에서는 설정에서 해결)
        // speed: 속도를 조절하기 위한 변수
        // fixedDeltaTime: 물리 프레임 하나가 소비한 시간 (프레임이 높은 기기가 더 빨리 이동하는 것 방지)
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + nextVec);
    }

    
    // 사용자의 입력 발생 시 호출 (using UnityEngine.InputSystem)
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>(); // InputValue 객체에서 Vector2 값 추출
    }
    
    // 프레임이 종료 되기 전 실행되는 생명주기 함수
    private void LateUpdate()
    {
        // Animator 내 변수 갱신 (magnitude: 벡터 크기 반환)
        anim.SetFloat("Speed", inputVec.magnitude); 

        if (inputVec.x != 0)
        {
            /* 좌우 반전
             * 왼쪽 키를 누른다. -> InputVec.x 음수 -> flipX가 true
             * 오른쪽 키를 누른다. -> InputVec.x 양수 -> flipX가 false */
            spriter.flipX = inputVec.x < 0;  
        }
    }   
}


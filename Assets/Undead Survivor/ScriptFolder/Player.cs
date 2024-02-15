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

    // �����Ӹ��� ȣ��
    void Update()
    {
    }

    // ���� ���� �����Ӹ��� ȣ��
    private void FixedUpdate()
    {
        // ��ġ�� �̵� (���� ��ġ�� ���ؾ� ��)

        // normalized: �밢������ �̵� �� ��Ʈ 2��ŭ ���� ���� ���� (�� ��ǲ �ý��ۿ����� �������� �ذ�)
        // speed: �ӵ��� �����ϱ� ���� ����
        // fixedDeltaTime: ���� ������ �ϳ��� �Һ��� �ð� (�������� ���� ��Ⱑ �� ���� �̵��ϴ� �� ����)
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + nextVec);
    }

    
    // ������� �Է� �߻� �� ȣ�� (using UnityEngine.InputSystem)
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>(); // InputValue ��ü���� Vector2 �� ����
    }
    
    // �������� ���� �Ǳ� �� ����Ǵ� �����ֱ� �Լ�
    private void LateUpdate()
    {
        // Animator �� ���� ���� (magnitude: ���� ũ�� ��ȯ)
        anim.SetFloat("Speed", inputVec.magnitude); 

        if (inputVec.x != 0)
        {
            /* �¿� ����
             * ���� Ű�� ������. -> InputVec.x ���� -> flipX�� true
             * ������ Ű�� ������. -> InputVec.x ��� -> flipX�� false */
            spriter.flipX = inputVec.x < 0;  
        }
    }   
}


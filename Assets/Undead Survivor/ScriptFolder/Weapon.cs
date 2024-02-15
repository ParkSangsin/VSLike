using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id; // ���� id
    public int prefabId; // ������ ��ȣ
    public float damage; // ������
    public int count; // ���� ���� or �����
    public float speed; // ȸ�� �ӵ� or �߻� �ӵ�

    float timer; // �߻� ����
    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>(); // �θ� ������Ʈ�� �ִ� ������Ʈ
    }

    private void Start()
    {
        Init(); // ���� ����
    }

    void Update()
    {
        // ���� �� ������ ����
        switch (id)
        {
            // 0�� ������ ���
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime); // �ð�������� ȸ��
                break;
            default:
                timer += Time.deltaTime;
                if (timer > speed) // speed�ʸ��� �߻� (���� ���� ���� �߻�)
                {
                    timer = 0;
                    Fire();
                }
                break;
        }

        // Level Up Test
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
        }
    }

    // ������ �� �� �Լ��� ȣ���. ���� ������ ����.
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0) Batch(); // ������ ������ �ٽ� ��ġ
    }

    public void Init()
    {
        // ���� �� ����
        switch (id)
        {
            case 0:
                speed = 150; // �ð� ���� ȸ��
                Batch(); // ���� ��ġ
                break;
            default:
                speed = 0.3f;
                break;
        }
    }

    // ������ ���⸦ ��ġ�ϴ� �Լ� 
    void Batch() 
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;
            
            if (index < transform.childCount) // weapon �ڽ� �������� ���� �ʿ��ϴٸ�
            {
                bullet = transform.GetChild(index); // ���� ������Ʈ�� ���� Ȱ��
            }
            else // ���ڶ��
            {
                bullet = GameManager.Instance.pool.Get(prefabId).transform; // Ǯ������ ������
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero; // ���� ��ġ�� �÷��̾��� ��ġ�� �ʱ�ȭ
            bullet.localRotation = Quaternion.identity; // ���� ȸ���� �ʱ�ȭ

            // �� ������� ĳ���͸� �߽����� ȸ�� ��Ű��, �ڽ��� ���� ������ �̵�
            Vector3 rotVec = Vector3.forward * 360 / count * index;
            bullet.Rotate(rotVec);
            // Translate�� �⺻������ ������ǥ�迡�� �̵�, ���� ������ǥ�迡�� �� ����(up)�� ������ǥ�迡�� �̵�
            bullet.Translate(bullet.up * 1.5f, Space.World);
            Debug.Log(bullet.up);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // ��������� ���� ��ġ -1 (����)
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget) return; // ��ĳ�ʿ� ĳ���õ� ����� ���ٸ� ��� X

        Vector3 targetPos = player.scanner.nearestTarget.position; // Ÿ�� ��ġ

        Vector3 dir = targetPos - transform.position; // Ÿ���� ���� ���� ����
        dir = dir.normalized; // ũ�⸦ 0���� ����ȭ

        Transform bullet = GameManager.Instance.pool.Get(prefabId).transform; // �������� ����� pool���� ������
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); // ������ ��(����)�� �������� ����(����)�� ���� ȸ��
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }

}

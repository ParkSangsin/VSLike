using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange; // ��ĵ�� ����
    public LayerMask targetLayer; // ������ ������Ʈ�� ���̾�
    public RaycastHit2D[] targets; // ����ĳ��Ʈ ���� ����� ������ �迭
    public Transform nearestTarget; // ���� ����� ������Ʈ�� Transform


    private void FixedUpdate()
    {
        // ���� ������ ĳ��Ʈ�� ��� ��� ����� ��ȯ
        // ����: ĳ���� ���� ��ġ, ���� ������, ĳ���� ����, ĳ���� ����, ��� ���̾�
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();
    }

    // ���� ����� ������Ʈ�� Transform�� ��ȯ�ϴ� �Լ�
    Transform GetNearest()
    {
        Transform result = null;

        float diff = 100; // ������ �� �Ÿ�

        // targets �ȿ� �ִ� RaycastHit2D ��
        foreach(RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos); // �� ��ġ ������ �Ÿ�

            // �ּڰ� ã��
            if (diff > curDiff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange; // 스캔할 범위
    public LayerMask targetLayer; // 선택할 오브젝트의 레이어
    public RaycastHit2D[] targets; // 레이캐스트 수행 결과를 저장할 배열
    public Transform nearestTarget; // 가장 가까운 오브젝트의 Transform


    private void FixedUpdate()
    {
        // 원형 형태의 캐스트를 쏘고 모든 결과를 반환
        // 인자: 캐스팅 시작 위치, 원의 반지름, 캐스팅 방향, 캐스팅 길이, 대상 레이어
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();
    }

    // 가장 가까운 오브젝트의 Transform을 반환하는 함수
    Transform GetNearest()
    {
        Transform result = null;

        float diff = 100; // 임의의 먼 거리

        // targets 안에 있는 RaycastHit2D 중
        foreach(RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos); // 두 위치 사이의 거리

            // 최솟값 찾기
            if (diff > curDiff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
}

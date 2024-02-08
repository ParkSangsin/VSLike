using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리팹들을 보관할 변수
    public GameObject[] prefabs;

    // 풀을 담당하는 리스트
    List<GameObject>[] pools; // 각 인덱스에 GameObject를 저장하는 List를 갖는 배열

    private void Awake()
    {
        // List를 저장하는 배열 초기화
        pools = new List<GameObject>[prefabs.Length]; // 프리펩의 개수 만큼 배열 할당

        // GameObject를 저장하는 List 초기화
        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>(); 
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null; // 반환할 GameObject

        // 요청한 index에 해당하는 pools를 순회
        foreach (GameObject prefab in pools[index])
        {
            if (!prefab.activeSelf) // 오브젝트가 활성화 되어 있지 않다면
            {
                select = prefab; // 선택
                select.SetActive(true); // 활성화
                break;
            }
        }

        if (!select) // 위 코드에서 선택되지 않았다면
        {
            // Instantiate: 원본 오브젝트를 복제하여 장면에 생성하는 함수
            // poolmanager의 자식으로 생성 (poolmanager의 위치에 생성)
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select); // 생성한 GameObject를 pools에 저장
        }

        return select;
    }
}

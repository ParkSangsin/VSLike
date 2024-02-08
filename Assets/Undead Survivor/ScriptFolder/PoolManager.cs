using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // �����յ��� ������ ����
    public GameObject[] prefabs;

    // Ǯ�� ����ϴ� ����Ʈ
    List<GameObject>[] pools; // �� �ε����� GameObject�� �����ϴ� List�� ���� �迭

    private void Awake()
    {
        // List�� �����ϴ� �迭 �ʱ�ȭ
        pools = new List<GameObject>[prefabs.Length]; // �������� ���� ��ŭ �迭 �Ҵ�

        // GameObject�� �����ϴ� List �ʱ�ȭ
        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>(); 
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null; // ��ȯ�� GameObject

        // ��û�� index�� �ش��ϴ� pools�� ��ȸ
        foreach (GameObject prefab in pools[index])
        {
            if (!prefab.activeSelf) // ������Ʈ�� Ȱ��ȭ �Ǿ� ���� �ʴٸ�
            {
                select = prefab; // ����
                select.SetActive(true); // Ȱ��ȭ
                break;
            }
        }

        if (!select) // �� �ڵ忡�� ���õ��� �ʾҴٸ�
        {
            // Instantiate: ���� ������Ʈ�� �����Ͽ� ��鿡 �����ϴ� �Լ�
            // poolmanager�� �ڽ����� ���� (poolmanager�� ��ġ�� ����)
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select); // ������ GameObject�� pools�� ����
        }

        return select;
    }
}

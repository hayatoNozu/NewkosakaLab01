using UnityEngine;
using System.Collections.Generic;

public class MirrorSpawner : MonoBehaviour
{
    public GameObject mirrorPrefab; // ����Prefab
    public Transform[] wallAreas;  // �ǃG���A�i4�̕ǂ̃G���A���w��j
    public int mirrorCount = 10;   // �������鋾�̐�
    public float minDistance = 2f; // �����m�̍ŏ�����

    private List<Vector3> mirrorPositions = new List<Vector3>(); // ���̈ʒu���L�^

    void Start()
    {
        SpawnMirrors();
    }

    void SpawnMirrors()
    {
        int mirrorsSpawned = 0;

        while (mirrorsSpawned < mirrorCount)
        {
            // �����_���ȕǃG���A��I��
            Transform selectedArea = wallAreas[Random.Range(0, wallAreas.Length)];

            // �G���A���̃����_���Ȉʒu���v�Z
            Vector3 randomPosition = GetRandomPositionInArea(selectedArea);

            // �����m�̋������`�F�b�N
            if (IsPositionValid(randomPosition))
            {
                // ���𐶐�
                Instantiate(mirrorPrefab, randomPosition, Quaternion.identity);

                // ���̈ʒu�����X�g�ɒǉ�
                mirrorPositions.Add(randomPosition);

                mirrorsSpawned++;
            }
        }
    }

    Vector3 GetRandomPositionInArea(Transform area)
    {
        // �G���A�̃T�C�Y���擾
        Vector3 areaSize = area.localScale;
        Vector3 areaCenter = area.position;

        // �G���A���̃����_���Ȉʒu���v�Z
        float randomX = Random.Range(-areaSize.x / 2, areaSize.x / 2);
        float randomY = Random.Range(-areaSize.y / 2, areaSize.y / 2);
        float randomZ = Random.Range(-areaSize.z / 2, areaSize.z / 2);

        return areaCenter + new Vector3(randomX, randomY, randomZ);
    }

    bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 existingPosition in mirrorPositions)
        {
            // �����̋��Ƃ̋������`�F�b�N
            if (Vector3.Distance(position, existingPosition) < minDistance)
            {
                return false;
            }
        }
        return true;
    }
}
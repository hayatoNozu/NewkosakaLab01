using UnityEngine;
using System.Collections.Generic;

public class MirrorSpawner : MonoBehaviour
{
    public GameObject mirrorPrefab; // 鏡のPrefab
    public Transform[] wallAreas;  // 壁エリア（4つの壁のエリアを指定）
    public int mirrorCount = 10;   // 生成する鏡の数
    public float minDistance = 2f; // 鏡同士の最小距離

    private List<Vector3> mirrorPositions = new List<Vector3>(); // 鏡の位置を記録

    void Start()
    {
        SpawnMirrors();
    }

    void SpawnMirrors()
    {
        int mirrorsSpawned = 0;

        while (mirrorsSpawned < mirrorCount)
        {
            // ランダムな壁エリアを選択
            Transform selectedArea = wallAreas[Random.Range(0, wallAreas.Length)];

            // エリア内のランダムな位置を計算
            Vector3 randomPosition = GetRandomPositionInArea(selectedArea);

            // 鏡同士の距離をチェック
            if (IsPositionValid(randomPosition))
            {
                // 鏡を生成
                Instantiate(mirrorPrefab, randomPosition, Quaternion.identity);

                // 鏡の位置をリストに追加
                mirrorPositions.Add(randomPosition);

                mirrorsSpawned++;
            }
        }
    }

    Vector3 GetRandomPositionInArea(Transform area)
    {
        // エリアのサイズを取得
        Vector3 areaSize = area.localScale;
        Vector3 areaCenter = area.position;

        // エリア内のランダムな位置を計算
        float randomX = Random.Range(-areaSize.x / 2, areaSize.x / 2);
        float randomY = Random.Range(-areaSize.y / 2, areaSize.y / 2);
        float randomZ = Random.Range(-areaSize.z / 2, areaSize.z / 2);

        return areaCenter + new Vector3(randomX, randomY, randomZ);
    }

    bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 existingPosition in mirrorPositions)
        {
            // 既存の鏡との距離をチェック
            if (Vector3.Distance(position, existingPosition) < minDistance)
            {
                return false;
            }
        }
        return true;
    }
}
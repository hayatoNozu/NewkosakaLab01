using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    // スポーンするオブジェクトのプレハブを2種類設定
    public GameObject[] objectPrefabs;

    // スポーン候補の位置（4か所）
    public Transform[] spawnPoints;

    // オブジェクトの最小距離
    public float minDistance = 2f;

    // 現在の生成済みオブジェクトの位置リスト
    private List<Vector3> spawnedPositions = new List<Vector3>();

    // スポーンを行うメソッド
    public void SpawnObject(Color mirrorColor)
    {
        if (spawnPoints.Length == 0 || objectPrefabs.Length == 0)
        {
            Debug.LogError("Spawn points or object prefabs are not set!");
            return;
        }

        // ランダムにスポーン位置を選ぶ
        int randomPointIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenPoint = spawnPoints[randomPointIndex];

        // ローカル座標でのZ軸オフセットを決定し、重複を回避
        Vector3 localOffset;
        Vector3 spawnPosition;
        int attempts = 0; // 無限ループ防止用
        const int maxAttempts = 100;

        do
        {
            localOffset = new Vector3(0, 0, Random.Range(-6f, 6f));
            spawnPosition = chosenPoint.TransformPoint(localOffset); // ローカル座標をワールド座標に変換
            attempts++;
        }
        while (!IsPositionValid(spawnPosition) && attempts < maxAttempts);

        // 最大試行回数を超えた場合
        if (attempts >= maxAttempts)
        {
            Debug.LogWarning("Failed to find a valid position for spawning. Skipping spawn.");
            return;
        }

        // ランダムにオブジェクトを選択
        int randomPrefabIndex = Random.Range(0, objectPrefabs.Length);
        GameObject chosenPrefab = objectPrefabs[randomPrefabIndex];

        // オブジェクトをインスタンス化
        GameObject spawnedObject = Instantiate(chosenPrefab, chosenPoint);
        spawnedObject.transform.localPosition = localOffset;

        Transform targetChild = spawnedObject.transform.Find("mirror");

        if (targetChild != null)
        {
            // 子オブジェクトが見つかった場合、そのRendererを取得してマテリアルを変更
            Renderer childRenderer = targetChild.GetComponent<Renderer>();
            if (childRenderer != null)
            {
                childRenderer.material.color = mirrorColor; // 新しい色を適用
            }
            else
            {
                Debug.LogWarning("Renderer not found on target child!");
            }
        }
        else
        {
            Debug.LogWarning($"Child with name mirror not found!");
        }

        // 新しいオブジェクトの位置をリストに追加
        spawnedPositions.Add(spawnPosition);
    }


    // 指定された位置が他のオブジェクトと十分離れているか確認
    private bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 spawnedPosition in spawnedPositions)
        {
            if (Vector3.Distance(position, spawnedPosition) < minDistance)
            {
                return false;
            }
        }
        return true;
    }

    // デバッグ用にキーでスポーンをトリガー
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // スペースキーでスポーン
        {
            SpawnObject(Color.white);
        }
    }
}
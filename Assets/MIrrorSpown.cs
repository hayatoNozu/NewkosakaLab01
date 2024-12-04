using System.Collections.Generic;
using UnityEngine;

public class MirrorSpown : MonoBehaviour
{
    public GameObject[] objectPrefabs; // スポーンするオブジェクトのプレハブを2種類設定
    public Transform[] spawnPoints;   // スポーン候補の位置（4か所）
    public float minDistance = 2f;    // オブジェクトの最小距離

    private List<GameObject> spawnedObjects = new List<GameObject>(); // 現在の生成済みオブジェクトのリスト
    private int currentSpawnIndex = 0; // 次に使用するスポーンポイントのインデックス

    public void SpawnObject(Color mirrorColor)
    {
        if (spawnPoints.Length == 0 || objectPrefabs.Length == 0)
        {
            Debug.LogError("Spawn points or object prefabs are not set!");
            return;
        }

        // 順番にスポーンポイントを選ぶ
        Transform chosenPoint = spawnPoints[currentSpawnIndex];
        currentSpawnIndex = (currentSpawnIndex + 1) % spawnPoints.Length; // インデックスを更新してループさせる

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

        if (attempts >= maxAttempts)
        {
            Debug.LogWarning("Failed to find a valid position for spawning. Skipping spawn.");
            return;
        }

        int randomPrefabIndex = Random.Range(0, objectPrefabs.Length);
        GameObject chosenPrefab = objectPrefabs[randomPrefabIndex];
        GameObject spawnedObject = Instantiate(chosenPrefab, chosenPoint);
        spawnedObject.transform.localPosition = localOffset;
        Transform targetChild = spawnedObject.transform.Find("mirror");

        if (targetChild != null)
        {
            Renderer childRenderer = targetChild.GetComponent<Renderer>();
            if (childRenderer != null)
            {
                childRenderer.material.color = mirrorColor;
            }
            else
            {
                Debug.LogWarning("Renderer not found on target child!");
            }

            string colorTag = GetColorTag(mirrorColor);
            spawnedObject.tag = colorTag;
            ApplyTagToChildren(spawnedObject.transform, colorTag);
        }
        else
        {
            Debug.LogWarning($"Child with name mirror not found!");
        }

        spawnedObjects.Add(spawnedObject); // 新しいオブジェクトをリストに追加
    }

    public void DestroyMirror(GameObject mirror)
    {
        // リストから削除
        if (spawnedObjects.Remove(mirror))
        {
            Debug.Log($"Mirror {mirror.name} removed from the list.");
        }
        else
        {
            Debug.LogWarning($"Mirror {mirror.name} not found in the list.");
        }

        // 実際にオブジェクトを破壊
        Destroy(mirror);
    }

    private string GetColorTag(Color color)
    {
        if (color == Color.red) return "red";
        if (color == Color.blue) return "blue";
        if (color == Color.green) return "green";
        if (color == Color.white) return "white";
        return null;
    }

    private void ApplyTagToChildren(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            child.tag = tag;
            ApplyTagToChildren(child, tag);
        }
    }

    private bool IsPositionValid(Vector3 position)
    {
        foreach (GameObject spawnedObject in spawnedObjects)
        {
            if (Vector3.Distance(position, spawnedObject.transform.position) < minDistance)
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnObject(Color.red);
        }
    }
}
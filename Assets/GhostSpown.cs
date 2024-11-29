using UnityEngine;
using System.Collections.Generic;
public class GhostSpown : MonoBehaviour
{

    [SerializeField] private GameObject objectToSpawn; // 生成するオブジェクト
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 spawnAreaMin;     // 生成範囲の最小値
    [SerializeField] private Vector3 spawnAreaMax;     // 生成範囲の最大値
    [SerializeField] private float spawnInterval = 5f; // 生成間隔（秒）
    [SerializeField] private float minDistanceBetweenObjects = 2f; // お化け間の最小距離
    [SerializeField] private float minDistanceFromPlayer = 3f;     // プレイヤーとの最小距離
    private List<GameObject> spawnedObjects = new List<GameObject>(); // スポーン済みのお化けを記録

    private float time;
    private bool spown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 0;
        spown = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (spown)
        {
            time += Time.deltaTime;
            if(time >= spawnInterval)
            {
                for (int i = 0; i < 10; i++) // 最大10回位置を試行
                {
                    // ランダムな位置を計算
                    Vector3 randomPosition = new Vector3(
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                    Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                    Random.Range(spawnAreaMin.z, spawnAreaMax.z)
                    );
                    if (IsPositionValid(randomPosition))
                    {
                        GameObject spawnedObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);

                        if (player != null)
                        {
                            Vector3 directionToPlayer = player.transform.position - randomPosition;
                            directionToPlayer.y = 0; // 水平方向のみを見る場合、Y軸の影響を無視
                            spawnedObject.transform.rotation = Quaternion.LookRotation(directionToPlayer);
                        }
                        time = 0;
                        return;
                    }
                    Debug.LogWarning("適切なスポーン位置が見つかりませんでした");
                }
            }


        }
    }

    private bool IsPositionValid(Vector3 position)
    {
        // プレイヤーとの距離をチェック
        if (player != null && Vector3.Distance(position, player.transform.position) < minDistanceFromPlayer)
        {
            return false; // プレイヤーに近すぎる
        }

        // 他のお化けとの距離をチェック
        foreach (GameObject obj in spawnedObjects)
        {
            if (Vector3.Distance(position, obj.transform.position) < minDistanceBetweenObjects)
            {
                return false; // 他のお化けと近すぎる
            }
        }

        return true; // すべての条件を満たしている
    }
}

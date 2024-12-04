using UnityEngine;
using System.Collections.Generic;

public class GhostSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] objectToSpawn; // 生成するオブジェクト配列
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 spawnAreaMin;     // 生成範囲の最小値
    [SerializeField] private Vector3 spawnAreaMax;     // 生成範囲の最大値
    [SerializeField] private float spawnInterval = 5f; // 生成間隔（秒）
    [SerializeField] private float minDistanceBetweenObjects = 2f; // オブジェクト間の最小距離
    [SerializeField] private float minDistanceFromPlayer = 3f;     // プレイヤーとの最小距離

    public List<GameObject> spawnedObjects = new List<GameObject>(); // スポーン済みのオブジェクトを記録
    public bool spawnPossible;
    private List<int> spawnSteps = new List<int> { 3, 3, 3, 1, 3, 3, 1, 3, 3, 2 }; // スポーン順
    private int spawnIndex = 0; // 現在のステップのインデックス
    private float time;
    private int randam;

    void Start()
    {
        time = 0;
        SetRandomSpawnInterval(); // 最初のスポーン間隔を設定
        spawnPossible = true;
    }

    void Update()
    {
        if (!spawnPossible)
        {
            return;
        }
        time += Time.deltaTime;

        if (time >= spawnInterval)
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
                    // スポーンするオブジェクトを決定
                    GameObject objectToSpawnNow = SelectObjectToSpawn();

                    GameObject spawnedObject = Instantiate(objectToSpawnNow, randomPosition, Quaternion.identity);
                    spawnedObjects.Add(spawnedObject);

                    if (player != null)
                    {
                        Vector3 directionToPlayer = player.transform.position - randomPosition;
                        directionToPlayer.y = 0; // 水平方向のみを見る場合、Y軸の影響を無視
                        spawnedObject.transform.rotation = Quaternion.LookRotation(directionToPlayer);
                    }

                    time = 0;
                    SetRandomSpawnInterval(); // 次のスポーン間隔を設定
                    return;
                }
            }

            Debug.LogWarning("適切なスポーン位置が見つかりませんでした");
        }
    }

    private void SetRandomSpawnInterval()
    {
        spawnInterval = Random.Range(5f, 8f); // 5秒から8秒の間でランダム
    }


    private bool IsPositionValid(Vector3 position)
    {
        // プレイヤーとの距離をチェック
        if (player != null && Vector3.Distance(position, player.transform.position) < minDistanceFromPlayer)
        {
            return false; // プレイヤーに近すぎる
        }

        // 他のオブジェクトとの距離をチェック
        foreach (GameObject obj in spawnedObjects)
        {
            if (Vector3.Distance(position, obj.transform.position) < minDistanceBetweenObjects)
            {
                return false; // 他のオブジェクトと近すぎる
            }
        }

        return true; // すべての条件を満たしている
    }

    private GameObject SelectObjectToSpawn()
    {
        // 現在のスポーンステップを取得
        int currentStep = spawnSteps[spawnIndex];

        GameObject selectedObject;
        switch (currentStep)
        {
            case 1: // 配列の2～4からランダムでスポーン
                randam = (int)Random.Range(1, 4);
                if (randam == 1)
                {
                    for (int i = 0; i < 5; i++) // 最大10回位置を試行
                    {
                        this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.red);
                    }
                }
                else if (randam == 2)
                {
                    for (int i = 0; i < 5; i++) // 最大10回位置を試行
                    {
                        this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.green);
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++) // 最大10回位置を試行
                    {
                        this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.blue);
                    }
                }
                selectedObject = objectToSpawn[randam];
                break;
            case 2: // 配列の5～7からランダムでスポーン
                randam = (int)Random.Range(4, 7);
                if (randam == 4)
                {
                    for (int i = 0; i < 8; i++) // 最大10回位置を試行
                    {
                        this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.green);
                        this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.blue);
                    }
                }
                else if (randam == 5)
                {
                    for (int i = 0; i < 8; i++) // 最大10回位置を試行
                    {
                        this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.blue);
                        this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.red);
                    }
                }
                else
                {
                    for (int i = 0; i < 8; i++) // 最大10回位置を試行
                    {
                        this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.red);
                        this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.green);
                    }
                }
                selectedObject = objectToSpawn[randam];
                break;
            case 3: // 配列の1番目をスポーン
                this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.white);
                selectedObject = objectToSpawn[0];
                break;
            default:
                selectedObject = objectToSpawn[0];
                break;
        }

        // スポーンインデックスを更新
        spawnIndex = (spawnIndex + 1) % spawnSteps.Count;

        return selectedObject;
    }


}
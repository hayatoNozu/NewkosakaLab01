using UnityEngine;

public class PlayerObjectSpawner : MonoBehaviour
{
    // インスタンス化するPrefabを指定する
    [SerializeField] private GameObject playerPrefab;

    // プレイヤーオブジェクトの名前
    [SerializeField] private string playerObjectName = "Player";


    void Start()
    {
        // シーン内に指定された名前のオブジェクトが存在しない場合
        if (GameObject.Find(playerObjectName) == null)
        {
            if (playerPrefab != null)
            {
                // オブジェクトをインスタンス化
                GameObject instantiatedPlayer = Instantiate(playerPrefab);

                Debug.Log($"{playerObjectName} オブジェクトをインスタンス化しました。");
            }
            else
            {
                Debug.LogWarning("PlayerPrefabが設定されていません。");
            }
        }
        else
        {
            Debug.Log($"{playerObjectName} オブジェクトは既に存在します。");
        }
    }
}
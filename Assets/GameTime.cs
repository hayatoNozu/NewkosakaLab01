using UnityEngine;

public class TimedObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // 出現させるオブジェクト
    public float spawnDelay; // 2分 = 120秒
    public float spawnFinish; // スポーン終了までの時間

    private float timer = 0f; // 経過時間を計測するタイマー

    public AudioSource bgm;
    public AudioSource resultBgm;

    private bool hasPlayedResultBgm = false; // resultBgmの再生フラグ

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnFinish)
        {
            // スポーンを停止
            this.gameObject.GetComponent<GhostSpawn>().spawnPossible = false;
        }

        if (timer >= spawnDelay && !hasPlayedResultBgm)
        {
            // resultBgmを一度だけ再生する
            bgm.Stop();
            resultBgm.Play();
            objectToSpawn.SetActive(true);

            hasPlayedResultBgm = true; // 再生済みフラグを設定
        }
    }
}
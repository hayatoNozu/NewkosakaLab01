using UnityEngine;
using System.Collections;
using TMPro;

public class Ghost_control : MonoBehaviour
{
    public enum GhostType
    {
        White,
        Red,
        Blue,
        Green,
        Cyan,
        Magenta,
        Yellow
    }

    public GhostType ghostType; // ゴーストの種類をインスペクターで設定可能にする

    private Vector3 originalScale;
    private bool isHit = false;
    private Coroutine shrinkCoroutine;
    private float HP;
    private float maxHP;
    private int ATtime;
    public TextMeshProUGUI HPLabel;

    private float minScale;
    private float maxScale;

    private Renderer ghostRenderer; // ゴーストのレンダラー
    private Renderer childRenderer; // 子オブジェクトのレンダラー


    // GameManage のインスタンスを参照
    public GameManage gameManage;

    void Start()
    {
        HP = 100;
        ATtime = 0;
        maxHP = 100;
        minScale = 0.3f;
        maxScale = 0.4f;

        gameManage = GameObject.Find("GameMnager").GetComponent<GameManage>();
        StartCoroutine(Atack());
        gameManage.AG += 1; // ゲームマネージャーの総ゴースト数を増加
        
    }

    void Update()
    {
        HPLabel.text = "HP:" + HP;

        if (HP <= 0)
        {
            AddDefeatCount();
            gameManage.DG += 1; // ゲーム全体の倒されたゴースト数を増加
            Destroy(this.gameObject);
        }

        float newScale = Mathf.Lerp(minScale, maxScale, HP / maxHP);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    private void OnTriggerEnter(Collider other)
    {
        string laserTag = other.gameObject.tag;
        if (laserTag != "laser") return;
        // オブジェクトの種類と対応するレーザーのタグが一致する場合にダメージを受ける
        if (laserTag == "laser" ||
            (ghostType == GhostType.Red && laserTag == "Rlaser") ||
            (ghostType == GhostType.Blue && laserTag == "Blaser") ||
            (ghostType == GhostType.Green && laserTag == "Glaser") ||
            (ghostType == GhostType.Cyan && laserTag == "Claser") ||
            (ghostType == GhostType.Magenta && laserTag == "Mlaser") ||
            (ghostType == GhostType.Yellow && laserTag == "Ylaser") ||
            ghostType == GhostType.White) // Whiteはすべてのレーザーでダメージを受ける
        {
            Debug.Log($"{ghostType}: {laserTag} タグでダメージを受けました！");
            HP -= 1f;
            this.gameObject.GetComponent<Animator>().SetTrigger("Hit");
        }
        else
        {
            Debug.Log("レーザータグが一致しません。");
        }
    }

    private IEnumerator Atack()
    {
        while (ATtime < 15)
        {
            ATtime += 1;
            yield return new WaitForSeconds(1f);
        }

        // ゴーストが消える直前にダメージを与える
        if (ghostType != GhostType.White)
        {
            yield return StartCoroutine(MoveToTargetAndDisappear());
        }
        else
        {
            StartCoroutine(Escape());
        }
    }

    private IEnumerator Escape()
    {
        // 反対方向を向く
        transform.rotation = Quaternion.LookRotation(-transform.forward);

        // ゴーストが逃げる向きを決定（現在の前方方向）
        Vector3 escapeDirection = transform.forward.normalized;

        // 移動速度を設定
        float escapeSpeed = 1f;

        // 移動を1秒間続ける
        float escapeDuration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < escapeDuration)
        {
            // ゴーストを移動させる
            transform.position += escapeDirection * escapeSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        OnDestroy();
        // 移動後にゴーストを削除
        Destroy(this.gameObject);
    }

    private IEnumerator MoveToTargetAndDisappear()
    {
        // `objectsToHide` から現在の `currentIndex` の位置を取得
        if (gameManage.objectsToHide != null && gameManage.currentIndex < gameManage.objectsToHide.Length)
        {
            GameObject targetObject = gameManage.objectsToHide[gameManage.currentIndex];

            // 対象オブジェクトが非アクティブでない場合のみ移動を実行
            if (targetObject != null && targetObject.activeSelf)
            {
                Vector3 startPosition = transform.position;
                Vector3 targetPosition = targetObject.transform.position;

                float moveDuration = 3f; // 移動にかかる時間
                float elapsedTime = 0f;

                while (elapsedTime < moveDuration)
                {
                    // ゴーストをターゲットの位置に線形補間で移動
                    transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // 最後にターゲット位置を正確に設定
                transform.position = targetPosition;

                // ダメージを加算
                gameManage.damage += 1;

                // 非表示にするオブジェクトを非アクティブに
                targetObject.SetActive(false);
                gameManage.currentIndex++;
            }
        }

        // ゴーストを削除
        OnDestroy();
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        GameObject.Find("Director").GetComponent<GhostSpawn>().spawnedObjects.Remove(this.gameObject);
    }

    private void AddDefeatCount()
    {
        switch (ghostType)
        {
            case GhostType.White:
                gameManage.whgD += 1;
                break;
            case GhostType.Red:
                gameManage.regD += 1;
                break;
            case GhostType.Blue:
                gameManage.blgD += 1;
                break;
            case GhostType.Green:
                gameManage.grgD += 1;
                break;
            case GhostType.Cyan:
                gameManage.cygD += 1;
                break;
            case GhostType.Magenta:
                gameManage.magD += 1;
                break;
            case GhostType.Yellow:
                gameManage.yegD += 1;
                break;
            default:
                Debug.LogWarning($"予期しないゴーストタイプ: {ghostType}");
                break;
        }
    }
}
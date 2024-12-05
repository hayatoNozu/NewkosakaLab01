using UnityEngine;
using System.Collections;
using TMPro;

public class Ghost_control : MonoBehaviour
{
    public enum GhostType
    {
        White,
        Red,
        Green,
        Blue,
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
    private bool isDefeated = false; // やられた状態を管理するフラグ
    // GameManage のインスタンスを参照
    public GameManage gameManage;
    public GameObject timer;



    void Start()
    {
        HP = 100;
        ATtime = 0;
        maxHP = 100;
        minScale = 0.3f;
        maxScale = 0.4f;

        // GameManage の参照を取得
        gameManage = GameObject.Find("GameMnager").GetComponent<GameManage>();

        // ゴーストの種類に応じて出現数を増加（配列を利用）
        gameManage.IncrementSpawnCount((GameManage.GhostType)ghostType);

        // 攻撃コルーチンを開始
        StartCoroutine(Atack());
    }

    void Update()
    {
        //HPLabel.text = "HP:" + HP;

        if (HP <= 0 && !isDefeated)
        {

            isDefeated = true; // やられた状態に設定
            StartCoroutine(HandleDefeat());
        }

        float newScale = Mathf.Lerp(minScale, maxScale, HP / maxHP);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    private IEnumerator HandleDefeat()
    {
        // アニメーション再生
        Animator animator = this.gameObject.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Defeated");
        }

        // アニメーションの終了を待機
        yield return new WaitForSeconds(1.4f);

        // 倒されたカウントを増加
        AddDefeatCount();

        // ゴーストを削除
        OnDestroy();
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        string laserTag = other.gameObject.tag;

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
            HP -= 3f;
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
        transform.rotation = Quaternion.LookRotation(-transform.forward);

        Vector3 escapeDirection = transform.forward.normalized;
        float escapeSpeed = 1f;
        float escapeDuration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < escapeDuration)
        {
            transform.position += escapeDirection * escapeSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        OnDestroy();
        Destroy(this.gameObject);
    }

    private IEnumerator MoveToTargetAndDisappear()
    {
        if (gameManage.objectsToHide != null && gameManage.currentIndex < gameManage.objectsToHide.Length)
        {
            GameObject targetObject = gameManage.objectsToHide[gameManage.currentIndex];

            if (targetObject != null && targetObject.activeSelf)
            {
                Animator animator = this.gameObject.GetComponent<Animator>();
                animator.SetTrigger("runaway");
                Vector3 startPosition = transform.position;
                Vector3 targetPosition = targetObject.transform.position;

                // 固定するY軸の値を保存
                float fixedY = startPosition.y;

                // ターゲット手前で止まるためのオフセット距離
                float stopDistance = 1.0f;

                float moveDuration = 2f;
                float elapsedTime = 0f;

                // ターゲット手前の位置を計算
                Vector3 directionToTarget = (targetPosition - startPosition).normalized;
                Vector3 stopPosition = targetPosition - directionToTarget * stopDistance;

                // ターゲット手前に移動
                while (elapsedTime < moveDuration)
                {
                    Vector3 newPosition = Vector3.Lerp(startPosition, stopPosition, elapsedTime / moveDuration);

                    // Y軸を固定
                    newPosition.y = fixedY;

                    transform.position = newPosition;
                    elapsedTime += Time.deltaTime;

                    // 移動を途中で終了する条件
                    if (Vector3.Distance(transform.position, stopPosition) < 0.1f)
                    {
                        break;
                    }

                    yield return null;
                }

                // 最終位置もY軸を固定
                transform.position = new Vector3(stopPosition.x, fixedY, stopPosition.z);

                // 攻撃アニメーションの再生
                if (animator != null)
                {
                    animator.SetTrigger("attack");
                    // アニメーションの終了を待機
                    yield return new WaitForSeconds(2f);

                    // ダメージ加算とターゲットオブジェクトの非アクティブ化
                    gameManage.damage += 1;
                    targetObject.SetActive(false);
                    gameManage.currentIndex++;
                    gameManage.CandleUI();
                    Destroy(this.gameObject);
                }
            }
        }
    }

    private void OnDestroy()
    {
        GameObject.Find("Director").GetComponent<GhostSpawn>().spawnedObjects.Remove(this.gameObject);
    }

    private void AddDefeatCount()
    {
        gameManage.IncrementDefeatCount((GameManage.GhostType)ghostType);
    }
}
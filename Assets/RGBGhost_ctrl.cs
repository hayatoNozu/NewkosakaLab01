using UnityEngine;
using System.Collections;
using TMPro;

public class RGBGhost_ctrl : MonoBehaviour
{
    private Vector3 originalScale; // 元のスケールを保存
    private bool isHit = false;    // 現在レーザーが当たっているかどうかを示すフラグ
    private Coroutine shrinkCoroutine; // 実行中のコルーチンを追跡
    private int HP;
    private int ATtime;
    public TextMeshProUGUI HPLabel;

    void Start()
    {
        HP = 100;
        ATtime = 0;
        StartCoroutine(Atack()); // コルーチンを一度だけ実行
    }

    void Update()
    {
        HPLabel.text = "HP:" + HP;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("laser"))
        {

            isHit = true;
            StartCoroutine(wait(other)); // `other`を渡す
            
        }
    }

    private IEnumerator wait(Collider other)
    {
        if(isHit)
        {
            float shrinkRate = 0.05f;
            float minScale = 0.1f;

            // 自分のマテリアルの色を取得
            Renderer ownRenderer = GetComponent<Renderer>();
            if (ownRenderer == null || ownRenderer.material == null) yield break;

            Color ownColor = ownRenderer.material.color;

            // 衝突したオブジェクトのマテリアルの色を取得
            Renderer otherRenderer = other.GetComponent<Renderer>();
            if (otherRenderer == null || otherRenderer.material == null) yield break;

            Color otherColor = otherRenderer.material.color;

            // RGB値を比較
            if (IsColorEqual(ownColor, otherColor))
            {
                Debug.Log($"マテリアルの色が一致しました！: {ownColor}");
                while (isHit && transform.localScale.x > minScale)
                {
                    Vector3 currentScale = transform.localScale;
                    transform.localScale = new Vector3(
                        Mathf.Max(currentScale.x - shrinkRate, minScale),
                        Mathf.Max(currentScale.y - shrinkRate, minScale),
                        Mathf.Max(currentScale.z - shrinkRate, minScale)
                    );
                    HP -= 2;
                    yield return new WaitForSeconds(0.1f);
                    isHit = false;
                }

            
            }
            else
            {
                Debug.Log("マテリアルの色が異なります。");
            }
            // 最小スケールに達した場合、自動的に停止
            if (HP <= 0)
            {
                gameObject.SetActive(false);
                GameManager.score += 5; // スコアを1増やす
            }
        }
    }

    private IEnumerator Atack()
    {
        while (ATtime < 10)
        {
            ATtime += 1;
            yield return new WaitForSeconds(1f); // 1秒ごとに増加
        }

        gameObject.SetActive(false);
    }

    private bool IsColorEqual(Color color1, Color color2, float tolerance = 0.01f)
    {
        return Mathf.Abs(color1.r - color2.r) < tolerance &&
               Mathf.Abs(color1.g - color2.g) < tolerance &&
               Mathf.Abs(color1.b - color2.b) < tolerance;
    }
}

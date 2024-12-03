using UnityEngine;
using System.Collections;
using TMPro;

public class RGBGhost_ctrl : MonoBehaviour
{
    private Vector3 originalScale; // 元のスケールを保存
    private bool isHit = false;    // 現在レーザーが当たっているかどうかを示すフラグ
    private Coroutine shrinkCoroutine; // 実行中のコルーチンを追跡
    private float HP;
    private float maxHP;
    private int ATtime;
    public TextMeshProUGUI HPLabel;

    private float minScale;
    private float maxScale;
    

    void Start()
    {
        HP = 100;
        ATtime = 0;
        maxHP = 100;
        minScale = 0.3f;
        maxScale = 0.4f;
        StartCoroutine(Atack()); // コルーチンを一度だけ実行
        string name = gameObject.name;
        
    }

    void Update()
    {
        HPLabel.text = "HP:" + HP;


        if(HP <=  0)
        {
            if(name == whiteGhost)
            {
                
            }
            else if(name == redGhost)
            {

            }
            else if(name == blueGhost)
            {
                
            }
            else if(name == greenGhost)
            {
                
            }
            else if(name == cyanGhost)
            {
                
            }
            else if(name == magentaGhost)
            {
                
            }
            else if(name == yellowGhost)
            {
                
            }

            Destroy(this.gameObject);
        }

        float newScale = Mathf.Lerp(minScale, maxScale, HP / maxHP);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }
    //whiteGhost,redGhost,blueGhost,greenGhost,cyanGhot,magentaGhost,yelllowGhost

    private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.CompareTag("laser"))
    {
        // ラインレンダラーの色を取得
        LineRenderer lineRenderer = other.GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            Color lineColor = lineRenderer.startColor; // 必要に応じてendColorも使用

            // 自分のマテリアルの色を取得
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null && renderer.material != null)
            {
                Color myColor = renderer.material.color;

                // 色の一致判定（近似比較）
                if (AreColorsSimilar(myColor, lineColor, 0.01f)) // 誤差は0.01f（適宜調整）
                {
                    Debug.Log("マテリアルの色とラインレンダラーの色が一致しました！");
                    HP -= 1f;
                    this.gameObject.GetComponent<Animator>().SetTrigger("Hit");
    
                }
                else
                {
                    Debug.Log("色が一致しません。");
                }
            }
        }
    }
}
/*

    private IEnumerator wait()
    {
        if (isHit)
        {
            float shrinkRate = 0.05f;
            float minScale = 0.1f;

            Debug.Log("Hitt!!!!!!!!!!!!!!!!!!");

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

            // 最小スケールに達した場合、自動的に停止
            if (HP <= 0)
            {
                gameObject.SetActive(false);
                GameManager.score += 1; // スコアを1増やす
            }
        }
    }
    */

    private IEnumerator Atack()
    {
        while (ATtime < 15)
        {
            ATtime += 1;
            yield return new WaitForSeconds(1f); // 1秒ごとに増加
        }
        

        gameObject.SetActive(false);
    }

    private bool AreColorsSimilar(Color color1, Color color2, float tolerance)
    {
        return Mathf.Abs(color1.r - color2.r) < tolerance &&
            Mathf.Abs(color1.g - color2.g) < tolerance &&
            Mathf.Abs(color1.b - color2.b) < tolerance;
    }
}
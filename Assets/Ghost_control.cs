using UnityEngine;
using System.Collections;
using TMPro;

public class Ghost_control : MonoBehaviour
{
    private Vector3 originalScale;
    private bool isHit = false;
    private Coroutine shrinkCoroutine;
    private float HP;
    private float maxHP;
    private int ATtime;
    public TextMeshProUGUI HPLabel;

    private float minScale;
    private float maxScale;

    // GhostColor enumの定義
    public enum GhostColor
    {
        White,
        Red,
        Blue,
        Green,
        Cyan,
        Magenta,
        Yellow
    }

    public GhostColor ghostColor;  // ゴーストの色を管理する変数

    private Renderer ghostRenderer; // ゴーストのレンダラー

    void Start()
    {
        HP = 100;
        ATtime = 0;
        maxHP = 100;
        minScale = 0.3f;
        maxScale = 0.4f;

        // 子オブジェクト「球.003」のRendererを取得
        Transform sphereTransform = transform.Find("球.003");
        if (sphereTransform != null)
        {
            ghostRenderer = sphereTransform.GetComponent<Renderer>();
            if (ghostRenderer == null || ghostRenderer.material == null)
            {
                Debug.LogError("Renderer または Material が見つかりません！");
            }
        }
        else
        {
            Debug.LogError("子オブジェクト「球.003」が見つかりません！");
        }

        StartCoroutine(Atack());
        GameManage.AG += 1;
    }

    void Update()
    {
        HPLabel.text = "HP:" + HP;

        if (HP <= 0)
        {
            // ghostColor に基づいて色を判定し、対応するゴーストのダメージを増やす
            switch (ghostColor)
            {
                case GhostColor.White:
                    GameManage.whgD += 1;
                    break;
                case GhostColor.Red:
                    GameManage.regD += 1;
                    break;
                case GhostColor.Blue:
                    GameManage.blgD += 1;
                    break;
                case GhostColor.Green:
                    GameManage.grgD += 1;
                    break;
                case GhostColor.Cyan:
                    GameManage.cygD += 1;
                    break;
                case GhostColor.Magenta:
                    GameManage.magD += 1;
                    break;
                case GhostColor.Yellow:
                    GameManage.yegD += 1;
                    break;
            }

            GameManage.DG += 1;
            Destroy(this.gameObject);
        }

        float newScale = Mathf.Lerp(minScale, maxScale, HP / maxHP);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("laser"))
        {
            LineRenderer lineRenderer = other.GetComponent<LineRenderer>();
            if (lineRenderer != null)
            {
                Color lineColor = lineRenderer.startColor;

                // レーザーの色とゴーストの色が一致した場合
                if (AreColorsSimilar(ghostRenderer.material.color, lineColor, 0.01f))
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

    private IEnumerator Atack()
    {
        while (ATtime < 15)
        {
            ATtime += 1;
            yield return new WaitForSeconds(1f);
        }

        if (ghostColor != GhostColor.White)
        {
            GameManage.damage += 1;
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

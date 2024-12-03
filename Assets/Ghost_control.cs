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

    // マテリアルの色判定用の色リスト
    public Color whiteGhostColor = new Color(1f, 1f, 1f);
    public Color redGhostColor = new Color(1f, 0f, 0f);
    public Color blueGhostColor = new Color(0f, 0f, 1f);
    public Color greenGhostColor = new Color(0f, 1f, 0f);
    public Color cyanGhostColor = new Color(0f, 1f, 1f);
    public Color magentaGhostColor = new Color(1f, 0f, 1f);
    public Color yellowGhostColor = new Color(1f, 1f, 0f);

    private Renderer ghostRenderer; // ゴーストのレンダラー

    void Start()
    {
        HP = 100;
        ATtime = 0;
        maxHP = 100;
        minScale = 0.3f;
        maxScale = 0.4f;

        ghostRenderer = GetComponent<Renderer>();
        if (ghostRenderer == null || ghostRenderer.material == null)
        {
            Debug.LogError("Renderer または Material が見つかりません！");
        }

        StartCoroutine(Atack()); 
        GameManage.AG += 1;
    }

    void Update()
    {
        HPLabel.text = "HP:" + HP;

        if (HP <= 0)
        {
            if (AreColorsSimilar(ghostRenderer.material.color, whiteGhostColor, 0.01f))
            {
                GameManage.whgD += 1;
            }
            else if (AreColorsSimilar(ghostRenderer.material.color, redGhostColor, 0.01f))
            {
                GameManage.regD += 1;
            }
            else if (AreColorsSimilar(ghostRenderer.material.color, blueGhostColor, 0.01f))
            {
                GameManage.blgD += 1;
            }
            else if (AreColorsSimilar(ghostRenderer.material.color, greenGhostColor, 0.01f))
            {
                GameManage.grgD += 1;
            }
            else if (AreColorsSimilar(ghostRenderer.material.color, cyanGhostColor, 0.01f))
            {
                GameManage.cygD += 1;
            }
            else if (AreColorsSimilar(ghostRenderer.material.color, magentaGhostColor, 0.01f))
            {
                GameManage.magD += 1;
            }
            else if (AreColorsSimilar(ghostRenderer.material.color, yellowGhostColor, 0.01f))
            {
                GameManage.yegD += 1;
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

        if (!AreColorsSimilar(ghostRenderer.material.color, whiteGhostColor, 0.01f))
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

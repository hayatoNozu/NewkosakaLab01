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

        ghostRenderer = GetComponent<Renderer>();
        if (ghostRenderer == null || ghostRenderer.material == null)
        {
            Debug.LogError("Renderer または Material が見つかりません！");
        }

        // 子オブジェクト"球.003"のRendererを取得
        Transform child = transform.Find("球.003");
        if (child != null)
        {
            childRenderer = child.GetComponent<Renderer>();
            if (childRenderer == null)
            {
                Debug.LogError("子オブジェクトのRendererが見つかりません！");
            }
        }
        else
        {
            Debug.LogError("子オブジェクト '球.003' が見つかりません！");
        }

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
        if (other.gameObject.CompareTag("laser") || other.gameObject.CompareTag("Rlaser") || other.gameObject.CompareTag("Blaser") || other.gameObject.CompareTag("Glaser") || other.gameObject.CompareTag("Claser") || other.gameObject.CompareTag("Mlaser") || other.gameObject.CompareTag("Ylaser"))
        {
            // オブジェクト名とレーザーのタグが一致する場合にダメージを受ける
           if((name == "ghostRedPre" && other.gameObject.CompareTag("Rlaser")) ||
            (name == "ghostBluePre" && other.gameObject.CompareTag("Blaser")) ||
            (name == "ghostGreenPre" && other.gameObject.CompareTag("Glaser")) ||
            (name == "ghostCianPre" && other.gameObject.CompareTag("Claser")) ||
            (name == "ghostMagentaPre" && other.gameObject.CompareTag("Mlaser")) ||
            (name == "ghostYellowPre" && other.gameObject.CompareTag("Ylaser")) ||
            (name == "ghostWhitePre")) // whiteGhostはどのレーザーでもダメージを受ける
            {
                Debug.Log($"{name}: {other.gameObject.tag} タグでダメージを受けました！");
                HP -= 1f;
                this.gameObject.GetComponent<Animator>().SetTrigger("Hit");
            }
            else
            {
                Debug.Log("レーザータグが一致しません。");
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

        // ゴーストが消える直前にダメージを与える
        if (name != "whiteGhost")
        {
            gameManage.damage += 1;
        }

        gameObject.SetActive(false);
    }

   private void AddDefeatCount()
{
    switch (name)
    {
        case "whiteGhost":
            gameManage.whgD += 1;
            break;
        case "redGhost":
            gameManage.regD += 1;
            break;
        case "blueGhost":
            gameManage.blgD += 1;
            break;
        case "greenGhost":
            gameManage.grgD += 1;
            break;
        case "cyanGhost":
            gameManage.cygD += 1;
            break;
        case "magentaGhost":
            gameManage.magD += 1;
            break;
        case "yellowGhost":
            gameManage.yegD += 1;
            break;
        default:
            Debug.LogWarning($"予期しないオブジェクト名: {name}");
            break;
    }
}

}

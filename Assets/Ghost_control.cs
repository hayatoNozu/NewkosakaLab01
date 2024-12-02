using UnityEngine;
using System.Collections;
using TMPro;

public class Ghost_control : MonoBehaviour
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
    }

    void Update()
    {
        HPLabel.text = "HP:" + HP;


        if(HP <=  0)
        {
            Destroy(this.gameObject);
        }

        float newScale = Mathf.Lerp(minScale, maxScale, HP / maxHP);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("laser")) 
        {
            HP -= 1f;
            this.gameObject.GetComponent<Animator>().SetTrigger("Hit");
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
}

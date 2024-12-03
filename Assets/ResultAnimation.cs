using UnityEngine;
using System.Collections;

public class SequentialUI : MonoBehaviour
{
    public GameObject[] ghostNormal; // 通常UI要素
    public GameObject[] ghostParfect; // 完璧UI要素
    public GameObject lastUI; // 最後に表示するUI
    public float displayInterval = 0.5f; // 表示間隔
    public float lastDisplayDelay = 1f; // 最後のUIを表示するまでの遅延

    void Start()
    {
        StartCoroutine(StartWithDelay());
    }

    IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(2f); // 2秒待つ
        StartCoroutine(DisplayUIElements());
    }

    IEnumerator DisplayUIElements()
    {
        // 配列内のUI要素を順に表示
        for (int i = 0; i < ghostParfect.Length; i++)
        {
            if (ghostParfect[i] != null)
            {
                ghostParfect[i].SetActive(true); // 完璧UIを表示
                ghostNormal[i]?.SetActive(false); // 通常UIを非表示
                yield return new WaitForSeconds(displayInterval);
            }
        }

        // 最後のUIを1秒後に表示
        yield return new WaitForSeconds(lastDisplayDelay);

        if (lastUI != null)
        {
            lastUI.SetActive(true);
        }
    }
}
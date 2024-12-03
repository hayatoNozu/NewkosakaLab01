using UnityEngine;
using System.Collections; // コルーチン用
using UnityEngine.UI;

public class SequentialUI : MonoBehaviour
{
    public GameObject[] ghostNormal; // UI要素を格納する配列
    public GameObject[] ghostParfect;
    public float displayInterval = 0.5f; // 表示間隔

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
        for (int i = 0; i < ghostParfect.Length; i++)
        {
            if (ghostParfect[i] != null)
            {
                ghostParfect[i].SetActive(true); // UIを表示
                ghostNormal[i].SetActive(false);
                yield return new WaitForSeconds(displayInterval); 
            }
        }
    }
}
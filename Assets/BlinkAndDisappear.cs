using UnityEngine;
using System.Collections;

public class BlinkAndDisappear : MonoBehaviour
{
    public float blinkInterval = 0.5f; // 点滅の間隔（秒）
    public int blinkCount = 3; // 点滅する回数

    public void StartCor()
    {
        // 点滅処理を開始
        StartCoroutine(BlinkAndDestroy());
    }

    private IEnumerator BlinkAndDestroy()
    {
        yield return new WaitForSeconds(blinkCount);
        gameObject.SetActive(false);
    }
}
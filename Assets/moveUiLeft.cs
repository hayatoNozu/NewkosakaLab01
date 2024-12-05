using UnityEngine;

public class MoveUILeft : MonoBehaviour
{
    public float moveSpeed = 50f; // 左に動く速度 (ピクセル/秒)

    private RectTransform rectTransform;

    void Start()
    {
        // RectTransformを取得
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("このスクリプトはUI要素にアタッチしてください (RectTransformが必要です)。");
        }
    }

    void Update()
    {
        if (rectTransform != null)
        {
            // 左に移動する
            rectTransform.anchoredPosition += Vector2.left * moveSpeed * Time.deltaTime;
        }
    }
}
using TMPro;
using UnityEngine;

public class tvTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // タイマーを表示するTextMeshProUGUI
    private float timeRemaining = 120f; // 2分 (120秒)
    private bool timerRunning = true;

    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("TextMeshProUGUI コンポーネントが設定されていません。");
        }

        // 初期タイマー表示を更新
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (timerRunning && timeRemaining > 0)
        {
            // 時間を減らす
            timeRemaining -= Time.deltaTime;

            // 時間が0以下になったら停止
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timerRunning = false;
            }

            // 表示を更新
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        // 残り時間を分と秒に分解
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);

        // フォーマットしてテキストに表示
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }
}

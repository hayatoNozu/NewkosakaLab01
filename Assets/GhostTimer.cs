using UnityEngine;
using TMPro;

public class GhostTimer : MonoBehaviour
{
    public float timeRemaining = 10f; // 初期タイマー時間
    public TextMeshProUGUI timerText;
    public GameObject timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        timerText.text = timeRemaining.ToString("0");
        if(timeRemaining < 0)
        {
            Destroy(timer);
        }
        

    }
}

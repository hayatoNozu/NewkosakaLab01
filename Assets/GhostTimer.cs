using UnityEngine;
using TMPro;

public class GhostTimer : MonoBehaviour
{
    public float timeRemaining = 10f; // �����^�C�}�[����
    public TextMeshProUGUI timerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        timerText.text = timeRemaining.ToString("0");

    }
}
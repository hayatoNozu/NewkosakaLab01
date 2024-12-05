using TMPro;
using UnityEngine;

public class tvTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // �^�C�}�[��\������TextMeshProUGUI
    private float timeRemaining = 120f; // 2�� (120�b)
    private bool timerRunning = true;

    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("TextMeshProUGUI �R���|�[�l���g���ݒ肳��Ă��܂���B");
        }

        // �����^�C�}�[�\�����X�V
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (timerRunning && timeRemaining > 0)
        {
            // ���Ԃ����炷
            timeRemaining -= Time.deltaTime;

            // ���Ԃ�0�ȉ��ɂȂ������~
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timerRunning = false;
            }

            // �\�����X�V
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        // �c�莞�Ԃ𕪂ƕb�ɕ���
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);

        // �t�H�[�}�b�g���ăe�L�X�g�ɕ\��
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }
}

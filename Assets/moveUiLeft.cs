using UnityEngine;

public class MoveUILeft : MonoBehaviour
{
    public float moveSpeed = 50f; // ���ɓ������x (�s�N�Z��/�b)

    private RectTransform rectTransform;

    void Start()
    {
        // RectTransform���擾
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("���̃X�N���v�g��UI�v�f�ɃA�^�b�`���Ă������� (RectTransform���K�v�ł�)�B");
        }
    }

    void Update()
    {
        if (rectTransform != null)
        {
            // ���Ɉړ�����
            rectTransform.anchoredPosition += Vector2.left * moveSpeed * Time.deltaTime;
        }
    }
}
using UnityEngine;
using System.Collections; // �R���[�`���p
using UnityEngine.UI;

public class SequentialUI : MonoBehaviour
{
    public GameObject[] ghostNormal; // UI�v�f���i�[����z��
    public GameObject[] ghostParfect;
    public float displayInterval = 0.5f; // �\���Ԋu

    void Start()
    {
        StartCoroutine(StartWithDelay());
    }

    IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(2f); // 2�b�҂�
        StartCoroutine(DisplayUIElements());
    }

    IEnumerator DisplayUIElements()
    {
        for (int i = 0; i < ghostParfect.Length; i++)
        {
            if (ghostParfect[i] != null)
            {
                ghostParfect[i].SetActive(true); // UI��\��
                ghostNormal[i].SetActive(false);
                yield return new WaitForSeconds(displayInterval); 
            }
        }
    }
}
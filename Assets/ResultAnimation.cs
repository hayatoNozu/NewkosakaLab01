using UnityEngine;
using System.Collections;
using TMPro;

public class SequentialUI : MonoBehaviour
{
    public GameObject[] ghostNormal; // �ʏ�UI�v�f
    public GameObject[] ghostParfect; // ����UI�v�f
    public GameObject[] ghostNokosi;
    public GameObject[] ghostCount;
    public GameObject lastUI; // �Ō�ɕ\������UI
    public float displayInterval = 0.5f; // �\���Ԋu
    public float lastDisplayDelay = 1f; // �Ō��UI��\������܂ł̒x��

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
        // �z�����UI�v�f�����ɕ\��
        for (int i = 0; i < ghostParfect.Length; i++)
        {
            if (ghostParfect[i] != null)
            {
                GameManage manage = GameObject.Find("GameMnager").GetComponent<GameManage>();
                string count = manage.GhostStats[i];
                ghostCount[i].GetComponent<TextMeshProUGUI>().text = count;

                if(manage.spawnCounts[i] != 0 && manage.spawnCounts[i] != manage.defeatCounts[i])
                {
                    ghostNormal[i].SetActive(false);
                    ghostNokosi[i].SetActive(true);
                }
                else if(manage.spawnCounts[i] == manage.defeatCounts[i])
                {
                    ghostParfect[i].SetActive(true); // ����UI��\��
                    ghostNormal[i]?.SetActive(false); // �ʏ�UI���\��
                }

                

                yield return new WaitForSeconds(displayInterval);
            }
        }

        // �Ō��UI��1�b��ɕ\��
        yield return new WaitForSeconds(lastDisplayDelay);

        if (lastUI != null)
        {
            lastUI.SetActive(true);
        }
    }
}
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI; // Image を使うために必要

public class SequentialUI : MonoBehaviour
{
    public GameObject[] ghostNormal;
    public GameObject[] ghostParfect;
    public GameObject[] ghostNokosi;
    public GameObject[] ghostCount;
    public GameObject lastUI;
    public float displayInterval = 0.5f;
    public float lastDisplayDelay = 1f;

    // グレードごとの Image を用意
    public Image gradeImage; // 表示する Image
    public Sprite gradeS;
    public Sprite gradeA;
    public Sprite gradeB;
    public Sprite gradeC;


    void Start()
    {
        StartCoroutine(StartWithDelay());
    }

    IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(DisplayUIElements());
    }

    IEnumerator DisplayUIElements()
    {
        for (int i = 0; i < ghostParfect.Length; i++)
        {
            if (ghostParfect[i] != null)
            {
                GameManage manage = GameObject.Find("GameMnager").GetComponent<GameManage>();
                string count = manage.GhostStats[i];
                ghostCount[i].GetComponent<TextMeshProUGUI>().text = count;

                if (manage.spawnCounts[i] != 0 && manage.spawnCounts[i] != manage.defeatCounts[i])
                {
                    ghostNormal[i].SetActive(false);
                    ghostNokosi[i].SetActive(true);
                }
                else if (manage.spawnCounts[i] == manage.defeatCounts[i])
                {
                    ghostParfect[i].SetActive(true);
                    ghostNormal[i].SetActive(false);
                }

                yield return new WaitForSeconds(displayInterval);
            }
        }

        yield return new WaitForSeconds(lastDisplayDelay);

        if (lastUI != null)
        {
            GameEnd();
            lastUI.SetActive(true);
        }
    }

    float rate = 0;
    public void GameEnd()
    {
        GameManage manage = GameObject.Find("GameMnager").GetComponent<GameManage>();
        int AG = manage.AG;
        int DG = manage.DG;

        if (AG > 0)
        {
            rate = (DG / (float)AG) * 100;
        }



        if (rate < 50)
        {
            gradeImage.sprite = gradeC;
        }
        else if (rate < 75)
        {
            gradeImage.sprite = gradeB;
        }
        else if (rate < 90)
        {
            gradeImage.sprite = gradeA;
        }
        else
        {
            gradeImage.sprite = gradeS;
        }
    }
}
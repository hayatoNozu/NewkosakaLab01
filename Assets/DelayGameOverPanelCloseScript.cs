using UnityEngine;
using System.Collections;

//GameOver��Panel�����X�N���v�g
public class DelayGameOverPanelCloseScript : MonoBehaviour
{
    public float delayTime; //�x������
    public GameObject GameOverHUD; //GameOver�p�l��
    public GameObject SeaneMirro;


    void Start()
    {
        StartCoroutine(PanelCloseDelay());
    }
    
    //�p�l�������Ƃ��ɒx������
    private IEnumerator PanelCloseDelay()
    {
        yield return new WaitForSeconds(delayTime);
        GameOverHUD.SetActive(false);
        SeaneMirro.SetActive(true);
    }
}

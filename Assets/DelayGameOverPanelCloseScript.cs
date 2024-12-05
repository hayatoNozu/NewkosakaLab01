using UnityEngine;
using System.Collections;

//GameOverでPanelを閉じるスクリプト
public class DelayGameOverPanelCloseScript : MonoBehaviour
{
    public float delayTime; //遅延時間
    public GameObject GameOverHUD; //GameOverパネル
    public GameObject SeaneMirro;


    void Start()
    {
        StartCoroutine(PanelCloseDelay());
    }
    
    //パネルを閉じるときに遅延処理
    private IEnumerator PanelCloseDelay()
    {
        yield return new WaitForSeconds(delayTime);
        GameOverHUD.SetActive(false);
        SeaneMirro.SetActive(true);
    }
}

using UnityEngine;
using Valve.VR;
using System;
using TMPro;
using UnityEngine.UI; // Image コンポーネントを扱うために必要

public class Energy : MonoBehaviour
{
    private Boolean interactUI;
    private SteamVR_Action_Boolean Iui = SteamVR_Actions.default_InteractUI;
    private float energy = 100;
    public TextMeshProUGUI energyCount; // テキストでエネルギー表示
    public Image energyGauge;          // 円形ゲージの Image コンポーネント

    public GameObject handle;
    private bool chek0;
    /*
     ハンドルのX軸を取得
    絶対値が１８０になったらエネルギー＋１０
    絶対値が０になるまで上が読まれない

     */
    void Start()
    {
        // 初期化処理が必要ならここに記述
    }

    void Update()
    {

        // エネルギー残量が 0 未満にならないよう制御
        if (energy < 0)
        {
            energy = 0;
            this.gameObject.GetComponent<LaserReflector>().empty = true;
        }
        else
        {
            this.gameObject.GetComponent<LaserReflector>().empty = false;
        }

        // エネルギー残量をテキストに反映
        energyCount.text = energy.ToString("0");

        // エネルギー残量をゲージに反映 (FillAmount は 0.0～1.0 の範囲で設定)
        energyGauge.fillAmount = energy / 100f;
        if(energy > 50f)
        {
            energyGauge.color = Color.green; // 緑
        }
        else if (energy > 25f)
        {
            energyGauge.color = Color.yellow; // 黄色
        }
        else
        {
            energyGauge.color = Color.red; // 赤
        }

        // SteamVR のインプットでエネルギーを減少
        interactUI = Iui.GetState(SteamVR_Input_Sources.RightHand);
        if (interactUI)
        {
            energy -= 0.1f;
        }

        float handleAngle = handle.transform.localEulerAngles.x;
        if(Mathf.Abs(handleAngle) >= 179&&chek0)
        {
            energy += 10;
            chek0 = false;
        }
        else if(Mathf.Abs(handleAngle)<=1)
        {
            chek0 = true;
        }

        Debug.Log(Mathf.Abs(handleAngle));
    }

    
}
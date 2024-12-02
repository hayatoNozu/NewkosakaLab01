using UnityEngine;
using Valve.VR;
using System;
using TMPro;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    private bool interactUI;
    private SteamVR_Action_Boolean Iui = SteamVR_Actions.default_InteractUI;
    public float energy = 100;
    public TextMeshProUGUI energyCount; // テキストでエネルギー表示
    public Image energyGauge;          // 円形ゲージの Image コンポーネント

    public GameObject handle;
    private float cumulativeAngle = 0f; // 累積回転角度
    private float lastAngle = 0f; // 前回フレームのハンドル角度
    private bool triger;

    void Start()
    {
        // ハンドルの初期角度を取得
        lastAngle = handle.transform.localEulerAngles.x;
    }

    void Update()
    {
        // エネルギー残量が 0 未満にならないよう制御
        if (energy < 0)
        {
            energy = 0;
            this.gameObject.GetComponent<LaserReflector>().empty = true;
        }
        else if(energy > 100)
        {
            energy = 100;
        }
        else
        {
            this.gameObject.GetComponent<LaserReflector>().empty = false;
        }

        // エネルギー残量をテキストに反映
        energyCount.text = energy.ToString("0");

        // エネルギー残量をゲージに反映 (FillAmount は 0.0～1.0 の範囲で設定)
        energyGauge.fillAmount = energy / 100f;
        if (energy > 50f)
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
            triger = true;
        }
        else
        {
            triger = false;
        }

        // ハンドルの角度を取得
        float currentAngle = handle.transform.localEulerAngles.x;

        // 角度の差を計算（360度補正を考慮）
        float angleDifference = Mathf.DeltaAngle(lastAngle, currentAngle);
        cumulativeAngle += Mathf.Abs(angleDifference);

        Debug.Log(cumulativeAngle);
        // 累積角度が 360 度を超えた場合にエネルギーを回復
        if (Mathf.Abs(cumulativeAngle) >= 360f)
        {
            if (!triger)
            {
                energy += 10;
            }

            
            cumulativeAngle = 0f; // 累積角度をリセット
        }

        // 現在の角度を保存
        lastAngle = currentAngle;
    }
}
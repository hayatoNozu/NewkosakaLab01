using UnityEngine;
using Valve.VR;

public class Tutorial_2 : MonoBehaviour
{
    public Energy energyScript; // Energy スクリプトへの参照
    public GameObject target2;  // 表示したいターゲット2
    public GameObject target3;  // 表示したいターゲット3

    private bool target2Shown = false; // ターゲット2表示済みフラグ

    private SteamVR_Action_Boolean triggerAction = SteamVR_Actions.default_InteractUI; // トリガー入力
    private SteamVR_Input_Sources hand = SteamVR_Input_Sources.RightHand; // トリガーの入力ソース

    void Start()
    {
        // ターゲット2とターゲット3を最初は非表示に設定
        if (target2 != null)
        {
            target2.SetActive(false);
        }
        if (target3 != null)
        {
            target3.SetActive(false);
        }
    }

    void Update()
    {
        // エネルギーが100でまだターゲット2が表示されていない場合
        if (energyScript.energy >= 100 && !target2Shown)
        {
            if (target2 != null)
            {
                target2.SetActive(true); // ターゲット2を表示
            }
            target2Shown = true; // フラグを更新
        }

        // トリガーを引いたらターゲット2を非表示にし、ターゲット3を表示
        if (target2 != null && target2.activeSelf && triggerAction.GetStateDown(hand))
        {
            target2.SetActive(false); // ターゲット2を非表示
            if (target3 != null)
            {
                target3.SetActive(true); // ターゲット3を表示
            }
        }
    }
}
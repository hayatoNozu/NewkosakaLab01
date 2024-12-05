using UnityEngine;

public class Tutorial_2 : MonoBehaviour
{
    public Energy energyScript; // Energy スクリプトへの参照
    public GameObject target2;  // 表示したいターゲット2
    public GameObject target3;  // 表示したいターゲット3

    private bool target2Shown = false; // ターゲット2表示済みフラグ
    private bool target2HiddenAgain = false; // ターゲット2が再度非表示になったか確認

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

        // ターゲット2が非表示に戻った場合、ターゲット3を表示
        if (target2 != null && !target2.activeSelf && target2Shown && !target2HiddenAgain)
        {
            if (target3 != null)
            {
                target3.SetActive(true); // ターゲット3を表示
            }
            target2HiddenAgain = true; // フラグを更新
        }
    }
}
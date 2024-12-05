using UnityEngine;

public class Tutorial_2 : MonoBehaviour
{
    public Energy energyScript; // Energy スクリプトへの参照
    public GameObject target3;  // 表示したいターゲット3
    public GameObject target4;  // 表示したいターゲット4
    public GameObject target5;  // 表示したいターゲット5
    public GameObject target6;  // 表示したいターゲット6

    private bool target3Shown = false; // ターゲット3表示済みフラグ
    private bool target4Shown = false; // ターゲット4表示済みフラグ
    private bool target3HiddenAgain = false; // ターゲット3が再度非表示になったか確認
    private bool target4HiddenAgain = false; // ターゲット4が再度非表示になったか確認

    void Start()
    {
        // ターゲット3、ターゲット4、ターゲット5、ターゲット6を最初は非表示に設定
        if (target3 != null)
        {
            target3.SetActive(false);
        }
        if (target4 != null)
        {
            target4.SetActive(false);
        }
        if (target5 != null)
        {
            target5.SetActive(false);
        }
        if (target6 != null)
        {
            target6.SetActive(false);
        }
    }

    void Update()
    {
        // エネルギーが100でまだターゲット3が表示されていない場合
        if (energyScript.energy >= 100 && !target3Shown)
        {
            if (target3 != null)
            {
                target3.SetActive(true); // ターゲット3を表示
            }
            target3Shown = true; // フラグを更新
        }

        // エネルギーが100でまだターゲット4が表示されていない場合
        if (energyScript.energy >= 100 && !target4Shown)
        {
            if (target4 != null)
            {
                target4.SetActive(true); // ターゲット4を表示
            }
            target4Shown = true; // フラグを更新
        }

        // ターゲット3が非表示に戻った場合、ターゲット5を表示
        if (target3 != null && !target3.activeSelf && target3Shown && !target3HiddenAgain)
        {
            if (target5 != null)
            {
                target5.SetActive(true); // ターゲット5を表示
            }
            target3HiddenAgain = true; // フラグを更新
        }

        // ターゲット4が非表示に戻った場合、ターゲット6を表示
        if (target4 != null && !target4.activeSelf && target4Shown && !target4HiddenAgain)
        {
            if (target6 != null)
            {
                target6.SetActive(true); // ターゲット6を表示
            }
            target4HiddenAgain = true; // フラグを更新
        }
    }
}
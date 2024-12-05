using UnityEngine;

public class Tutorial_1 : MonoBehaviour
{
    public Energy energyScript; // Energy スクリプトへの参照
    public GameObject targetObject1; // 非表示にするオブジェクト

    void Update()
    {
        // エネルギーが100の場合、対象オブジェクトを非表示にする
        if (energyScript.energy >= 100)
        {
            targetObject1.SetActive(false);
        }
    }
}
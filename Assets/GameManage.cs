using UnityEngine;

public class GameManage : MonoBehaviour
{
    // 出現数
    public static int whg = 0;
    public static int reg = 0;
    public static int grg = 0;
    public static int blg = 0;
    public static int cyg = 0;
    public static int mag = 0;
    public static int yeg = 0;

    // 倒した数
    public static int whgD = 0;
    public static int regD = 0;
    public static int grgD = 0;
    public static int blgD = 0;
    public static int cygD = 0;
    public static int magD = 0;
    public static int yegD = 0;

    public static float rate = 0;
    public static int AG = 0; // 出現数
    public static int DG = 0; // 倒した数
    public static int damage = 0; // ダメージ

    public GameObject[] objectsToHide; // 非表示にするオブジェクトのリスト
    private int currentIndex = 0;      // 次に非表示にするオブジェクトのインデックス

    // 出現数と倒した数を「出現数/倒した数」の形式で管理する変数
    public static string whgStats => $"{whg}/{whgD}";
    public static string regStats => $"{reg}/{regD}";
    public static string grgStats => $"{grg}/{grgD}";
    public static string blgStats => $"{blg}/{blgD}";
    public static string cygStats => $"{cyg}/{cygD}";
    public static string magStats => $"{mag}/{magD}";
    public static string yegStats => $"{yeg}/{yegD}";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ダメージによるオブジェクト非表示処理
        if (damage > currentIndex && currentIndex < objectsToHide.Length)
        {
            // 現在のインデックスに対応するオブジェクトを非表示にする
            objectsToHide[currentIndex].SetActive(false);
            currentIndex++;
        }

        // すべてのオブジェクトが非表示になった場合のゲームオーバー判定
        if (currentIndex >= objectsToHide.Length)
        {
            // ゲームオーバーの処理
        }
    }

    public void GameEnd()
    {
        // 出現数と倒した数の比率を計算
        if (AG > 0) 
        {
            rate = (DG / (float)AG) * 100;
        }

        // 評価を決定
        string grade = "";
        if (rate < 60)
        {
            grade = "F";
        }
        else if (rate < 70)
        {
            grade = "C";
        }
        else if (rate < 80)
        {
            grade = "B";
        }
        else if (rate < 90)
        {
            grade = "A";
        }
        else
        {
            grade = "S";
        }

        // 評価の表示（もしくはその他の処理）
        Debug.Log("評価: " + grade + " (" + rate.ToString("F2") + "%)");
    }
}

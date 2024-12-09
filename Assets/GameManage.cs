using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    public enum GhostType
    {
        White,
        Red,
        Green,
        Blue,
        Cyan,
        Magenta,
        Yellow
    }

    // 出現数と倒した数を配列で管理
    public int[] spawnCounts = new int[7];  // 出現数を配列で管理
    public int[] defeatCounts = new int[7]; // 倒した数を配列で管理

    public float rate = 0;
    public int AG = 0; // 出現数
    public int DG = 0; // 倒した数
    public int damage = 0; // ダメージ

    public GameObject[] objectsToHide; // 非表示にするオブジェクトのリスト
    public int currentIndex = 0;      // 次に非表示にするオブジェクトのインデックス
    public GameObject candleUI;
    public TextMeshProUGUI candleText;
    public TextMeshProUGUI gameoverText;
    public GameObject GameOberHUD;
    public string yarareText;
    public Image ghostColor;

    public Sprite[] color;
    public int colorNumber;

    public AudioSource bgm;
    public AudioSource GameOverBgm;

    // 出現数と倒した数を「出現数/倒した数」の形式で取得
    public string[] GhostStats
    {
        get
        {
            string[] stats = new string[7];
            for (int i = 0; i < spawnCounts.Length; i++)
            {
                stats[i] = $"{defeatCounts[i]}/{spawnCounts[i]}";
            }
            return stats;
        }
    }

    public void IncrementSpawnCount(GhostType type)
    {
        spawnCounts[(int)type]++;
        AG++; // 総出現数も増加
    }

    public void IncrementDefeatCount(GhostType type)
    {
        defeatCounts[(int)type]++;
        DG++; // 総倒した数も増加
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
    
    public void CandleUI()
    {
        int remainingObjects = objectsToHide.Length - currentIndex;
        if (remainingObjects == 0)
        {
            //GameOver処理
            GameOberHUD.SetActive(true);
            gameoverText.text = yarareText;
            ghostColor.sprite = color[colorNumber];
            GameObject.Find("Director").GetComponent<GhostSpawn>().spawnPossible = false;
            DestroyGhost();
            bgm.Stop();
            GameOverBgm.Play();
            return;
        }
        candleUI.SetActive(true);
        candleUI.GetComponent<BlinkAndDisappear>().StartCor();       
        // 残りオブジェクト数を計算
        
        candleText.text = $"残り{remainingObjects}本";
    }

    public void DestroyGhost()
    {
        GameObject[] ghosts = GameObject.FindGameObjectsWithTag("ghost");

        // 各オブジェクトを削除
        foreach (GameObject ghost in ghosts)
        {
            ghost.GetComponent<Ghost_control>().HP =0;
        }
    }
}
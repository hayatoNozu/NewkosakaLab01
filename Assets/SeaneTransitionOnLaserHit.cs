using UnityEngine;
using UnityEngine.SceneManagement; // シーン遷移に必要
using Valve.VR; // SteamVR_Fadeを使うのに必要

public class SceneTransitionOnLaserHit : MonoBehaviour
{
    [Tooltip("遷移先のシーン名を指定してください (例: MainGame, Title)")]
    public string targetSceneName; // 遷移先のシーン名をインスペクターから指定

    [Tooltip("フェードイン・アウトの時間")]
    public float fadeDuration = 2f; // フェードの時間をインスペクターで調整可能

    private bool one = true;

    private GameObject Player;

    private void OnTriggerEnter(Collider other)
    {
        // 当たったオブジェクトのタグが "wlaser" なら
        if (other.gameObject.CompareTag("wlaser") && one)
        {
            Debug.Log($"レーザーが当たりました。{targetSceneName} シーンへ遷移します。");

            // フェードアウトを開始
            SteamVR_Fade.Start(new Color(0, 0, 0, 1), fadeDuration);
            
            // フェードアウトが終わるタイミングでシーン遷移
            StartCoroutine(LoadSceneAfterFade());
            one = false;
        }
    }

    private System.Collections.IEnumerator LoadSceneAfterFade()
    {
        gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(fadeDuration);

        // シーン遷移
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            Player = GameObject.Find("Player");
            Destroy(Player);
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("遷移先のシーン名が指定されていません。");
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement; // シーン遷移に必要
using Valve.VR;//SteamVR_Fadeを使うのに必要

public class SceneTransitionOnLaserHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 当たったオブジェクトのタグが "laser" なら
        if (other.gameObject.CompareTag("wlaser"))
        {
            Debug.Log("レーザーが当たりました。MainGameシーンへ遷移します。");
            SteamVR_Fade.Start(new Color(0, 0, 0, 1), 2);
            // シーンを遷移
            SceneManager.LoadScene("MainGame");
        }
    }
}


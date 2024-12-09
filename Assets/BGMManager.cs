using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource bgm1; // BGM1のAudioSource
    public AudioSource bgm2; // BGM2のAudioSource
    public float transitionDuration = 2.0f; // フェードにかかる時間

    private bool isTransitioning = false;

    void Start()
    {
        // 初期ボリュームを0.5に設定
        if (bgm1 != null) bgm1.volume = 0.3f;
        if (bgm2 != null) bgm2.volume = 0f; // 初めはミュート
    }

    // BGMをクロスフェードする関数
    public void CrossfadeToBGM2()
    {
        if (!isTransitioning)
        {
            bgm2.Play();
            StartCoroutine(Crossfade(bgm1, bgm2));
        }
    }

    public void CrossfadeToBGM1()
    {
        if (!isTransitioning)
        {
            StartCoroutine(Crossfade(bgm2, bgm1));
        }
    }

    private System.Collections.IEnumerator Crossfade(AudioSource from, AudioSource to)
    {
        isTransitioning = true;
        float timer = 0f;

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / transitionDuration;

            from.volume = Mathf.Lerp(0.3f, 0f, progress); // 最大値を0.5に変更
            to.volume = Mathf.Lerp(0f, 0.3f, progress);

            yield return null;
        }

        from.volume = 0f;
        to.volume = 0.3f;

        isTransitioning = false;
    }
}
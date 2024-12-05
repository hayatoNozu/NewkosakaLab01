using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource bgm1; // BGM1のAudioSource
    public AudioSource bgm2; // BGM2のAudioSource
    public float transitionDuration = 2.0f; // フェードにかかる時間

    private bool isTransitioning = false;

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

            from.volume = Mathf.Lerp(1f, 0f, progress);
            to.volume = Mathf.Lerp(0f, 1f, progress);

            yield return null;
        }

        from.volume = 0f;
        to.volume = 1f;

        isTransitioning = false;
    }
}
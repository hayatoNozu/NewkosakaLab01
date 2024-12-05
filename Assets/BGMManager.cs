using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource bgm1; // BGM1��AudioSource
    public AudioSource bgm2; // BGM2��AudioSource
    public float transitionDuration = 2.0f; // �t�F�[�h�ɂ����鎞��

    private bool isTransitioning = false;

    // BGM���N���X�t�F�[�h����֐�
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
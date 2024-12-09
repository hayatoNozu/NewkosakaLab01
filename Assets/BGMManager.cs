using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource bgm1; // BGM1��AudioSource
    public AudioSource bgm2; // BGM2��AudioSource
    public float transitionDuration = 2.0f; // �t�F�[�h�ɂ����鎞��

    private bool isTransitioning = false;

    void Start()
    {
        // �����{�����[����0.5�ɐݒ�
        if (bgm1 != null) bgm1.volume = 0.3f;
        if (bgm2 != null) bgm2.volume = 0f; // ���߂̓~���[�g
    }

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

            from.volume = Mathf.Lerp(0.3f, 0f, progress); // �ő�l��0.5�ɕύX
            to.volume = Mathf.Lerp(0f, 0.3f, progress);

            yield return null;
        }

        from.volume = 0f;
        to.volume = 0.3f;

        isTransitioning = false;
    }
}
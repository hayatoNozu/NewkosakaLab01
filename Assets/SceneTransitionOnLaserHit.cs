using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���J�ڂɕK�v
using Valve.VR; // SteamVR_Fade���g���̂ɕK�v

public class SceneTransitionOnLaserHit : MonoBehaviour
{
    [Tooltip("�J�ڐ�̃V�[�������w�肵�Ă������� (��: MainGame, Title)")]
    public string targetSceneName; // �J�ڐ�̃V�[�������C���X�y�N�^�[����w��

    [Tooltip("�t�F�[�h�C���E�A�E�g�̎���")]
    public float fadeDuration = 2f; // �t�F�[�h�̎��Ԃ��C���X�y�N�^�[�Œ����\

    private void OnTriggerEnter(Collider other)
    {
        // ���������I�u�W�F�N�g�̃^�O�� "wlaser" �Ȃ�
        if (other.gameObject.CompareTag("wlaser"))
        {
            Debug.Log($"���[�U�[��������܂����B{targetSceneName} �V�[���֑J�ڂ��܂��B");

            // �t�F�[�h�A�E�g���J�n
            SteamVR_Fade.Start(new Color(0, 0, 0, 1), fadeDuration);

            // �t�F�[�h�A�E�g���I���^�C�~���O�ŃV�[���J��
            StartCoroutine(LoadSceneAfterFade());
        }
    }

    private System.Collections.IEnumerator LoadSceneAfterFade()
    {
        yield return new WaitForSeconds(fadeDuration);

        // �V�[���J��
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("�J�ڐ�̃V�[�������w�肳��Ă��܂���B");
        }
    }
}
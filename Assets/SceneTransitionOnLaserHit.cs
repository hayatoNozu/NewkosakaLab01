using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���J�ڂɕK�v
using Valve.VR;//SteamVR_Fade���g���̂ɕK�v

public class SceneTransitionOnLaserHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // ���������I�u�W�F�N�g�̃^�O�� "laser" �Ȃ�
        if (other.gameObject.CompareTag("wlaser"))
        {
            Debug.Log("���[�U�[��������܂����BMainGame�V�[���֑J�ڂ��܂��B");
            SteamVR_Fade.Start(new Color(0, 0, 0, 1), 2);
            // �V�[����J��
            SceneManager.LoadScene("MainGame");
        }
    }
}


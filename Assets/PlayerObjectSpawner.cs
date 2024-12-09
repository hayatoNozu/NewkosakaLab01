using UnityEngine;

public class PlayerObjectSpawner : MonoBehaviour
{
    // �C���X�^���X������Prefab���w�肷��
    [SerializeField] private GameObject playerPrefab;

    // �v���C���[�I�u�W�F�N�g�̖��O
    [SerializeField] private string playerObjectName = "Player";


    void Start()
    {
        // �V�[�����Ɏw�肳�ꂽ���O�̃I�u�W�F�N�g�����݂��Ȃ��ꍇ
        if (GameObject.Find(playerObjectName) == null)
        {
            if (playerPrefab != null)
            {
                // �I�u�W�F�N�g���C���X�^���X��
                GameObject instantiatedPlayer = Instantiate(playerPrefab);

                Debug.Log($"{playerObjectName} �I�u�W�F�N�g���C���X�^���X�����܂����B");
            }
            else
            {
                Debug.LogWarning("PlayerPrefab���ݒ肳��Ă��܂���B");
            }
        }
        else
        {
            Debug.Log($"{playerObjectName} �I�u�W�F�N�g�͊��ɑ��݂��܂��B");
        }
    }
}
using UnityEngine;
using System.Collections.Generic;
public class GhostSpown : MonoBehaviour
{

    [SerializeField] private GameObject objectToSpawn; // ��������I�u�W�F�N�g
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 spawnAreaMin;     // �����͈͂̍ŏ��l
    [SerializeField] private Vector3 spawnAreaMax;     // �����͈͂̍ő�l
    [SerializeField] private float spawnInterval = 5f; // �����Ԋu�i�b�j
    [SerializeField] private float minDistanceBetweenObjects = 2f; // �������Ԃ̍ŏ�����
    [SerializeField] private float minDistanceFromPlayer = 3f;     // �v���C���[�Ƃ̍ŏ�����
    private List<GameObject> spawnedObjects = new List<GameObject>(); // �X�|�[���ς݂̂��������L�^

    private float time;
    private bool spown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 0;
        spown = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (spown)
        {
            time += Time.deltaTime;
            if(time >= spawnInterval)
            {
                for (int i = 0; i < 10; i++) // �ő�10��ʒu�����s
                {
                    // �����_���Ȉʒu���v�Z
                    Vector3 randomPosition = new Vector3(
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                    Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                    Random.Range(spawnAreaMin.z, spawnAreaMax.z)
                    );
                    if (IsPositionValid(randomPosition))
                    {
                        GameObject spawnedObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);

                        if (player != null)
                        {
                            Vector3 directionToPlayer = player.transform.position - randomPosition;
                            directionToPlayer.y = 0; // ���������݂̂�����ꍇ�AY���̉e���𖳎�
                            spawnedObject.transform.rotation = Quaternion.LookRotation(directionToPlayer);
                        }
                        time = 0;
                        return;
                    }
                    Debug.LogWarning("�K�؂ȃX�|�[���ʒu��������܂���ł���");
                }
            }


        }
    }

    private bool IsPositionValid(Vector3 position)
    {
        // �v���C���[�Ƃ̋������`�F�b�N
        if (player != null && Vector3.Distance(position, player.transform.position) < minDistanceFromPlayer)
        {
            return false; // �v���C���[�ɋ߂�����
        }

        // ���̂������Ƃ̋������`�F�b�N
        foreach (GameObject obj in spawnedObjects)
        {
            if (Vector3.Distance(position, obj.transform.position) < minDistanceBetweenObjects)
            {
                return false; // ���̂������Ƌ߂�����
            }
        }

        return true; // ���ׂĂ̏����𖞂����Ă���
    }
}

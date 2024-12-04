using UnityEngine;
using System.Collections.Generic;

public class GhostSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] objectToSpawn; // ��������I�u�W�F�N�g�z��
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 spawnAreaMin;     // �����͈͂̍ŏ��l
    [SerializeField] private Vector3 spawnAreaMax;     // �����͈͂̍ő�l
    [SerializeField] private float spawnInterval = 5f; // �����Ԋu�i�b�j
    [SerializeField] private float minDistanceBetweenObjects = 2f; // �I�u�W�F�N�g�Ԃ̍ŏ�����
    [SerializeField] private float minDistanceFromPlayer = 3f;     // �v���C���[�Ƃ̍ŏ�����

    public List<GameObject> spawnedObjects = new List<GameObject>(); // �X�|�[���ς݂̃I�u�W�F�N�g���L�^
    public bool spawnPossible;
    private List<int> spawnSteps = new List<int> { 3, 3, 3, 1, 3, 3, 1, 3, 3, 2 }; // �X�|�[����
    private int spawnIndex = 0; // ���݂̃X�e�b�v�̃C���f�b�N�X
    private float time;
    private int randam;

    void Start()
    {
        time = 0;
        SetRandomSpawnInterval(); // �ŏ��̃X�|�[���Ԋu��ݒ�
        spawnPossible = true;
    }

    void Update()
    {
        if (!spawnPossible)
        {
            return;
        }
        time += Time.deltaTime;

        if (time >= spawnInterval)
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
                    // �X�|�[������I�u�W�F�N�g������
                    GameObject objectToSpawnNow = SelectObjectToSpawn();

                    GameObject spawnedObject = Instantiate(objectToSpawnNow, randomPosition, Quaternion.identity);
                    spawnedObjects.Add(spawnedObject);

                    if (player != null)
                    {
                        Vector3 directionToPlayer = player.transform.position - randomPosition;
                        directionToPlayer.y = 0; // ���������݂̂�����ꍇ�AY���̉e���𖳎�
                        spawnedObject.transform.rotation = Quaternion.LookRotation(directionToPlayer);
                    }

                    time = 0;
                    SetRandomSpawnInterval(); // ���̃X�|�[���Ԋu��ݒ�
                    return;
                }
            }

            Debug.LogWarning("�K�؂ȃX�|�[���ʒu��������܂���ł���");
        }
    }

    private void SetRandomSpawnInterval()
    {
        spawnInterval = Random.Range(5f, 8f); // 5�b����8�b�̊ԂŃ����_��
    }


    private bool IsPositionValid(Vector3 position)
    {
        // �v���C���[�Ƃ̋������`�F�b�N
        if (player != null && Vector3.Distance(position, player.transform.position) < minDistanceFromPlayer)
        {
            return false; // �v���C���[�ɋ߂�����
        }

        // ���̃I�u�W�F�N�g�Ƃ̋������`�F�b�N
        foreach (GameObject obj in spawnedObjects)
        {
            if (Vector3.Distance(position, obj.transform.position) < minDistanceBetweenObjects)
            {
                return false; // ���̃I�u�W�F�N�g�Ƌ߂�����
            }
        }

        return true; // ���ׂĂ̏����𖞂����Ă���
    }

    private GameObject SelectObjectToSpawn()
    {
        // ���݂̃X�|�[���X�e�b�v���擾
        int currentStep = spawnSteps[spawnIndex];

        GameObject selectedObject;
        switch (currentStep)
        {
            case 1: // �z���2�`4���烉���_���ŃX�|�[��
                randam = (int)Random.Range(1, 4);
                if (randam == 1)
                {
                    for (int i = 0; i < 5; i++) // �ő�10��ʒu�����s
                    {
                        this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.red);
                    }
                }
                else if (randam == 2)
                {
                    for (int i = 0; i < 5; i++) // �ő�10��ʒu�����s
                    {
                        this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.green);
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++) // �ő�10��ʒu�����s
                    {
                        this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.blue);
                    }
                }
                selectedObject = objectToSpawn[randam];
                break;
            case 2: // �z���5�`7���烉���_���ŃX�|�[��
                randam = (int)Random.Range(4, 7);
                if (randam == 4)
                {
                    for (int i = 0; i < 8; i++) // �ő�10��ʒu�����s
                    {
                        this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.green);
                        this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.blue);
                    }
                }
                else if (randam == 5)
                {
                    for (int i = 0; i < 8; i++) // �ő�10��ʒu�����s
                    {
                        this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.blue);
                        this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.red);
                    }
                }
                else
                {
                    for (int i = 0; i < 8; i++) // �ő�10��ʒu�����s
                    {
                        this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.red);
                        this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.green);
                    }
                }
                selectedObject = objectToSpawn[randam];
                break;
            case 3: // �z���1�Ԗڂ��X�|�[��
                this.gameObject.GetComponent<MirrorSpown>().SpawnObject(Color.white);
                selectedObject = objectToSpawn[0];
                break;
            default:
                selectedObject = objectToSpawn[0];
                break;
        }

        // �X�|�[���C���f�b�N�X���X�V
        spawnIndex = (spawnIndex + 1) % spawnSteps.Count;

        return selectedObject;
    }


}
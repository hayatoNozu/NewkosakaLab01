using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    // �X�|�[������I�u�W�F�N�g�̃v���n�u��2��ސݒ�
    public GameObject[] objectPrefabs;

    // �X�|�[�����̈ʒu�i4�����j
    public Transform[] spawnPoints;

    // �I�u�W�F�N�g�̍ŏ�����
    public float minDistance = 2f;

    // ���݂̐����ς݃I�u�W�F�N�g�̈ʒu���X�g
    private List<Vector3> spawnedPositions = new List<Vector3>();

    // �X�|�[�����s�����\�b�h
    public void SpawnObject(Color mirrorColor)
    {
        if (spawnPoints.Length == 0 || objectPrefabs.Length == 0)
        {
            Debug.LogError("Spawn points or object prefabs are not set!");
            return;
        }

        // �����_���ɃX�|�[���ʒu��I��
        int randomPointIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenPoint = spawnPoints[randomPointIndex];

        // ���[�J�����W�ł�Z���I�t�Z�b�g�����肵�A�d�������
        Vector3 localOffset;
        Vector3 spawnPosition;
        int attempts = 0; // �������[�v�h�~�p
        const int maxAttempts = 100;

        do
        {
            localOffset = new Vector3(0, 0, Random.Range(-6f, 6f));
            spawnPosition = chosenPoint.TransformPoint(localOffset); // ���[�J�����W�����[���h���W�ɕϊ�
            attempts++;
        }
        while (!IsPositionValid(spawnPosition) && attempts < maxAttempts);

        // �ő厎�s�񐔂𒴂����ꍇ
        if (attempts >= maxAttempts)
        {
            Debug.LogWarning("Failed to find a valid position for spawning. Skipping spawn.");
            return;
        }

        // �����_���ɃI�u�W�F�N�g��I��
        int randomPrefabIndex = Random.Range(0, objectPrefabs.Length);
        GameObject chosenPrefab = objectPrefabs[randomPrefabIndex];

        // �I�u�W�F�N�g���C���X�^���X��
        GameObject spawnedObject = Instantiate(chosenPrefab, chosenPoint);
        spawnedObject.transform.localPosition = localOffset;

        Transform targetChild = spawnedObject.transform.Find("mirror");

        if (targetChild != null)
        {
            // �q�I�u�W�F�N�g�����������ꍇ�A����Renderer���擾���ă}�e���A����ύX
            Renderer childRenderer = targetChild.GetComponent<Renderer>();
            if (childRenderer != null)
            {
                childRenderer.material.color = mirrorColor; // �V�����F��K�p
            }
            else
            {
                Debug.LogWarning("Renderer not found on target child!");
            }
        }
        else
        {
            Debug.LogWarning($"Child with name mirror not found!");
        }

        // �V�����I�u�W�F�N�g�̈ʒu�����X�g�ɒǉ�
        spawnedPositions.Add(spawnPosition);
    }


    // �w�肳�ꂽ�ʒu�����̃I�u�W�F�N�g�Ə\������Ă��邩�m�F
    private bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 spawnedPosition in spawnedPositions)
        {
            if (Vector3.Distance(position, spawnedPosition) < minDistance)
            {
                return false;
            }
        }
        return true;
    }

    // �f�o�b�O�p�ɃL�[�ŃX�|�[�����g���K�[
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // �X�y�[�X�L�[�ŃX�|�[��
        {
            SpawnObject(Color.white);
        }
    }
}
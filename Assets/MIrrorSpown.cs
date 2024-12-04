using System.Collections.Generic;
using UnityEngine;

public class MirrorSpown : MonoBehaviour
{
    public GameObject[] objectPrefabs; // �X�|�[������I�u�W�F�N�g�̃v���n�u��2��ސݒ�
    public Transform[] spawnPoints;   // �X�|�[�����̈ʒu�i4�����j
    public float minDistance = 2f;    // �I�u�W�F�N�g�̍ŏ�����

    private List<GameObject> spawnedObjects = new List<GameObject>(); // ���݂̐����ς݃I�u�W�F�N�g�̃��X�g
    private int currentSpawnIndex = 0; // ���Ɏg�p����X�|�[���|�C���g�̃C���f�b�N�X

    public void SpawnObject(Color mirrorColor)
    {
        if (spawnPoints.Length == 0 || objectPrefabs.Length == 0)
        {
            Debug.LogError("Spawn points or object prefabs are not set!");
            return;
        }

        // ���ԂɃX�|�[���|�C���g��I��
        Transform chosenPoint = spawnPoints[currentSpawnIndex];
        currentSpawnIndex = (currentSpawnIndex + 1) % spawnPoints.Length; // �C���f�b�N�X���X�V���ă��[�v������

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

        if (attempts >= maxAttempts)
        {
            Debug.LogWarning("Failed to find a valid position for spawning. Skipping spawn.");
            return;
        }

        int randomPrefabIndex = Random.Range(0, objectPrefabs.Length);
        GameObject chosenPrefab = objectPrefabs[randomPrefabIndex];
        GameObject spawnedObject = Instantiate(chosenPrefab, chosenPoint);
        spawnedObject.transform.localPosition = localOffset;
        Transform targetChild = spawnedObject.transform.Find("mirror");

        if (targetChild != null)
        {
            Renderer childRenderer = targetChild.GetComponent<Renderer>();
            if (childRenderer != null)
            {
                childRenderer.material.color = mirrorColor;
            }
            else
            {
                Debug.LogWarning("Renderer not found on target child!");
            }

            string colorTag = GetColorTag(mirrorColor);
            spawnedObject.tag = colorTag;
            ApplyTagToChildren(spawnedObject.transform, colorTag);
        }
        else
        {
            Debug.LogWarning($"Child with name mirror not found!");
        }

        spawnedObjects.Add(spawnedObject); // �V�����I�u�W�F�N�g�����X�g�ɒǉ�
    }

    public void DestroyMirror(GameObject mirror)
    {
        // ���X�g����폜
        if (spawnedObjects.Remove(mirror))
        {
            Debug.Log($"Mirror {mirror.name} removed from the list.");
        }
        else
        {
            Debug.LogWarning($"Mirror {mirror.name} not found in the list.");
        }

        // ���ۂɃI�u�W�F�N�g��j��
        Destroy(mirror);
    }

    private string GetColorTag(Color color)
    {
        if (color == Color.red) return "red";
        if (color == Color.blue) return "blue";
        if (color == Color.green) return "green";
        if (color == Color.white) return "white";
        return null;
    }

    private void ApplyTagToChildren(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            child.tag = tag;
            ApplyTagToChildren(child, tag);
        }
    }

    private bool IsPositionValid(Vector3 position)
    {
        foreach (GameObject spawnedObject in spawnedObjects)
        {
            if (Vector3.Distance(position, spawnedObject.transform.position) < minDistance)
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnObject(Color.red);
        }
    }
}
using UnityEngine;

public class TimedObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // �o��������I�u�W�F�N�g
    public float spawnDelay; // 2�� = 120�b
    public float spawnFinish; // �X�|�[���I���܂ł̎���

    private float timer = 0f; // �o�ߎ��Ԃ��v������^�C�}�[

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnFinish)
        {
            // �X�|�[�����~
            this.gameObject.GetComponent<GhostSpawn>().spawnPossible = false;
        }
        if (timer >= spawnDelay)
        {
            objectToSpawn.SetActive(true);
        }
    }
}
using UnityEngine;

public class TimedObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // �o��������I�u�W�F�N�g
    public float spawnDelay; // 2�� = 120�b
    public float spawnFinish; // �X�|�[���I���܂ł̎���

    private float timer = 0f; // �o�ߎ��Ԃ��v������^�C�}�[

    public AudioSource bgm;
    public AudioSource resultBgm;

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
            //bgm.Stop();
            resultBgm.Play();
            objectToSpawn.SetActive(true);
        }
    }
}
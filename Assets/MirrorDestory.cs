using UnityEngine;

public class MirrorDestory : MonoBehaviour
{
    private float timer = 20f;
    private MirrorSpown mirrorSpown; // MirrorSpown�̎Q��

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // MirrorSpown���V�[������T���i�K�v�ɉ����ĕύX�j
        mirrorSpown = FindObjectOfType<MirrorSpown>();
        if (mirrorSpown == null)
        {
            Debug.LogError("MirrorSpown script not found in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (mirrorSpown != null)
            {
                // ���X�g����폜���Ă���I�u�W�F�N�g��j��
                mirrorSpown.DestroyMirror(gameObject);
            }
            else
            {
                // MirrorSpown��������Ȃ��ꍇ�A���ڔj��
                Destroy(gameObject);
            }
        }
    }
}
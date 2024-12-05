using UnityEngine;

public class Tutorial_2 : MonoBehaviour
{
    public Energy energyScript; // Energy �X�N���v�g�ւ̎Q��
    public GameObject target3;  // �\���������^�[�Q�b�g3
    public GameObject target4;  // �\���������^�[�Q�b�g4
    public GameObject target5;  // �\���������^�[�Q�b�g5
    public GameObject target6;  // �\���������^�[�Q�b�g6

    private bool target3Shown = false; // �^�[�Q�b�g3�\���ς݃t���O
    private bool target4Shown = false; // �^�[�Q�b�g4�\���ς݃t���O
    private bool target3HiddenAgain = false; // �^�[�Q�b�g3���ēx��\���ɂȂ������m�F
    private bool target4HiddenAgain = false; // �^�[�Q�b�g4���ēx��\���ɂȂ������m�F

    void Start()
    {
        // �^�[�Q�b�g3�A�^�[�Q�b�g4�A�^�[�Q�b�g5�A�^�[�Q�b�g6���ŏ��͔�\���ɐݒ�
        if (target3 != null)
        {
            target3.SetActive(false);
        }
        if (target4 != null)
        {
            target4.SetActive(false);
        }
        if (target5 != null)
        {
            target5.SetActive(false);
        }
        if (target6 != null)
        {
            target6.SetActive(false);
        }
    }

    void Update()
    {
        // �G�l���M�[��100�ł܂��^�[�Q�b�g3���\������Ă��Ȃ��ꍇ
        if (energyScript.energy >= 100 && !target3Shown)
        {
            if (target3 != null)
            {
                target3.SetActive(true); // �^�[�Q�b�g3��\��
            }
            target3Shown = true; // �t���O���X�V
        }

        // �G�l���M�[��100�ł܂��^�[�Q�b�g4���\������Ă��Ȃ��ꍇ
        if (energyScript.energy >= 100 && !target4Shown)
        {
            if (target4 != null)
            {
                target4.SetActive(true); // �^�[�Q�b�g4��\��
            }
            target4Shown = true; // �t���O���X�V
        }

        // �^�[�Q�b�g3����\���ɖ߂����ꍇ�A�^�[�Q�b�g5��\��
        if (target3 != null && !target3.activeSelf && target3Shown && !target3HiddenAgain)
        {
            if (target5 != null)
            {
                target5.SetActive(true); // �^�[�Q�b�g5��\��
            }
            target3HiddenAgain = true; // �t���O���X�V
        }

        // �^�[�Q�b�g4����\���ɖ߂����ꍇ�A�^�[�Q�b�g6��\��
        if (target4 != null && !target4.activeSelf && target4Shown && !target4HiddenAgain)
        {
            if (target6 != null)
            {
                target6.SetActive(true); // �^�[�Q�b�g6��\��
            }
            target4HiddenAgain = true; // �t���O���X�V
        }
    }
}
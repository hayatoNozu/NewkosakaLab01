using UnityEngine;

public class Tutorial_2 : MonoBehaviour
{
    public Energy energyScript; // Energy �X�N���v�g�ւ̎Q��
    public GameObject target2;  // �\���������^�[�Q�b�g2
    public GameObject target3;  // �\���������^�[�Q�b�g3

    private bool target2Shown = false; // �^�[�Q�b�g2�\���ς݃t���O
    private bool target2HiddenAgain = false; // �^�[�Q�b�g2���ēx��\���ɂȂ������m�F

    void Start()
    {
        // �^�[�Q�b�g2�ƃ^�[�Q�b�g3���ŏ��͔�\���ɐݒ�
        if (target2 != null)
        {
            target2.SetActive(false);
        }
        if (target3 != null)
        {
            target3.SetActive(false);
        }
    }

    void Update()
    {
        // �G�l���M�[��100�ł܂��^�[�Q�b�g2���\������Ă��Ȃ��ꍇ
        if (energyScript.energy >= 100 && !target2Shown)
        {
            if (target2 != null)
            {
                target2.SetActive(true); // �^�[�Q�b�g2��\��
            }
            target2Shown = true; // �t���O���X�V
        }

        // �^�[�Q�b�g2����\���ɖ߂����ꍇ�A�^�[�Q�b�g3��\��
        if (target2 != null && !target2.activeSelf && target2Shown && !target2HiddenAgain)
        {
            if (target3 != null)
            {
                target3.SetActive(true); // �^�[�Q�b�g3��\��
            }
            target2HiddenAgain = true; // �t���O���X�V
        }
    }
}
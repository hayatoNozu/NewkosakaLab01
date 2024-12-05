using UnityEngine;
using Valve.VR;

public class Tutorial_2 : MonoBehaviour
{
    public Energy energyScript; // Energy �X�N���v�g�ւ̎Q��
    public GameObject target2;  // �\���������^�[�Q�b�g2
    public GameObject target3;  // �\���������^�[�Q�b�g3

    private bool target2Shown = false; // �^�[�Q�b�g2�\���ς݃t���O

    private SteamVR_Action_Boolean triggerAction = SteamVR_Actions.default_InteractUI; // �g���K�[����
    private SteamVR_Input_Sources hand = SteamVR_Input_Sources.RightHand; // �g���K�[�̓��̓\�[�X

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

        // �g���K�[����������^�[�Q�b�g2���\���ɂ��A�^�[�Q�b�g3��\��
        if (target2 != null && target2.activeSelf && triggerAction.GetStateDown(hand))
        {
            target2.SetActive(false); // �^�[�Q�b�g2���\��
            if (target3 != null)
            {
                target3.SetActive(true); // �^�[�Q�b�g3��\��
            }
        }
    }
}
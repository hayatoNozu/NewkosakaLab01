using UnityEngine;
using Valve.VR;
using System;
using TMPro;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    private bool interactUI;
    private SteamVR_Action_Boolean Iui = SteamVR_Actions.default_InteractUI;
    public float energy = 100;
    public TextMeshProUGUI energyCount; // �e�L�X�g�ŃG�l���M�[�\��
    public Image energyGauge;          // �~�`�Q�[�W�� Image �R���|�[�l���g

    public GameObject handle;
    private float cumulativeAngle = 0f; // �ݐω�]�p�x
    private float lastAngle = 0f; // �O��t���[���̃n���h���p�x
    private bool triger;

    void Start()
    {
        // �n���h���̏����p�x���擾
        lastAngle = handle.transform.localEulerAngles.x;
    }

    void Update()
    {
        // �G�l���M�[�c�ʂ� 0 �����ɂȂ�Ȃ��悤����
        if (energy < 0)
        {
            energy = 0;
            this.gameObject.GetComponent<LaserReflector>().empty = true;
        }
        else if(energy > 100)
        {
            energy = 100;
        }
        else
        {
            this.gameObject.GetComponent<LaserReflector>().empty = false;
        }

        // �G�l���M�[�c�ʂ��e�L�X�g�ɔ��f
        energyCount.text = energy.ToString("0");

        // �G�l���M�[�c�ʂ��Q�[�W�ɔ��f (FillAmount �� 0.0�`1.0 �͈̔͂Őݒ�)
        energyGauge.fillAmount = energy / 100f;
        if (energy > 50f)
        {
            energyGauge.color = Color.green; // ��
        }
        else if (energy > 25f)
        {
            energyGauge.color = Color.yellow; // ���F
        }
        else
        {
            energyGauge.color = Color.red; // ��
        }

        // SteamVR �̃C���v�b�g�ŃG�l���M�[������
        interactUI = Iui.GetState(SteamVR_Input_Sources.RightHand);
        if (interactUI)
        {
            energy -= 0.1f;
            triger = true;
        }
        else
        {
            triger = false;
        }

        // �n���h���̊p�x���擾
        float currentAngle = handle.transform.localEulerAngles.x;

        // �p�x�̍����v�Z�i360�x�␳���l���j
        float angleDifference = Mathf.DeltaAngle(lastAngle, currentAngle);
        cumulativeAngle += Mathf.Abs(angleDifference);

        Debug.Log(cumulativeAngle);
        // �ݐϊp�x�� 360 �x�𒴂����ꍇ�ɃG�l���M�[����
        if (Mathf.Abs(cumulativeAngle) >= 360f)
        {
            if (!triger)
            {
                energy += 10;
            }

            
            cumulativeAngle = 0f; // �ݐϊp�x�����Z�b�g
        }

        // ���݂̊p�x��ۑ�
        lastAngle = currentAngle;
    }
}
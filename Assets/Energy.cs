using UnityEngine;
using Valve.VR;
using System;
using TMPro;
using UnityEngine.UI; // Image �R���|�[�l���g���������߂ɕK�v

public class Energy : MonoBehaviour
{
    private Boolean interactUI;
    private SteamVR_Action_Boolean Iui = SteamVR_Actions.default_InteractUI;
    private float energy = 100;
    public TextMeshProUGUI energyCount; // �e�L�X�g�ŃG�l���M�[�\��
    public Image energyGauge;          // �~�`�Q�[�W�� Image �R���|�[�l���g

    public GameObject handle;
    private bool chek0;
    /*
     �n���h����X�����擾
    ��Βl���P�W�O�ɂȂ�����G�l���M�[�{�P�O
    ��Βl���O�ɂȂ�܂ŏオ�ǂ܂�Ȃ�

     */
    void Start()
    {
        // �������������K�v�Ȃ炱���ɋL�q
    }

    void Update()
    {

        // �G�l���M�[�c�ʂ� 0 �����ɂȂ�Ȃ��悤����
        if (energy < 0)
        {
            energy = 0;
            this.gameObject.GetComponent<LaserReflector>().empty = true;
        }
        else
        {
            this.gameObject.GetComponent<LaserReflector>().empty = false;
        }

        // �G�l���M�[�c�ʂ��e�L�X�g�ɔ��f
        energyCount.text = energy.ToString("0");

        // �G�l���M�[�c�ʂ��Q�[�W�ɔ��f (FillAmount �� 0.0�`1.0 �͈̔͂Őݒ�)
        energyGauge.fillAmount = energy / 100f;
        if(energy > 50f)
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
        }

        float handleAngle = handle.transform.localEulerAngles.x;
        if(Mathf.Abs(handleAngle) >= 179&&chek0)
        {
            energy += 10;
            chek0 = false;
        }
        else if(Mathf.Abs(handleAngle)<=1)
        {
            chek0 = true;
        }

        Debug.Log(Mathf.Abs(handleAngle));
    }

    
}
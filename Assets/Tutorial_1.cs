using UnityEngine;

public class Tutorial_1 : MonoBehaviour
{
    public Energy energyScript; // Energy �X�N���v�g�ւ̎Q��
    public GameObject targetObject1; // ��\���ɂ���I�u�W�F�N�g
    public GameObject targetObject2;

    void Update()
    {
        // �G�l���M�[��100�̏ꍇ�A�ΏۃI�u�W�F�N�g���\���ɂ���
        if (energyScript.energy >= 100)
        {
            targetObject1.SetActive(false);
            targetObject2.SetActive(false);
        }
    }
}
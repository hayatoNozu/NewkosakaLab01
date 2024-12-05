using UnityEngine;
using System.Collections;

public class BlinkAndDisappear : MonoBehaviour
{
    public float blinkInterval = 0.5f; // �_�ł̊Ԋu�i�b�j
    public int blinkCount = 3; // �_�ł����

    public void StartCor()
    {
        // �_�ŏ������J�n
        StartCoroutine(BlinkAndDestroy());
    }

    private IEnumerator BlinkAndDestroy()
    {
        yield return new WaitForSeconds(blinkCount);
        gameObject.SetActive(false);
    }
}
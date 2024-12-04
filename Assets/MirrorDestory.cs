using UnityEngine;

public class MirrorDestory : MonoBehaviour
{
    private float timer = 20f;
    private MirrorSpown mirrorSpown; // MirrorSpownの参照

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // MirrorSpownをシーンから探す（必要に応じて変更）
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
                // リストから削除してからオブジェクトを破壊
                mirrorSpown.DestroyMirror(gameObject);
            }
            else
            {
                // MirrorSpownが見つからない場合、直接破壊
                Destroy(gameObject);
            }
        }
    }
}
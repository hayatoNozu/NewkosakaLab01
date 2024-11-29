using UnityEngine;
using System.Collections.Generic;
using Valve.VR;
using System;

public class LaserReflector : MonoBehaviour
{
    public GameObject laserPrefab;  // レーザーのPrefab
    public Transform laserStartPoint;  // レーザーのスタート位置を指定するTransform
    public int maxReflections = 5;  // 最大反射回数
    public float maxLaserDistance = 100f;  // 最大レーザー距離
    public LayerMask reflectLayerMask;  // 反射可能なレイヤー
    public Color[] laserColors;  // 反射ごとに使用する色
    private SteamVR_Action_Boolean Iui = SteamVR_Actions.default_InteractUI;
    private Boolean interacrtui;

    private List<GameObject> laserSegments = new List<GameObject>();  // 生成したレーザーオブジェクト

    [HideInInspector] public bool empty=false;
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        interacrtui = Iui.GetState(SteamVR_Input_Sources.RightHand);
        if (interacrtui&&!empty)
        {
            GenerateLaserPath();
        }
        else
        {
            ClearLaserSegments();
        }
    }

    void GenerateLaserPath()
    {
        ClearLaserSegments();

        // レーザーの開始位置を取得
        Vector3 startPosition = laserStartPoint != null ? laserStartPoint.position : transform.position;
        Vector3 direction = transform.forward;
        int reflections = 0;
        Color currentColor = laserColors[0];  // 最初のレーザーの色（デフォルト）

        while (reflections < maxReflections)
        {
            RaycastHit hit;
            if (Physics.Raycast(startPosition, direction, out hit, maxLaserDistance, reflectLayerMask))
            {
                // レーザーセグメントを生成
                List<Vector3> positions = new List<Vector3> { startPosition, hit.point };
                CreateLaserSegment(startPosition, hit.point, reflections, currentColor, positions);

                // コライダーに応じて色を変更
                currentColor = ChooseColorBasedOnCollider(hit.collider, currentColor);

                // 反射処理
                direction = Vector3.Reflect(direction, hit.normal);
                startPosition = hit.point;

                reflections++;
            }
            else
            {
                // 何にも当たらなかった場合
                List<Vector3> positions = new List<Vector3> { startPosition, startPosition + direction * maxLaserDistance };
                CreateLaserSegment(startPosition, startPosition + direction * maxLaserDistance, reflections, currentColor, positions);
                break;
            }
        }
    }

    void CreateLaserSegment(Vector3 start, Vector3 end, int reflectionIndex, Color currentColor, List<Vector3> posList)
    {
        GameObject laser = Instantiate(laserPrefab, Vector3.zero, Quaternion.identity);
        laser.tag = "laser";  // タグ設定

        // LineRendererを取得
        LineRenderer line = laser.GetComponent<LineRenderer>();
        line.positionCount = posList.Count;

        // LineRendererに座標を設定
        for (int iLoop = 0; iLoop < posList.Count; iLoop++)
        {
            line.SetPosition(iLoop, posList[iLoop]);
        }

        // BoxColliderを追加して調整
        BoxCollider boxCollider = laser.AddComponent<BoxCollider>();

        // レーザーの方向ベクトルを計算
        Vector3 direction = end - start;
        float length = direction.magnitude; // レーザーの長さ

        // レーザーの回転を設定
        laser.transform.position = start;
        laser.transform.LookAt(end);

        // コライダーの中心を設定 (ローカル空間での中心)
        boxCollider.center = new Vector3(0, 0, length / 2.0f);

        // コライダーのサイズを設定 (長さに沿ったスケール)
        boxCollider.size = new Vector3(0.1f, 0.1f, length);

        // LineRendererの設定
        line.startColor = currentColor;
        line.endColor = currentColor;
        line.SetPosition(0, start);
        line.SetPosition(1, end);

        // 作成したレーザーをリストに追加
        laserSegments.Add(laser);
    }

    Color ChooseColorBasedOnCollider(Collider collider, Color currentColor)
    {
        if (collider.CompareTag("Wall"))
        {
            if (currentColor == laserColors[2])
            {
                return laserColors[4];  // シアン
            }
            else if (currentColor == laserColors[3])
            {
                return laserColors[5];  // マゼンタ
            }
            return laserColors[1]; // 青
        }
        else if (collider.CompareTag("Wall2"))
        {
            if (currentColor == laserColors[1])
            {
                return laserColors[4];  // シアン
            }
            else if (currentColor == laserColors[3])
            {
                return laserColors[6];  // イエロー
            }
            return laserColors[2]; // 緑
        }
        else if (collider.CompareTag("Wall3"))
        {
            if (currentColor == laserColors[2])
            {
                return laserColors[6];  // イエロー
            }
            else if (currentColor == laserColors[1])
            {
                return laserColors[5];  // マゼンタ
            }
            return laserColors[3]; // 赤
        }
        return currentColor;
    }

    void ClearLaserSegments()
    {
        foreach (GameObject laser in laserSegments)
        {
            Destroy(laser);
        }
        laserSegments.Clear();
    }
}
using UnityEngine;
using System.Collections.Generic;
using Valve.VR;
using System;

public class LaserReflector : MonoBehaviour
{
    public GameObject laserPrefab;
    public int maxReflections = 5;
    public float maxLaserDistance = 100f;
    public LayerMask reflectLayerMask;
    public Color[] laserColors;

    private List<GameObject> laserSegments = new List<GameObject>();

    // 反射回数を記録する変数
    public int currentReflections { get; private set; } // 外部から読み取り専用
    private SteamVR_Action_Boolean Iui = SteamVR_Actions.default_InteractUI;
    private Boolean interacrtui;

    AudioSource audio;
    private bool se =true;

    [HideInInspector] public bool empty=false;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        interacrtui = Iui.GetState(SteamVR_Input_Sources.RightHand);
        if (interacrtui && !empty)
        {
            GenerateLaserPath();

            //bgm razer
            if (se)
            {
                PlayMusic();
                se = false; 
            }
            
        }
        else
        {
            StopMusic();
            se =true;
            ClearLaserSegments();
        }
    }

    void GenerateLaserPath()
    {
        ClearLaserSegments();

        Vector3 startPosition = transform.position;
        Vector3 direction = transform.forward;
        currentReflections = 0; // 反射回数をリセット
        Color currentColor = laserColors[0];

        while (currentReflections < maxReflections)
        {
            RaycastHit hit;
            if (Physics.Raycast(startPosition, direction, out hit, maxLaserDistance, reflectLayerMask))
            {
                List<Vector3> positions = new List<Vector3> { startPosition, hit.point };
                CreateLaserSegment(startPosition, hit.point, currentReflections, currentColor, positions);

                currentColor = ChooseColorBasedOnCollider(hit.collider, currentColor);

                direction = Vector3.Reflect(direction, hit.normal);
                startPosition = hit.point;

                currentReflections++; // 反射回数を増加
            }
            else
            {
                List<Vector3> positions = new List<Vector3> { startPosition, startPosition + direction * maxLaserDistance };
                CreateLaserSegment(startPosition, startPosition + direction * maxLaserDistance, currentReflections, currentColor, positions);
                break;
            }
        }

    }

    void PlayMusic()
    {
        if(audio != null)
        {
            audio.Play();
        }
    }

    void StopMusic()
    {
        audio.Stop();
    }

    void CreateLaserSegment(Vector3 start, Vector3 end, int reflectionIndex, Color currentColor, List<Vector3> posList)
{
    GameObject laser = Instantiate(laserPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    laser.tag = "laser";  // 最初に"laser"タグを設定
    laser.layer = LayerMask.NameToLayer("laserLayer");

    LineRenderer line = laser.GetComponent<LineRenderer>();
    line.positionCount = posList.Count;

    for (int iLoop = 0; iLoop < posList.Count; iLoop++)
    {
        line.SetPosition(iLoop, posList[iLoop]);
    }

    BoxCollider boxCollider = laser.AddComponent<BoxCollider>();
    boxCollider.isTrigger = true;

    Vector3 direction = end - start;
    float length = direction.magnitude;

    laser.transform.position = start;
    laser.transform.LookAt(end);

    boxCollider.center = new Vector3(0, 0, length / 2.0f);
    boxCollider.size = new Vector3(0.1f, 0.1f, length);

    line.startColor = currentColor;
    line.endColor = currentColor;
    line.SetPosition(0, start);
    line.SetPosition(1, end);

    // ここでレーザーのタグを色に基づいて変更
    laser.tag = GetLaserTagForColor(currentColor);

    laserSegments.Add(laser);
}

Color ChooseColorBasedOnCollider(Collider collider, Color currentColor)
{
    if (collider.CompareTag("blue"))
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
    else if (collider.CompareTag("green"))
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
    else if (collider.CompareTag("red"))
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

// 色に基づいてタグを決定
string GetLaserTagForColor(Color currentColor)
{
    if (currentColor == laserColors[0]) // 白
    {
        return "Wlaser";
    }
    else if (currentColor == laserColors[1]) // 青
    {
        return "Blaser";
    }
    else if (currentColor == laserColors[2]) // 緑
    {
        return "Glaser";
    }
    else if (currentColor == laserColors[3]) // 赤
    {
        return "Rlaser";
    }
    else if (currentColor == laserColors[4]) // シアン
    {
        return "Claser";
    }
    else if (currentColor == laserColors[5]) // マゼンタ
    {
        return "Mlaser";
    }
    else if (currentColor == laserColors[6]) // イエロー
    {
        return "Ylaser";
    }
    return "laser"; // デフォルトタグ
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

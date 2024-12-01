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
    public Material[] laserMaterials;

    private List<GameObject> laserSegments = new List<GameObject>();

    // 反射回数を記録する変数
    public int currentReflections { get; private set; } // 外部から読み取り専用
    private SteamVR_Action_Boolean Iui = SteamVR_Actions.default_InteractUI;
    private Boolean interacrtui;

    [HideInInspector] public bool empty=false;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
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

        Vector3 startPosition = transform.position;
        Vector3 direction = transform.forward;
        currentReflections = 0; // 反射回数をリセット
        Material currentMaterial = laserMaterials[0];

        while (currentReflections < maxReflections)
        {
            RaycastHit hit;
            if (Physics.Raycast(startPosition, direction, out hit, maxLaserDistance, reflectLayerMask))
            {
                List<Vector3> positions = new List<Vector3> { startPosition, hit.point };
                CreateLaserSegment(startPosition, hit.point, currentReflections, currentMaterial, positions);

                currentMaterial = ChooseMaterialBasedOnCollider(hit.collider, currentMaterial);

                direction = Vector3.Reflect(direction, hit.normal);
                startPosition = hit.point;

                currentReflections++; // 反射回数を増加
            }
            else
            {
                List<Vector3> positions = new List<Vector3> { startPosition, startPosition + direction * maxLaserDistance };
                CreateLaserSegment(startPosition, startPosition + direction * maxLaserDistance, currentReflections, currentMaterial, positions);
                break;
            }
        }
    }

    void CreateLaserSegment(Vector3 start, Vector3 end, int reflectionIndex, Material currentMaterial, List<Vector3> posList)
    {
        GameObject laser = Instantiate(laserPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        laser.tag = "laser";
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

        line.SetPosition(0, start);
        line.SetPosition(1, end);
        line.material = currentMaterial;

        laserSegments.Add(laser);
    }

    Material ChooseMaterialBasedOnCollider(Collider collider, Material currentMaterial)
    {
        if (collider.CompareTag("blue"))
        {
            if (currentMaterial == laserMaterials[2])
            {
                return laserMaterials[4];
            }
            else if (currentMaterial == laserMaterials[3])
            {
                return laserMaterials[5];
            }
            return laserMaterials[1];
        }
        else if (collider.CompareTag("green"))
        {
            if (currentMaterial == laserMaterials[1])
            {
                return laserMaterials[4];
            }
            else if (currentMaterial == laserMaterials[3])
            {
                return laserMaterials[6];
            }
            return laserMaterials[2];
        }
        else if (collider.CompareTag("red"))
        {
            if (currentMaterial == laserMaterials[2])
            {
                return laserMaterials[6];
            }
            else if (currentMaterial == laserMaterials[1])
            {
                return laserMaterials[5];
            }
            return laserMaterials[3];
        }
        return currentMaterial;
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

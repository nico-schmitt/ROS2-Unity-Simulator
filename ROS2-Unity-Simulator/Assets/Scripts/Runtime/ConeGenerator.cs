using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ConeGeneratorCSV : MonoBehaviour
{
    public TextAsset _csvFile;
    public GameObject _blueConePrefab;
    public GameObject _yellowConePrefab;
    public List<ConeInfo> _coneList {get; set;}

    public void GenerateTrackFromCSV(TextAsset csv)
    {
        _coneList = CSVReader.ReadCSV(csv);
        RemoveOldCones();
        InstanceNewCones();
    }

    public void RemoveOldCones()
    {
        for(int i = transform.childCount; i > 0; i--)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    public void InstanceNewCones()
    {
        foreach(ConeInfo cone in _coneList)
        {
            if(cone.color == "blue")
                Instantiate(_blueConePrefab, new Vector3(cone.x, 0, cone.z), Quaternion.identity, this.transform);
            else if(cone.color == "yellow")
                Instantiate(_yellowConePrefab, new Vector3(cone.x, 0, cone.z), Quaternion.identity, this.transform);
            else
                Debug.Log("Only blue and yellow accepted in csv");

        }
    }
}   

public readonly struct ConeInfo
{
    public ConeInfo(float x, float z, string color)
    {
        this.x = x;
        this.z = z;
        this.color = color;
    }

    public readonly float x;
    public readonly float z;
    public readonly string color;
}

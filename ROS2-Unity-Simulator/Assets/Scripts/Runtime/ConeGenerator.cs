using System;
using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;

[ExecuteInEditMode]
public class ConeGeneratorCSV : MonoBehaviour
{
    public bool skipColumnNamesRow = false;
    public TextAsset csvFile;
    public GameObject blueConePrefab;
    public GameObject yellowConePrefab;
    public GameObject noColorSpecifiedPrefab;
    public List<ConeInfo> coneList {get; set;}

    public void GenerateTrackFromCSV(TextAsset csv)
    {
        coneList = CSVReader.ReadCSV(csv, skipColumnNamesRow);
        RemoveOldCones();
        InstanceNewCones();
        OffsetTrack();
    }

    public void RemoveOldCones()
    {
        for(int i = transform.GetChild(0).transform.childCount; i > 0; i--)
        {
            DestroyImmediate(transform.GetChild(0).transform.GetChild(0).gameObject);
        }
    }

    public void InstanceNewCones()
    {
        foreach(ConeInfo cone in coneList)
        {
            if(cone.color == "blue")
                Instantiate(blueConePrefab, new Vector3(cone.x, 0, cone.z), Quaternion.identity, this.transform.GetChild(0));
            else if(cone.color == "yellow")
                Instantiate(yellowConePrefab, new Vector3(cone.x, 0, cone.z), Quaternion.identity, this.transform.GetChild(0));
            else
                Instantiate(noColorSpecifiedPrefab, new Vector3(cone.x, 0, cone.z), Quaternion.identity, this.transform.GetChild(0));
        }
    }

    public void OffsetTrack()
    {
        Debug.Log(csvFile.name);
        switch(csvFile.name)
        {
            case "skidpad_track":
                transform.GetChild(0).position = new Vector3(0,0,16.5f);
                break;
            case "accel_track":
                transform.GetChild(0).position = new Vector3(53f,0,0);
                break;
            case "FSG":
                transform.GetChild(0).position = new Vector3(0,0,0);
                break;
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

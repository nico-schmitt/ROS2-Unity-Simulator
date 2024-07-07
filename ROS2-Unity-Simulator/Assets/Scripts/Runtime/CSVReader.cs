using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public static List<ConeInfo> ReadCSV(TextAsset csv, bool skipColumnNamesRow)
        {
            string csvText = csv.text;
            List<ConeInfo> coneList = new List<ConeInfo>();

            string[] csvLine = csvText.Split('\n');

            ICoordinateSpace coordinateSpace = new FRD();

            int startOffset = skipColumnNamesRow ? 1 : 0;
            for(int i = startOffset; i < csvLine.Length-1; i++)
            {
                string[] csvValues = csvLine[i].Split(',');

                if(csvValues.Length < 3)
                {
                    Debug.LogError($"CSV line {i} does not have enough values");
                    continue;
                }


                if(float.TryParse(csvValues[0], out float x) && 
                    float.TryParse(csvValues[1], out float z))
                {
                    string color = csvValues[2];
                    Vector3 toUnitySpaceConvertedPos = coordinateSpace.ConvertToRUF(new Vector3(x,0,z));
                    ConeInfo coneInfo = new ConeInfo(x, z, color);
                    //ConeInfo coneInfo = new ConeInfo(toUnitySpaceConvertedPos.x, toUnitySpaceConvertedPos.z, color);
                    coneList.Add(coneInfo);
                }
                else
                {
                    Debug.LogError($"CSV line {i} contains invalid float format");
                }


                // if(float.TryParse(csvValues[0], out var a))
                //     Debug.Log("yap");
                // else
                //     Debug.Log("no");
                //float a = float.Parse(csvValues[0]);
                //Debug.Log(typeof(a));
            }
            return coneList;
        }
}

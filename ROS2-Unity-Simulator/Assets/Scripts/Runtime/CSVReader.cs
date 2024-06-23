using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public static List<ConeInfo> ReadCSV(TextAsset csv)
        {
            string csvText = csv.text;
            List<ConeInfo> coneList = new List<ConeInfo>();

            string[] csvLine = csvText.Split('\n');
            
            for(int i = 1; i < csvLine.Length-1; i++) // Start at 1 to skip column names
            {
                string[] csvValues = csvLine[i].Split(',');


                if(i == 72)
                {
                    Debug.Log($"x:{csvValues[0]}, z:{csvValues[1]}, color:{csvValues[2]}");
                }



                if(csvValues.Length < 3)
                {
                    Debug.LogError($"CSV line {i} does not have enough values");
                    continue;
                }

                if(float.TryParse(csvValues[0], out float x) && 
                    float.TryParse(csvValues[1], out float z))
                {
                    string color = csvValues[2];
                    ConeInfo coneInfo = new ConeInfo(x, z, color);
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

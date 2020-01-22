using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;

public class CSVLogger : MonoBehaviour
{
    public string path;
    public StringBuilder csv_string;
    public int condition;
    public int probandId;

    public void Start()
    {
        path = "ID"+ probandId + "_CON" + condition + "_" + DateTime.Now.ToString("dd.MM.yyyy-HH_mm_ss") + ".csv";
        csv_string = new StringBuilder();
    }

    public void BeeSpawn(int id)
    {
        csv_string.AppendLine($"{id}, 1, {Time.timeSinceLevelLoad}");
    }

    public void BeeDeath(int id)
    {
// csv_string.AppendLine($"{id}, 2, {DateTime.Now.ToString("HH:mm:ss:fff")}");
        csv_string.AppendLine($"{id}, 2, {Time.timeSinceLevelLoad}");
    }

    public void BeeHit(int id)
    {
        csv_string.AppendLine($"{id}, 3, {Time.timeSinceLevelLoad}");
    }

    public void SaveToFile()
    {
        File.AppendAllText(path, csv_string.ToString());
    }

    public void Update() {
         
    }
}
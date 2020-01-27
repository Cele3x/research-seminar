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
        csv_string.AppendLine($"probandId; condition; beeId; result; startTime; endTime");

    }

    public void BeeDied(int id, string spawnTime)
    {
        csv_string.AppendLine($"{probandId};{condition};{id}; 1; {spawnTime}; {DateTime.Now.ToString("HH:mm:ss.fff")}");
        //Debug.Log($"Bee {id} died");
    }

    public void BeeHit(int id, string spawnTime)
    {
        csv_string.AppendLine($"{probandId};{condition};{id}; 2; {spawnTime}; {DateTime.Now.ToString("HH:mm:ss.fff")}");
        //Debug.Log($"Bee {id} hit you");
    }

    public void SaveToFile()
    {
        File.AppendAllText(path, csv_string.ToString());
    }

    public void Update() {
         
    }
}
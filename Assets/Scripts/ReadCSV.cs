using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadCSV : MonoBehaviour
{
    [SerializeField] private GameObject sandDot;
    [SerializeField] private Transform testCSV;

    private float smallestX;
    private float largestX;
    private float smallestY;
    private float largestY;
    private float smallestZ;
    private float largestZ;
    void Start()
    {
        //readCSVFile("Assets/Scripts/test_02.csv");
        //readCSVFile("Assets/Scripts/test_03.csv");
        readCSVFile("Assets/Scripts/export_test.csv");
    }

    void readCSVFile(string path)
    {
        StreamReader strReader = new StreamReader(path);
        bool endOfFile = false;

        while (!endOfFile)
        {
            string data_string = strReader.ReadLine();
            if (data_string == null)
            {
                endOfFile = true;
                break;
            }
            else if (data_string.Contains("PosX"))
            {
                continue;
            }
            var data_values = data_string.Split(',');
            
            float x = float.Parse(data_values[1], System.Globalization.CultureInfo.InvariantCulture) / 100f;
            float y = float.Parse(data_values[2], System.Globalization.CultureInfo.InvariantCulture) / 100f;
            float z = float.Parse(data_values[3], System.Globalization.CultureInfo.InvariantCulture) / 100f;

            Vector3 sandDotLoc = new Vector3(x, y, z);
            GameObject sandDotClone = Instantiate(sandDot, sandDotLoc, Quaternion.identity);
            sandDotClone.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            sandDotClone.transform.parent = testCSV;

            //Debug.Log(x.ToString() + " " + y.ToString() + " " + z.ToString());
        }
    }
}

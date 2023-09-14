using System;
using System.IO;
using UnityEngine;

public class DataFile : MonoBehaviour
{
    private string _docPath;

    private void Start()
    {
        WriteFile();
    }

    private void WriteFile()
    {
        string[] vertices =
        {
            "0 12 0",
            "56 8 0",
            "0 0.5 56",
            "56 9 56",
            "0 0.5 112",
            "56 11.5 112"
        };

        string[] triangleInfo =
        {
            "0 1 2  1 -1 -1",
            "1 3 2  0 -1 2",
            "2 3 5  1 -1 3",
            "2 5 4  2 -1 -1"
        };

        _docPath = "E://Unity Games//Ball Simulation - VisSim Comp 2";

        using StreamWriter outputFile = new StreamWriter(Path.Combine(_docPath, "DataFile.txt"));
        
        // Adding the strings into the txt file
        {
            foreach (string vertex in vertices)
                outputFile.WriteLine(vertex);

            outputFile.WriteLine(string.Empty);

            foreach (string triangle in triangleInfo)
                outputFile.WriteLine(triangle);
        }
    }

    private void ReadFile()
    {
        
    }
}

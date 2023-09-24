using System;
using System.IO;
using UnityEngine;

public class DataFile : MonoBehaviour
{
    private string _docPath;
    public string[] data;

    private void Start()
    {
        _docPath = Environment.CurrentDirectory + "//DataFile.txt";
        
        WriteFile();
        //ReadFile();
    }

    private void WriteFile()
    {
        string[] vertices =
        {
            "0 12 0",
            "0 0.5 56",
            "0 0.5 112",
            "56 8 0",
            "56 9 56",
            "56 11.5 112"
        };

        string[] indices =
        {
            "0 1 3",
            "1 4 3",
            "1 5 4",
            "1 2 5"
        };

        string[] neighbours =
        {
            "-1 1 -1",
            "2 -1 0",
            "3 -1 1",
            "-1 -1 2"
        };
        
        using StreamWriter outputFile = new StreamWriter(Path.Combine(_docPath));
        
        // Adding the strings into the txt file
        {
            foreach (var vertex in vertices)
                outputFile.WriteLine(vertex);

            outputFile.WriteLine(string.Empty);

            foreach (var index in indices)
                outputFile.WriteLine(index);
            
            outputFile.WriteLine(string.Empty);

            foreach (var neighbour in neighbours)
                outputFile.WriteLine(neighbour);
        }
    }

    private void ReadFile()
    {
        int i = 0;
        
        using var sr = new StreamReader(_docPath);
        while (sr.Peek() >= 0)
        {
            //data[i] = sr.ReadLine();
            //Debug.Log(data[i]);
            i++;
        }
    }
}

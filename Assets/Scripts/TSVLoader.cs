using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TSVLoader : MonoBehaviour
{
    public List<Story> AllStories = new List<Story>();
    public TextAsset tsvFile;                             // Assign in Unity Inspector with text file export from Excel Data Set

    void Awake()                       // Pre Update()
    {
        if (tsvFile == null)      // For Debugging
        {
            Debug.LogError("TSV file not assigned in the inspector.");
            return;
        }
        LoadTSV();      // Load File
    }

    void LoadTSV()
    {
        string[] lines = tsvFile.text.Split('\n');           // Split the text into lines at new line i.e. new row from Excel

        for (int i = 1; i < lines.Length; i++)               // skip header row that contains column titles
        {
            string[] fields = lines[i].Split('\t');          // Use TAB as delimiter for each line
            if (fields.Length < 24) continue;

            int number = i; // Use the line number as the story number
            string headline = fields[0];
            string body = fields[1];
            string author = fields[2];
            string date = fields[3];

            float[] scores = new float[20];
            for (int j = 0; j < 20; j++)
            {
                float.TryParse(fields[4 + j], out scores[j]);
            }

            AllStories.Add(new Story(number, headline, body, author, date, scores));
        }
    }
}

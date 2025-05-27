using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVLoader : MonoBehaviour
{
    public List<Story> AllStories = new List<Story>();
    public TextAsset csvFile;

    void Awake()
    {
        LoadCSV();
    }

    void LoadCSV()
    {
        string[] lines = csvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++) // skip header
        {
            string[] fields = lines[i].Split('\t'); // Use ';' as delimiter
            if (fields.Length < 24) continue;

            string headline = fields[0];
            string body = fields[1];
            string author = fields[2];
            string date = fields[3];

            float[] scores = new float[20];
            for (int j = 0; j < 20; j++)
            {
                float.TryParse(fields[4 + j], out scores[j]);
            }

            AllStories.Add(new Story(headline, body, author, date, scores));
        }
    }
}

// Class to contain story information

using System;

[Serializable]
public class Story
{
    public int Number;
    public string Headline;
    public string Body;
    public string Author;
    public string Date;
    public float[] CategoryScores = new float[20];

    public int TimesShown = 0;
    public bool Clicked = false;

    // Constructor to initialize a story with all fields - Used in TSVLoader to load stories from the TSV file
    public Story(int number, string headline, string body, string author, string date, float[] scores)
    {
        Number = number;
        Headline = headline;
        Body = body;
        Author = author;
        Date = date;
        CategoryScores = scores;
    }
}

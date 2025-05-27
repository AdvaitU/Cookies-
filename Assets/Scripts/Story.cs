using System;

[Serializable]
public class Story
{
    public string Headline;
    public string Body;
    public string Author;
    public string Date;
    public float[] CategoryScores = new float[20];

    public int TimesShown = 0;
    public bool Clicked = false;

    public Story(string headline, string body, string author, string date, float[] scores)
    {
        Headline = headline;
        Body = body;
        Author = author;
        Date = date;
        CategoryScores = scores;
    }
}

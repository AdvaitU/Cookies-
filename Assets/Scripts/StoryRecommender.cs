using System.Collections.Generic;
using UnityEngine;

public class StoryRecommender : MonoBehaviour
{
    public UserProfile userProfile;
    public CSVLoader storyLoader;

    public List<Story> Test;

    public void Start()
    {
        Test = RecommendTop5();
    }
    public List<Story> RecommendTop5()
    {
        List<Story> candidates = new List<Story>();

        foreach (var story in storyLoader.AllStories)
        {
            if (story.Clicked || story.TimesShown >= 3)
                continue;

            candidates.Add(story);
        }

        candidates.Sort((a, b) =>
            CosineSimilarity(userProfile.Preferences, b.CategoryScores)
            .CompareTo(CosineSimilarity(userProfile.Preferences, a.CategoryScores))
        );

        List<Story> top5 = candidates.GetRange(0, Mathf.Min(5, candidates.Count));
        foreach (var s in top5) s.TimesShown++;

        return top5;
    }

    float CosineSimilarity(float[] a, float[] b)
    {
        float dot = 0f, magA = 0f, magB = 0f;
        for (int i = 0; i < a.Length; i++)
        {
            dot += a[i] * b[i];
            magA += a[i] * a[i];
            magB += b[i] * b[i];
        }

        float denom = Mathf.Sqrt(magA) * Mathf.Sqrt(magB);
        return denom > 0f ? dot / denom : 0f;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class StoryRecommender : MonoBehaviour
{
    public UserProfile userProfile;
    public TSVLoader storyLoader; // Or CSVLoader if you're still using that

    /// <summary>
    /// Recommends the top 'n' stories based on cosine similarity to the user's preferences.
    /// Stories already clicked or shown more than 3 times will be excluded.
    /// Increments TimesShown for returned stories.
    /// </summary>
    public List<Story> RecommendStories(int n)
    {
        if (userProfile == null || storyLoader == null)
        {
            Debug.LogError("Missing references in StoryRecommender.");
            return new List<Story>();
        }

        List<Story> eligible = new List<Story>();

        foreach (Story story in storyLoader.AllStories)
        {
            if (!story.Clicked && story.TimesShown < 3)
            {
                eligible.Add(story);
            }
        }

        eligible.Sort((a, b) =>
            CosineSimilarity(userProfile.Preferences, b.CategoryScores)
            .CompareTo(CosineSimilarity(userProfile.Preferences, a.CategoryScores))
        );

        int count = Mathf.Min(n, eligible.Count);
        List<Story> topStories = eligible.GetRange(0, count);

        foreach (Story s in topStories)
            s.TimesShown++;

        return topStories;
    }

    /// <summary>
    /// Computes cosine similarity between two 20-dimensional vectors.
    /// </summary>
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

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

    public Story LoadRandomStory()
    {
        if (storyLoader == null || storyLoader.AllStories.Count == 0)
        {
            Debug.LogError("No stories available to load.");
            return null;
        }
        int randomIndex = Random.Range(0, storyLoader.AllStories.Count);
        Story randomStory = storyLoader.AllStories[randomIndex];
        randomStory.TimesShown++; // Increment TimesShown for the loaded story
        return randomStory;
    }

    public List<Story> LoadRandomStories(int numberOfStories)
    {

        List<Story> randomStories = new List<Story>();
        for (int j = 0; j < numberOfStories; j++)
        {
            Story randomStory = LoadRandomStory();
            if (randomStory != null)
            {
                randomStories.Add(randomStory);
            }
        }
        return randomStories;
    }
}

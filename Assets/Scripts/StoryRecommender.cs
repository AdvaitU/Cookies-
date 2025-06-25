using System.Collections.Generic;
using UnityEngine;

public class StoryRecommender : MonoBehaviour
{
    public UserProfile userProfile;
    public TSVLoader storyLoader; 

    // Recommends the top 'n' stories based on cosine similarity to the user's preferences.
    // Stories already clicked or shown more than 3 times will be excluded.
    // Increments TimesShown for returned stories.

    public List<Story> RecommendStories(int n)
    {
        // Throws
        if (userProfile == null || storyLoader == null)
        {
            Debug.LogError("Missing references in StoryRecommender.");
            return new List<Story>();
        }

        // Temporary list to contain 'eligible' stories
        List<Story> eligible = new List<Story>();

        // Put all stories that have not been clicked and shown less than 3 times into the eligible list
        foreach (Story story in storyLoader.AllStories)
        {
            if (!story.Clicked && story.TimesShown < 3)
            {
                eligible.Add(story);
            }
        }

        // Sort them by cosine similarity to the user's preferences
        eligible.Sort((a, b) =>
            CosineSimilarity(userProfile.Preferences, b.CategoryScores)
            .CompareTo(CosineSimilarity(userProfile.Preferences, a.CategoryScores))
        );

        int count = Mathf.Min(n, eligible.Count);             // Safeguard - Is the number of stories requested lower or the number of eligible? --> Choose lower of the two
        List<Story> topStories = eligible.GetRange(0, count); // Get the top 'count' stories

        foreach (Story s in topStories)                       // Increment TimesShown for each story in the top stories
            s.TimesShown++;

        return topStories;     // Return as list of stories
    }

    // Computes cosine similarity between two 20-dimensional vectors - Picked off the internet
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

    // Load one random story from the loader.
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

    // Wrapper to load multiple random stories.
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

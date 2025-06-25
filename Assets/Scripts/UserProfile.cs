// Summary: Defines the UserProfile class that manages user preferences and coffee recommendations based on story scores.

using UnityEngine;

public class UserProfile : MonoBehaviour
{
    public float[] Preferences = new float[20];

    void Awake()                      // Called in tandem with TSVLoader in the Awake() method for the level to set everything up for the Start() functions
    {
        InitialisePreferences();      // Randomly initialise preferences to a value between 0 and 10
    }

    // Initialisation of preferences. If neutral is true, all preferences are set to 5 (neutral) - False by default.
    public void InitialisePreferences(bool neutral = false)
    {
        if (neutral)
        {
            for (int i = 0; i < Preferences.Length; i++)
                Preferences[i] = 5f; // Neutral preference
        }
        else
        {
            for (int i = 0; i < Preferences.Length; i++)
                Preferences[i] = Random.Range(0.0f, 10.0f); // Random preference
        }
    }

    // Update preferences based on story scores - Called on Headline Click
    public void UpdatePreferences(float[] storyScores, float weight = 0.5f)
    {
        for (int i = 0; i < Preferences.Length; i++)
        {
            float delta = Mathf.Clamp((storyScores[i] - 5f) / 2.5f, -2f, 2f);
            Preferences[i] += delta * weight;
            Preferences[i] = Mathf.Clamp(Preferences[i], 0f, 10f);
        }
    }

    // Update a single preference based on column number and modifier
    public void UpdateSingularPreference(int columnNumber, int modifier)
    {
        Preferences[columnNumber] += modifier;
    }


    // Coffee Recommendation Methods ------------------------------------------------------------
    public float GetBoldness()
    {
        // Example: Politics, Foreign Affairs, Activism
        return (Preferences[1] + Preferences[3] + Preferences[18]) / 3f * 10f;
    }

    public float GetComplexity()
    {
        // Example: Science, Tech, Opinion, Arts
        return (Preferences[5] + Preferences[6] + Preferences[11] + Preferences[17]) / 4f * 10f;
    }

    public float GetBitterness()
    {
        // Example: Finance, Law, World News
        return (Preferences[2] + Preferences[7] + Preferences[19]) / 3f * 10f;
    }

}

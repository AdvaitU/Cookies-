using UnityEngine;

public class UserProfile : MonoBehaviour
{
    public float[] Preferences = new float[20];

    void Awake()
    {
        // Initialize preferences to 5.0 (neutral)
        for (int i = 0; i < Preferences.Length; i++)
            Preferences[i] = 5f;
    }

    public void UpdatePreferences(float[] storyScores, float weight = 0.5f)
    {
        for (int i = 0; i < Preferences.Length; i++)
        {
            float delta = Mathf.Clamp((storyScores[i] - 5f) / 2.5f, -2f, 2f);
            Preferences[i] += delta * weight;
            Preferences[i] = Mathf.Clamp(Preferences[i], 0f, 10f);
        }
    }

    public void UpdateSingularPreference(int columnNumber, int modifier)
    {
        Preferences[columnNumber] += modifier;
    }


    // Coffee Recommendation Methods
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

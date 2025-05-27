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
}

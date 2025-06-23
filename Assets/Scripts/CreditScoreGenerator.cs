using System.Collections.Generic;
using UnityEngine;

public class CreditScoreGenerator : MonoBehaviour
{
    public UserProfile userProfile;

    [Range(300, 850)] public int RawCreditScore;
    [Range(0f, 1f)] public float NormalizedScore;
    public List<string> ScoreReasons = new List<string>();

    private readonly int minScore = 300;
    private readonly int maxScore = 850;
    private readonly float capPercentage = 0.7f; // 70% cap, i.e. 595 max

    //UI Elements
    public TMPro.TMP_Text scoreText;
    public TMPro.TMP_Text reasonText;
    public DialController dialController;

    private readonly string[] reasonBank = new string[]
    {
        "Correlated browsing habits with high-risk financial behavior",
        "Temporal cluster detected in preference shifts",
        "Engagement pattern matches low-trust activity archetype",
        "Inverse similarity to preferred user segment: 'Reliable Spenders'",
        "Detected lack of algorithmic consistency over time",
        "Low entropy score in information consumption habits",
        "Similarity to demographically unstable cluster (DS-2X)",
        "Weighted volatility across key interest segments",
        "Under-indexing in fiscally responsible content categories",
        "Excessive leaning toward entertainment over finance content",
        "Consumption bias aligned with impulsivity markers",
        "Profile drift indicates non-linear decision heuristics",
        "Flagged asymmetry in civic vs. personal interest scores",
        "Suboptimal engagement pacing across high-trust categories",
        "Disproportionate interest in satire over hard news",
        "Interaction heatmap deviates from recommended thresholds",
        "Non-conformant semantic weight in recent browsing sequence",
        "Preference echo pattern diverges from high-score baseline",
        "Affinity score matches prototype cluster 'Dissonant Dreamers'",
        "Inconsistent topic granularity suggests attention volatility"
    };

    public void PublishScore()
    {
        GenerateScore();
        scoreText.text = RawCreditScore.ToString();
        reasonText.text = string.Join("\n", ScoreReasons);
        dialController.SetDialValue(RawCreditScore);

    }
    public void GenerateScore()
    {
        float userEngagement = CalculateUserScoreFromPreferences();
        float cappedMaxScore = maxScore * capPercentage;

        RawCreditScore = Mathf.RoundToInt(minScore + userEngagement * (cappedMaxScore - minScore));
        NormalizedScore = (RawCreditScore - minScore) / (cappedMaxScore - minScore);

        GenerateReasons();
    }

    private float CalculateUserScoreFromPreferences()
    {
        // Example: average across Finance, Law, Science, World News
        float[] prefs = userProfile.Preferences;

        float score = (prefs[2] + prefs[7] + prefs[5] + prefs[19]) / 4f;
        return Mathf.Clamp01(score / 10f);
    }

    private void GenerateReasons()
    {
        ScoreReasons.Clear();
        List<int> usedIndices = new List<int>();

        int reasonCount = Random.Range(3, 5); // 3 or 4 reasons

        while (ScoreReasons.Count < reasonCount)
        {
            int index = Random.Range(0, reasonBank.Length);
            if (!usedIndices.Contains(index))
            {
                ScoreReasons.Add(reasonBank[index]);
                usedIndices.Add(index);
            }
        }
    }
}

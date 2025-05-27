using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Only if using TextMeshPro

public class StoryUIManager : MonoBehaviour
{
    public GameObject storyItemPrefab;
    public Transform storyListParent; // The container (e.g., Vertical Layout Group)
    public StoryRecommender recommender;
    public UserProfile userProfile;

    public List<GameObject> currentItems = new List<GameObject>();

    public void Start()
    {
        ShowTop5Headlines();
    }
    public void ShowTop5Headlines()
    {
        ClearOldHeadlines();

        List<Story> top5 = recommender.RecommendTop5();

        foreach (Story story in top5)
        {
            GameObject item = Instantiate(storyItemPrefab, storyListParent);
            TMP_Text textComponent = item.GetComponentInChildren<TMP_Text>(); // or TMP_Text if using TextMeshPro
            textComponent.text = story.Headline;

            Button btn = item.GetComponent<Button>();
            btn.onClick.AddListener(() => OnStoryClicked(story));

            currentItems.Add(item);
        }
    }

    void ClearOldHeadlines()
    {
        foreach (var obj in currentItems)
        {
            Destroy(obj);
        }
        currentItems.Clear();
    }

    void OnStoryClicked(Story story)
    {
        story.Clicked = true;
        userProfile.UpdatePreferences(story.CategoryScores);

        // TODO: Display full story page
        Debug.Log("Clicked on: " + story.Headline);
    }
}

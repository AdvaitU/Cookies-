using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Only if using TextMeshPro

[System.Serializable]
public struct MainStoryUI
{
    public TMP_Text headlineText;
    public TMP_Text bodyText;
    public TMP_Text authorText;
    public TMP_Text dateText;
}

public class StoryUIManager : MonoBehaviour
{
    public StoryRecommender recommender;
    public UserProfile userProfile;
    public MainStoryUI mainStoryUI;
    public TMP_Text[] recommendedStories = new TMP_Text[5];
    public TMP_Text[] recommendedStoryAuthors = new TMP_Text[5];


    public List<Story> selectedStories = new List<Story>();
    public Story mainStory;

    private string[] days = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

    public void Start()
    {
        SelectRandomMainStory();                            // Publish First Main Story
        LoadNextStories();                          // Load Recommended Stories
    }

    public void LoadNextStories()
    {
        RefreshMainStory(); // Refresh Main Story UI
        selectedStories = recommender.RecommendStories(5);      // Get 5 Recommended Stories
        for (int i = 0; i < selectedStories.Count; i++)
        {
            recommendedStories[i].text = selectedStories[i].Headline;   // Display Recommended Stories
            recommendedStoryAuthors[i].text = selectedStories[i].Author; // Display Authors of Recommended Stories
        }
    }

    public void RefreshMainStory()
    {
        if (mainStory != null)
        {
            mainStoryUI.headlineText.text = mainStory.Headline;
            mainStoryUI.bodyText.text = mainStory.Body;
            mainStoryUI.authorText.text = "by " + mainStory.Author;
            mainStoryUI.dateText.text = days[Random.Range(0, 6)] + ", " + mainStory.Date + " " + System.DateTime.Now.TimeOfDay.Hours + ":" + System.DateTime.Now.TimeOfDay.Minutes + " BST"; // Random day for demo purposes
        }
    }
    public void SelectRandomMainStory()
    {
        mainStory = recommender.storyLoader.AllStories[Random.Range(0, recommender.storyLoader.AllStories.Count)];
        mainStoryUI.headlineText.text = mainStory.Headline;
        mainStoryUI.bodyText.text = mainStory.Body;
        mainStoryUI.authorText.text = "by " + mainStory.Author;
        mainStoryUI.dateText.text = days[Random.Range(0,6)] + ", " + mainStory.Date + " " + Time.time.ToString("HH:mm") +" BST";// Random day for demo purposes
    }

    
    //public void ShowTop5Headlines()
    //{
    //    ClearOldHeadlines();

    //    List<Story> top5 = recommender.RecommendStories(5);

    //    foreach (Story story in top5)
    //    {
    //        GameObject item = Instantiate(storyItemPrefab, storyListParent);
    //        TMP_Text textComponent = item.GetComponentInChildren<TMP_Text>(); // or TMP_Text if using TextMeshPro
    //        textComponent.text = story.Headline;

    //        Button btn = item.GetComponent<Button>();
    //        btn.onClick.AddListener(() => OnStoryClicked(story));

    //        currentItems.Add(item);
    //    }
    //}

    //void ClearOldHeadlines()
    //{
    //    foreach (var obj in currentItems)
    //    {
    //        Destroy(obj);
    //    }
    //    currentItems.Clear();
    //}

    //void OnStoryClicked(Story story)
    //{
    //    story.Clicked = true;
    //    userProfile.UpdatePreferences(story.CategoryScores);

    //    // TODO: Display full story page
    //    Debug.Log("Clicked on: " + story.Headline);
    //}
}

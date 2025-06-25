// Summary: This mainly manages the UI for Screen 1 - Since it contains the most number of UI elements that are continuosly updated - It also hence exists on a separate GameObject called StoryUIManager
/* Does the following:
 * 1. Loads Accept Cookies Popup and handles dismissing it
 * 2. Displays a random main story on startup and 5 recommended stories + 3 randomised stories based on user profile
 * 3. Handles the function that refreshes the UI when clicks are made - Called from HeadlineClicked.cs
 * 4. Also contains helper functions to generate Day of the week, random string to put in the URL, etc.
 * 5. Contains struct for MainStoryUI to hold references to the UI elements for the main story
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;                      // All text UI elements use TextMeshPro


// Struct for Ease of Use - Contains references to the UI elements for the main story
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
    public TMP_Text[] recommendedStoriesUI = new TMP_Text[5];         // Array to hold references to the recommended story text UI elements
    public TMP_Text[] recommendedStoryAuthorsUI = new TMP_Text[5];    // Array to hold references to the recommended story author text UI elements
    public Story mainStory;                                           // The main story to be displayed - Updated by HeadlineClicked.cs

    public List<Story> selectedStories = new List<Story>();
    public List<Story> selectedRandomStories = new List<Story>();

    // Unused Variables - For future, if needed
    public int nonRandomStoriesCount = 5; // Number of 'recommended' stories to recommend
    public int randomStoriesCount = 3;    // Number of random stories to recommend

    // UI Elements for Current Story Tab and URL
    public TMP_Text currentStoryTab;
    public TMP_Text currentStoryURL;

    //Accept Cookies Popup
    public GameObject popup;


    private string[] days = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };   // Randomly select a day from this array
    const string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";                                                            // To create randomised string for URL

    public void Awake()
    {
        popup.SetActive(true);  // Enable the cookies popup by default
    }

    public void Start()
    {
        RefreshMainStory(true);                     // Publish First Main Story - True argument indicates randomised story.
        LoadNextStories();                          // Load Recommended Stories
    }


    // Called from HeadlineClicked.cs when a headline is clicked
    // Refreshes main story based on selection, generates 5 more recommended stories, and 3 more random stories, and updates UI elements accordingly, and updates Tab name and URL
    public void LoadNextStories()
    {
        RefreshMainStory(false);   // False argument indicates non-random story - Main story is assigned in HeadlineClicked.cs

        // Load 5 Recommended Stories
        selectedStories = recommender.RecommendStories(5);      
        for (int i = 0; i < selectedStories.Count; i++)
        {
            if (selectedStories[i] == mainStory) selectedStories[i] = recommender.RecommendStories(1)[0];     // Ensure main story is not in recommended stories
            recommendedStoriesUI[i].text = selectedStories[i].Headline;                                       // Display
            recommendedStoryAuthorsUI[i].text = selectedStories[i].Author; 
        }

        // Load 3 Random Stories
        selectedRandomStories = recommender.LoadRandomStories(3);
        // Update UI Elements with Random Stories and Author
        recommendedStoriesUI[5].text = selectedRandomStories[0].Headline;    // Story 6 in Recommended column
        recommendedStoryAuthorsUI[5].text = selectedRandomStories[0].Author; // Only story 6 in Recommended Column requires author
        recommendedStoriesUI[6].text = selectedRandomStories[1].Headline;    // Left under Main Story
        recommendedStoriesUI[7].text = selectedRandomStories[2].Headline;    // Right under Main story


        // Adjust Tab Name and URL
        currentStoryTab.text = mainStory.Headline; // Set Current Story Tab to First Recommended Story
        currentStoryURL.text = "/readwhateverisuptoday/givedataplease/" + GenerateRandomString() + "/render.html"; // Generate a random URL string

    }


    // Called at the top of LoadNextStories()
    // Uses MainStoryUI struct to update the main story UI elements
    public void RefreshMainStory(bool firsttime = false) 
    { 
        if (mainStory != null)
        {
            if (firsttime) mainStory = recommender.storyLoader.AllStories[Random.Range(0, recommender.storyLoader.AllStories.Count)];
            mainStoryUI.headlineText.text = mainStory.Headline;
            mainStoryUI.bodyText.text = mainStory.Body;
            mainStoryUI.authorText.text = "by " + mainStory.Author;
            mainStoryUI.dateText.text = days[Random.Range(0, 6)] + ", " + mainStory.Date + " " + System.DateTime.Now.TimeOfDay.Hours + ":" + System.DateTime.Now.TimeOfDay.Minutes + " BST"; // Random day for demo purposes
        }
    } 

    // Helper Function - Random string for URL generation
    public string GenerateRandomString(int minLength = 10, int maxLength = 20)
    {
        int length = Random.Range(minLength, maxLength);
        string randomString = "";
        for (int i = 0; i < length; i++)
        {
            randomString += glyphs[Random.Range(0, glyphs.Length)];
        }
        return randomString;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeadlineClick : MonoBehaviour
{
    public StoryUIManager storyUIManager; // Reference to the StoryUIManager script
    public UserProfile userProfile; // Reference to the UserProfile script
    public int storyIndex; // Index of the story in the recommended stories array
    public Story story; // Reference to the Story object
    public CoffeeMatcher coffeeMatcher; // Reference to the CoffeeMatcher script
    public CreditScoreGenerator creditScoreGenerator; // Reference to the CreditScoreGenerator script


    // Update the other two screens on start (with the vaniall user profile)
    public void Start()
    {
        coffeeMatcher.PublishCoffee(); // Update the coffee matcher with the new user profile
        creditScoreGenerator.PublishScore(); // Generate a new credit score based on updated preferences
    }

    // Update the other two screens on click
    public void LoadNextStoriesOnClick()
    {
        if (storyIndex <= storyUIManager.selectedStories.Count - 1)
        {
            story = storyUIManager.selectedStories[storyIndex]; // Get the story based on the index
        }
        else { 
        
            story = storyUIManager.selectedRandomStories[storyIndex - storyUIManager.selectedStories.Count]; // Get the random story if index exceeds recommended stories
        }

        storyUIManager.mainStory = story; // Set the main story to the clicked story
        storyUIManager.LoadNextStories(); // Load the next stories to update the UI
        userProfile.UpdatePreferences(story.CategoryScores); // Update user preferences based on the clicked story's scores

        coffeeMatcher.PublishCoffee(); // Update the coffee matcher with the new user profile
        creditScoreGenerator.PublishScore(); // Generate a new credit score based on updated preferences
    }

    


}

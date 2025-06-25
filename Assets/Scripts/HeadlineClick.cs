// Summary: The script is used on all objects that are clickable headlines. It will call functions from multiple other scripts to update the UI and user profile based on the clicked headline.

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeadlineClick : MonoBehaviour
{
    public StoryUIManager storyUIManager; 
    public UserProfile userProfile; 
    public int storyIndex;               // Index of the story in the recommended stories array - Set in the Inspector, this will drive which UI element is connected to which Story in the backend
    public Story story; 
    public CoffeeMatcher coffeeMatcher; 
    public CreditScoreGenerator creditScoreGenerator; 


    // Update the other two screens on start (with the vanilla user profile)
    public void Start()
    {
        UpdateOtherScreens();   // Credit Score updated on first frame for vanilla profile
    }

    // Update the other two screens on click
    public void LoadNextStoriesOnClick()
    {

        // This works because the number of headlines is hard-coded, as well as the number of recommended stories from the Story Recommender (5)
        // Story UI elements that have an index of 4 or more will draw from random stories instead
        if (storyIndex <= storyUIManager.selectedStories.Count - 1)  // Only if the index is within the range of recommended stories
        {
            story = storyUIManager.selectedStories[storyIndex]; // Get the story based on the index
        }
        else { 
        
            story = storyUIManager.selectedRandomStories[storyIndex - storyUIManager.selectedStories.Count]; // Get a random story if index exceeds recommended stories
        }

        // Update the UI and user profile based on the clicked story
        storyUIManager.mainStory = story;                    // Set the main story to be what was just clicked
        storyUIManager.LoadNextStories();                    // Load the next stories to update the UI - This function will generate more reccos, random stories and refresh the UI to reflect the main story
        userProfile.UpdatePreferences(story.CategoryScores); // Update user preferences based on the clicked story's scores

        // Update the other two screens
        UpdateOtherScreens(); 
    }

    // Wrapper function that runs the functions for the other two screens together
    public void UpdateOtherScreens()
    {
        coffeeMatcher.PublishCoffee();         
        creditScoreGenerator.PublishScore();   
    }

    


}

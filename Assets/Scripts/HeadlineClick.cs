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

    public void LoadNextStoriesOnClick()
    {
        story = storyUIManager.selectedStories[storyIndex]; // Get the story based on the index

        storyUIManager.mainStory = story; // Set the main story to the clicked story
        storyUIManager.LoadNextStories(); // Load the next stories to update the UI
        userProfile.UpdatePreferences(story.CategoryScores); // Update user preferences based on the clicked story's scores
    }


}

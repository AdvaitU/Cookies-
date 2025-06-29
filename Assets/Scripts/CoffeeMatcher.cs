// Class that matches the User Profile to a kind of coffee archetype

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeMatcher : MonoBehaviour
{
    public TSVLoader loader;                // Archetypes are loaded from TSV file
    public UserProfile userProfile;
    public CoffeeArchetype selectedCoffee; // Selected coffee archetype
    
    // UI Components
    public TMP_Text coffeeTitle;             // UI Elements
    public TMP_Text coffeeBody;
    public TMP_Text coffeeTabName;           // To update the tab name in the UI on Screen 2
    public TMP_Text coffeeURL;               // To update the URL in the UI on Screen 2
    public GameObject loadingUI;             // Loading UI element to show while coffee is being matched
    public Image coffeeImage;                // Image to show coffee image in UI
    private string coffeeInfoTemplate = "Boldness: 65%\r\nComplexity: 50%\r\nBiterness: 45%\r\nServed: Hot\r\nPairs Well With: Collecting obscure mechanical keyboards for “focus”";

    private void Start()
    {
        if (loader == null || userProfile == null)
        {
            Debug.LogError("Loader or UserProfile is not assigned in the inspector.");
            return;
        }
        
    }

    // Method to find the best coffee archetype based on user profile preferences using closest match in a 3 dimension vector space 
    public CoffeeArchetype GetBestCoffee()
    {
        float userBoldness = userProfile.GetBoldness();
        float userComplexity = userProfile.GetComplexity();
        float userBitterness = userProfile.GetBitterness();

        CoffeeArchetype bestMatch = default;
        float closestDistance = float.MaxValue;

        foreach (var archetype in loader.archetypes)
        {
            float distance = Mathf.Pow(archetype.Boldness - userBoldness, 2)
                           + Mathf.Pow(archetype.Complexity - userComplexity, 2)
                           + Mathf.Pow(archetype.Bitterness - userBitterness, 2);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                bestMatch = archetype;
            }
        }

        return bestMatch;
    }

    // Gets called when user clicks on Headline or anywhere else where an update to coffee is needed
    public void PublishCoffee()
    {

        // Get the best coffee archetype based on user profile
        selectedCoffee = GetBestCoffee();
        // Update Main PageUI with selected coffee information
        coffeeTitle.text = $"You are {CheckVowel(selectedCoffee.Name)} {selectedCoffee.Name}!!";
        string bodyText = $"Boldness: {selectedCoffee.Boldness}%\r\nComplexity: {selectedCoffee.Complexity}%\r\nBiterness: {selectedCoffee.Bitterness}%\r\nServed: {selectedCoffee.Temperature}\r\nPairs Best With: {selectedCoffee.PairedBestWith}";
        coffeeBody.text = bodyText;

        // Update Tab
        coffeeTabName.text = $"You are {CheckVowel(selectedCoffee.Name)} {selectedCoffee.Name}!! | Quiz Results";
        coffeeURL.text = "/whatcoffeeareyou/users/temp" + GenerateRandomString(Random.Range(8,12)) + "/results.html";

        // Put Image
        LoadImage();
    }

    public string GenerateRandomString(int count = 10)
    {
        string result = "";
        for (int i = 0; i < count; i++)
        {
            int randomNumber = Random.Range(0, 10); // Generates a random number between 0 and 9
            result += randomNumber.ToString();
        }
        return result;
    }

    public string CheckVowel(string theString)
    {
        char c = theString.ToLower()[0]; // Get the first character and convert to lowercase
        if ("aeiou".IndexOf(c) >= 0)     // Check if the character is a vowel
        {
            return "an";
        }
        else
        {
            return "a";
        }
    }

    // Load the Image from the Resources folder based on the selected coffee archetype name
    public void LoadImage()
    {
        Sprite sprite = Resources.Load<Sprite>($"CoffeeImages/{selectedCoffee.Name}"); // No extension
        if (sprite != null)
        {
            coffeeImage.sprite = sprite;
        }
        else
        {
            Debug.LogError("Image not found!");
        }

    }

}

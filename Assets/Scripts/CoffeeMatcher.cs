using TMPro;
using UnityEngine;

public class CoffeeMatcher : MonoBehaviour
{
    public TSVLoader loader;
    public UserProfile userProfile;
    public CoffeeArchetype selectedCoffee; // Selected coffee archetype
    
    // UI Components
    public TMP_Text coffeeTitle;
    public TMP_Text coffeeBody;
    private string coffeeInfoTemplate = "Boldness: 65%\r\nComplexity: 50%\r\nBiterness: 45%\r\nServed: Hot\r\nPairs Well With: Collecting obscure mechanical keyboards for “focus”";

    private void Start()
    {
        if (loader == null || userProfile == null)
        {
            Debug.LogError("Loader or UserProfile is not assigned in the inspector.");
            return;
        }
        PublishCoffee(); // Automatically publish coffee on start
    }

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
    

    public void PublishCoffee()
    {

        // Get the best coffee archetype based on user profile
        selectedCoffee = GetBestCoffee();
        // Update UI with selected coffee information
        coffeeTitle.text = selectedCoffee.Name;
        string bodyText = $"Boldness: {selectedCoffee.Boldness}%\r\nComplexity: {selectedCoffee.Complexity}%\r\nBiterness: {selectedCoffee.Bitterness}%\r\nServed: {selectedCoffee.Temperature}\r\nPairs Best With: {selectedCoffee.PairedBestWith}";

        coffeeBody.text = bodyText;
    }

}

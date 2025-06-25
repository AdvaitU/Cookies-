// Summary: This script resets the current game instance by reloading the active scene - for the exhibition. Script is on the "Reset" button in the UI.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public void ResetInstance()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

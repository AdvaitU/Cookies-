// Summary: This script activates all connected displays in a Unity project.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayActivation : MonoBehaviour
{
    public bool activateAllDisplays = true;      // If true, all connected displays will be activated.
    public bool verbose = false;                 // If true, logs the number of connected displays.
    void Start()
    {
        ActivateAllDisplays();
    }

    // RUns at the start of the game to activate all displays.
    private void ActivateAllDisplays()
    {
        if (verbose) Debug.Log("Displays connected: " + Display.displays.Length);
        if (!activateAllDisplays) return;
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }

}

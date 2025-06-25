// Simply handles closing (disabling) the popup for cookies consent - Called on Button CLick of 'Accept Cookies'

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookies_Popup : MonoBehaviour
{
    // Switch it on when the game starts
    /*private void Awake()
    {
        this.gameObject.SetActive(true);
    }*/

    public void ClosePopup()
    {
        this.gameObject.SetActive(false);
    }
}

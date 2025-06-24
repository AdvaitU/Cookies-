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

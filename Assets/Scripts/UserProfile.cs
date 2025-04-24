using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum Likes
{
    None, 
}

[System.Serializable]
public struct User
{
    public int identifier;
    public string name;
    public Likes likes;

}
public class UserProfile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

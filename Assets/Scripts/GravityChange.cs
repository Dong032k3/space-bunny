using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChange : MonoBehaviour
{
    public float initialGravity, newGravity;
    void Start() { }

    void OnTriggerEnter2D(Collider2D obj)
    {
        // switch between earth and space gravity
        if (obj.CompareTag("player")) {
            if (PlayerPrefs.GetFloat("gravity") == newGravity)
            {
                PlayerPrefs.SetFloat("gravity", initialGravity);
            } else
            {
                PlayerPrefs.SetFloat("gravity", newGravity);
            }
            
        }
    }
}

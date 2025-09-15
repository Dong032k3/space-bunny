using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : MonoBehaviour
{
    private Vector3 startPos;
    public int heal = 1;

    void Start()
    {
        startPos = transform.position;
    }

    // hovering effect
    void Update()
    {
        // sin (time * frequency) * amplitude + y position
        transform.position = new Vector3(startPos.x, Mathf.Sin(Time.time * 2) * 0.1f + startPos.y, startPos.z);
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("player"))
        {
            PlayerPrefs.SetInt("bunnyHeal", 1);
            // update player prefs - cannot heal above max health
            if (PlayerPrefs.GetInt("bunnyHealth") < PlayerPrefs.GetInt("bunnyMaxHealth"))
            {
                PlayerPrefs.SetInt("bunnyHealth", PlayerPrefs.GetInt("bunnyHealth") + heal);
            }

            // heart disappears when player "collects" it
            Destroy(this.gameObject);
        }
    }
}

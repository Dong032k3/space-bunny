using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BunnyHealth : MonoBehaviour
{
    private TMP_Text healthCounter;

    // Start is called before the first frame update
    void Start()
    {
        healthCounter = GetComponent<TMP_Text>();

        healthCounter.text = PlayerPrefs.GetInt("bunnyHealth").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        healthCounter.text = PlayerPrefs.GetInt("bunnyHealth").ToString();
    }

}

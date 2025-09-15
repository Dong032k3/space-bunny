using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HawkController : MonoBehaviour
{
    Rigidbody2D hawk;
    public float speed;
    private int direction = 1;

    public int dmg = 1;

    // Start is called before the first frame update
    void Start()
    {
        hawk = GetComponent<Rigidbody2D>();
        hawk.linearVelocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // hawk speed is constant
        hawk.linearVelocity = new Vector2(direction * speed, 0);

        // change sprite direction based on direction
        if (direction < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        } else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    // hawk collision behavior
    void OnTriggerEnter2D(Collider2D obj)
    {
        // hawk turns when it hits a platform
        if (obj.gameObject.tag == "platform" || obj.gameObject.tag == "screen_bounds")
        {
            direction *= -1;
        }
        // bunny takes damage when it hits a hawk
        else if (obj.CompareTag("player"))
        {
            GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("bunnyHit", 1); // indicator for bunny flash effect
            PlayerPrefs.SetInt("bunnyHealth", PlayerPrefs.GetInt("bunnyHealth") - dmg);
            if (PlayerPrefs.GetInt("bunnyHealth") <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}

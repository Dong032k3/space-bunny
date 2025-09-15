using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class for moving platforms
public class PlatformController : MonoBehaviour
{
    Rigidbody2D platform;
    public float speed;
    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<Rigidbody2D>();
        platform.linearVelocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // speed is constant
        platform.linearVelocity = new Vector2(direction * speed, 0);
    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        // changes direction when hitting other platform or screen bounds
        if (obj.gameObject.tag == "platform" || obj.gameObject.tag == "screen_bounds")
        {
            direction *= -1;
        }
    }
}

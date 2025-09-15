using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotBuff : MonoBehaviour
{
    private GameObject carrot;
    private Renderer carrotRenderer;
    private Collider2D carrotCollider;
    private Vector3 startPos;
    public int buffEffect = 20;

    void Start()
    {
        carrot = this.gameObject;
        carrotRenderer = carrot.GetComponent<SpriteRenderer>();
        carrotCollider = carrot.GetComponent<CapsuleCollider2D>();

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
            // add buff effect here
            PlayerPrefs.SetInt("bunnyBuff", 1);
            PlayerPrefs.SetInt("carrotBuff", buffEffect);

            // carrot disappears when player "collects" it
            carrotRenderer.enabled = false;
            carrotCollider.enabled = false;

            // respawns after a short duration
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        // wait for 4 seconds
        yield return new WaitForSeconds(4);

        carrotRenderer.enabled = true;
        carrotCollider.enabled = true;
    }
}

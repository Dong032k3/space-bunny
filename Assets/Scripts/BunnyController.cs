using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController : MonoBehaviour
{
    Rigidbody2D bunny;
    SpriteRenderer spriteRenderer;
    Animator anim;

    // materials
    Material bunnyMaterial;
    [SerializeField] Material healMaterial;
    [SerializeField] Material buffMaterial;

    // bunny traits
    public int maxHealth = 10;
    public float jumpPower, speed;
    private float jump;

    // grounded check
    private bool bGrounded;

    // Start is called before the first frame update
    void Start()
    {
        bunny = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        bunnyMaterial = spriteRenderer.material;

        PlayerPrefs.SetInt("bunnyMaxHealth", maxHealth);
        PlayerPrefs.SetInt("bunnyHealth", maxHealth);
        jump = jumpPower;

        PlayerPrefs.SetFloat("gravity", 1.8f);
        PlayerPrefs.SetInt("carrotBuff", 0);
    }

    // FixedUpdate is called before update
    void FixedUpdate()
    {
        // movement
        if ((Input.GetKey("w") || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && bGrounded)
        {
            if (PlayerPrefs.GetInt("carrotBuff") == 0)
            {
                jump = jumpPower;
            }
            else
            {
                jump = jumpPower * PlayerPrefs.GetInt("carrotBuff");
                PlayerPrefs.SetInt("carrotBuff", 0);

            }
            bunny.linearVelocity = new Vector2(bunny.linearVelocity.x, jump);
            GetComponent<AudioSource>().Play();
            anim.SetBool("jumping", true);
        }
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            bunny.linearVelocity = new Vector2(-speed, bunny.linearVelocity.y);
            if (bGrounded) anim.SetBool("moving", true);
        }
        else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(1, 1, 1);
            bunny.linearVelocity = new Vector2(speed, bunny.linearVelocity.y);
            if (bGrounded) anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }

        // update gravity depending on location
        bunny.gravityScale = PlayerPrefs.GetFloat("gravity");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        // hurt animation check
        if (PlayerPrefs.GetInt("bunnyHit") == 1)
        {
            anim.SetBool("damaged", true);
            StartCoroutine(HitEffect());
        }

        // check for buff effect
        if (PlayerPrefs.GetInt("bunnyBuff") == 1)
        {
            StartCoroutine(BuffEffect());
        }
        // check for heal effect
        else if (PlayerPrefs.GetInt("bunnyHeal") == 1)
        {
            StartCoroutine(HealEffect());
        }
    }

    // collision effect depends on object type
    void OnCollisionEnter2D(Collision2D obj)
    {
        // bunny can only jump if in contact with a platform
        if (obj.gameObject.tag == "platform")
        {
            float platWidth = (obj.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2) * obj.gameObject.transform.localScale.x;
            float platHeight = obj.gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;
            // check if bunny is above platform and not on the sides
            if (bunny.position.y > obj.gameObject.transform.position.y + platHeight && 
                (bunny.position.x > obj.gameObject.transform.position.x - platWidth && bunny.position.x < obj.gameObject.transform.position.x + platWidth))
            {
                bGrounded = true;
                anim.SetBool("jumping", false);
            }
        }
    }

    // no collision = rabbit not grounded
    void OnCollisionExit2D(Collision2D obj)
    {
        bGrounded = false;
    }

    private IEnumerator HitEffect()
    {
        // hit animation plays for a bit
        yield return new WaitForSeconds(0.5f);

        // stop animation
        anim.SetBool("damaged", false);
        PlayerPrefs.SetInt("bunnyHit", 0);
    }

    private IEnumerator BuffEffect()
    {
        // swap to flash effect
        spriteRenderer.material = buffMaterial;

        // wait before executing next part of function
        yield return new WaitForSeconds(0.3f);

        // swap to original material
        spriteRenderer.material = bunnyMaterial;

        // stop routine
        PlayerPrefs.SetInt("bunnyBuff", 0);
    }

    private IEnumerator HealEffect()
    {
        // swap to heal effect
        spriteRenderer.material = healMaterial;

        // wait before executing next part of function
        yield return new WaitForSeconds(0.3f);

        // swap to original material
        spriteRenderer.material = bunnyMaterial;

        // stop routine
        PlayerPrefs.SetInt("bunnyHeal", 0);
    }
}

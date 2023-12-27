using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private float dirX = 0f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D coll;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private PlayerHealth health;
    private GameObject enemies;
    private GameObject animals;
    public float hurtForce;
    bool isHurting;
    public GameObject firePoint;
    private bool hasRotated = false;
    public GameObject DeathPanel;

    private enum AnimState { Idle, Run,  Jump, Fall, Death}

    private AnimState state = AnimState.Idle;

    private AudioManager audioManager;

    private void Awake()
    {
        enemies = GameObject.FindGameObjectWithTag("Enemies");
        animals = GameObject.FindGameObjectWithTag("Animals");
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isHurting)
        {
            dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded())
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    audioManager.PlaySFX(audioManager.jump);
                }
            }
        }

        AnimUpdate();
        animator.SetInteger("state", (int)state);
    }

    private void AnimUpdate()
    {
        if(dirX  > 0)
        {
            state = AnimState.Run;
            spriteRenderer.flipX = false;
            
            if (hasRotated)
            {
                firePoint.transform.Rotate(0, 180, 0);
            }
            hasRotated = false;
        }
        else if (dirX < 0)
        {
            state = AnimState.Run;
            spriteRenderer.flipX = true;
            
            if(!hasRotated)
            {
                firePoint.transform.Rotate(0, 180, 0);
            }
            hasRotated = true;
        }
        else
        {
            state = AnimState.Idle;
        }

        if(rb.velocity.y > .1f)
        {
            state = AnimState.Jump;
        }
        else if(rb.velocity.y < -.1f)
        {
            state = AnimState.Fall;
        }

        if (health.health <= 0)
        {
            state = AnimState.Death;
            StartCoroutine("DisplayDeadMenu");
        }
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemies"))
        {
            health.health -= enemies.GetComponent<Enemies>().damage;
            animator.SetTrigger("isHurting");
            StartCoroutine("Hurt");
        }
        if (collision.gameObject.CompareTag("Animals"))
        {
            health.health -= animals.GetComponent<Animals>().damage;
            animator.SetTrigger("isHurting");
            StartCoroutine("Hurt");
        }
    }
    IEnumerator Hurt()
    {
        isHurting = true;
        rb.velocity = Vector2.zero;
        audioManager.PlaySFX(audioManager.hurt);
        if (enemies && animals)
        {
            if(transform.position.x > enemies.transform.position.x && transform.position.x > animals.transform.position.x)
            {
                rb.AddForce(new Vector2 (hurtForce, rb.velocity.y));
            }
            else
            {
                rb.AddForce(new Vector2(-hurtForce, rb.velocity.y));
            }
        }
        yield return new WaitForSeconds(0.5f);

        isHurting = false;
    }

    IEnumerator DisplayDeadMenu()
    {
        audioManager.PlaySFX(audioManager.death);
        yield return new WaitForSeconds(0.7f);
        DeathPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}

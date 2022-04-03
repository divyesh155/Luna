using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement1 : MonoBehaviour
{
    [Header("Player Movement")]
    public float movementSpeed;
    public float jumpForce;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public bool isGrounded;
    public bool lookingright = true;

    [Header("Animation Properties")]
    public Animator animator;
    private Rigidbody2D _rigidbody2D;

    [Header("HealthBar")]

    public int maxHealth = 100;
    public int currentHealth;
    private float elapsed = 0f;
    public HealthBar healthBar;

    [Header("Weapon")]

    //public GameObject bulletPrefab;
    //public Vector3 spawnPos;
    private float movement;
    public ProjectileBehaviour projectilePrefab;
    public Transform launchOffset;

    void Start()
    {

        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        //Move();

        //spawnPos = transform.position;

        //Life leak
        elapsed += Time.deltaTime;
        if (elapsed >= 1f)
        {
            elapsed = elapsed % 1f;
            TakeDamage(3);
        }
        if (currentHealth <= 0)
        {
            animator.SetInteger("AnimationState", 4);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(4);

        }

        movement = Input.GetAxisRaw("Horizontal");

        if (movement > 0 || movement < 0)
        {
            //isMoving = true;
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementSpeed;
            animator.SetInteger("AnimationState", 1);
        }
        if (movement == 0)
        {
            // isMoving = false;
            animator.SetInteger("AnimationState", 0);
        }
        if (!Mathf.Approximately(0, movement))
        {
            transform.rotation = movement > 0 ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody2D.velocity.y) < 0.001f)
        {
            //isJumping = true;
            _rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            //isGrounded = false;
            animator.SetInteger("AnimationState", 2);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            //movement = 0;
            TakeDamage(5);
            animator.SetInteger("AnimationState", 3);
            Instantiate(projectilePrefab, launchOffset.position, transform.rotation);

        }

    }


    private void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * movementSpeed;
            _rigidbody2D.AddForce(new Vector2(0, jumpForce / 2), ForceMode2D.Impulse);
            TakeDamage(10);
        }

        if (col.gameObject.tag == "Offscreen2")
        {
            TakeDamage(100);
            animator.SetInteger("AnimationState", 4);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (col.gameObject.CompareTag("Platform"))
        {
            this.transform.parent = col.transform;
        }
        if (col.gameObject.tag == "Offscreen1")
        {
            TakeDamage(100);
            animator.SetInteger("AnimationState", 4);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (col.gameObject.CompareTag("Finish1"))
        {
            SceneManager.LoadScene(2);
        }
        if (col.gameObject.CompareTag("Finish2"))
        {
            SceneManager.LoadScene(3);
        }
        if (col.gameObject.CompareTag("Finish3"))
        {
            SceneManager.LoadScene(5);
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            TakeDamage(100);
            this.transform.parent = null;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(10);
        }
        if (col.gameObject.CompareTag("Blackhole"))
        {
            TakeDamage(100);
            Debug.Log("hi");
        }
        if (col.gameObject.tag == "Offscreen")
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(4);
        }
        if (col.gameObject.tag == "Item")
        {
            Destroy(col.gameObject);
            TakeDamage(-15);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}



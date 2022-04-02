using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Movement")]
    public float horizontalForce;
    public float verticalForce;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public bool isGrounded;
    public bool lookingright = true;

    [Header("Animation Properties")]
    public Animator animator;

    private Rigidbody2D rigidbody2D;

    [Header("HealthBar")]

    public int maxHealth = 100;
	public int currentHealth;
    private float elapsed = 0f;
	public HealthBar healthBar;

    [Header("Weapon")]

    public GameObject bulletPrefab;
    public Vector3 spawnPos;

    [Header("Tutorials")]
    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject tutorial3;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

        spawnPos = transform.position;

    //Life leak
    elapsed += Time.deltaTime;
     if (elapsed >= 1f) {
         elapsed = elapsed % 1f;
			TakeDamage(3);
     }
     if(currentHealth <= 0)
     {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
     }
    }

    private void Move()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayerMask);


        if (isGrounded)
        {
            float jump = Input.GetAxisRaw("Jump");
            float run = Input.GetAxisRaw("Horizontal");

                if (run != 0)
            {
                run = Flip(run);
                lookingright = false;

            }
            else if(run == 0 && jump == 0)
            {
                animator.SetInteger("AnimationState", 0);
            }
            if(jump > 0)
            {
             animator.SetInteger("AnimationState", 2);
            }
            if(run < 0 || run > 0)
            {
             animator.SetInteger("AnimationState", 1);
            }
            if (Input.GetButtonDown("Fire1"))
            {
                run = 0;
             animator.SetInteger("AnimationState", 3);
        Instantiate( bulletPrefab, spawnPos, bulletPrefab.transform.rotation);

            }
        
            Vector2 move = new Vector2(run * horizontalForce, jump * verticalForce);
            rigidbody2D.AddForce(move);

            
        }
    }
    void TakeDamage(int damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);
	}

    private float Flip(float x)
    {
        x = (x > 0) ? 1 : -1;

        transform.localScale = new Vector3(x, 1.0f);
        return x;
    }
    //collisions
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy"){
        
			TakeDamage(5);
        }
        if (col.gameObject.tag == "Item"){

			TakeDamage(-10);
        }
        if (col.gameObject.tag == "Offscreen"){

			SceneManager.LoadScene("Level 1");
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
       if (col.gameObject.CompareTag("Obstacle"))
    {
        			TakeDamage(5);
    }
       if (col.gameObject.CompareTag("Tutorial1"))
        {
            tutorial1.SetActive(true);
        }

        if (col.gameObject.CompareTag("Tutorial2"))
        {
            tutorial2.SetActive(true);
        }

        if (col.gameObject.CompareTag("Tutorial3"))
        {
            tutorial3.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Tutorial1"))
        {
            tutorial1.SetActive(false);
        }

        if (col.gameObject.CompareTag("Tutorial2"))
        {
            tutorial2.SetActive(false);
        }

        if (col.gameObject.CompareTag("Tutorial3"))
        {
            tutorial3.SetActive(false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}

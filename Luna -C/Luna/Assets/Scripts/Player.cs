using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float movementSpeed = 4.0f;
    public float JumpForce;
    private Rigidbody2D _rigidbody;
    private Animator anim;
    private bool isMoving = false;
    private bool isJumping = false;
    public bool isGrounded = false;
    public float movement;
    public KillPlayer kill;
    public int life = 1;

    [Header("HealthBar")]

    public int maxHealth = 100;
    public int currentHealth;
    private float elapsed = 0f;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
       // healthBar = GetComponent<HealthBar>();
        _rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void FixedUpdate()
    {
        

        //Life leak
        elapsed += Time.deltaTime;
        if (elapsed >= 1f)
        {
            elapsed = elapsed % 1f;
            TakeDamage(3);
        }
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("Level1");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( life == 1)
        {
            movement = Input.GetAxis("Horizontal");

            if (movement > 0 || movement < 0)
            {
                isMoving = true;
                transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementSpeed;
                anim.SetBool("Walking", isMoving);
               // movement = Flip(movement);
            }
            else if (movement == 0)
            {
                isMoving = false;
                anim.SetBool("Walking", isMoving);
            }

            if(!Mathf.Approximately(0, movement))
            {
                transform.rotation = movement > 0? Quaternion.Euler(0, 0, 0) : Quaternion.identity;
           }

            if(Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
            {
                isJumping = true;
                _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                anim.SetTrigger("Up");
                isGrounded = false;
            }

            if(isJumping)
            {
                isJumping = false;

                if (!isGrounded)
                {
                    anim.SetTrigger("Ground");
                    //isGrounded = true;
                }
                
            }
        }
    }
     void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Platform"))
        {
            this.transform.parent = collision.transform;
        }
        if(collision.gameObject.CompareTag("DeathZone"))
        {
            SceneManager.LoadScene(0);
        }
        if (collision.gameObject.tag == "Item")
        {
            TakeDamage(-10);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            this.transform.parent = null;
        }
    }
    private float Flip(float x)
    {
        x = (x > 0) ? 1 : -1;

        transform.localScale = new Vector3(x, 1.0f);
        return x;
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float elapsed = 0f;
    private Rigidbody2D rigidbody2D;
    public Animator animator;
    public bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetInteger("AnimationState", 0);

    }
    void Jump()
    {
        Vector2 impulse = new Vector2(0, 500);
        rigidbody2D.AddForce(impulse);
        animator.SetInteger("AnimationState", 1);
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 4f)
        {
            elapsed = elapsed % 1f;
            Jump();
            isJumping = false;
        }
        if (elapsed >= 5f)
        {
            elapsed = elapsed % 1f;
            animator.SetInteger("AnimationState", 0);
        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Shot")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.CompareTag("Player"))
        {
            //Destroy(col.gameObject);
        }
    }
}

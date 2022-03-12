using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float elapsed = 0f;
    public float speed = 30; 
    private PlayerBehaviour _playerBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.right*speed*Time.deltaTime);    
    elapsed += Time.deltaTime;
     if (elapsed >= 5f) {
         elapsed = elapsed % 1f;
         Destroy(gameObject);
     }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy"){
        Destroy(gameObject);
    }
    }
}

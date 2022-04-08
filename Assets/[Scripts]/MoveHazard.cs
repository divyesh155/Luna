using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHazard : MonoBehaviour
{
    private float elapsed = 0f;

    public float speed = 30; 
    public bool onplayer = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(onplayer){
        transform.Translate(Vector3.down*speed*Time.deltaTime);   
        elapsed += Time.deltaTime;
        if (elapsed >= 4f)
        {
            elapsed = elapsed % 1f;
            Destroy(gameObject);
        }  
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
       if (col.gameObject.CompareTag("Player"))
    {
        Debug.Log("Player");
        onplayer = true;
    }
    }
}

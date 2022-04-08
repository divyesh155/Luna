using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveleft : MonoBehaviour
{
    private float elapsed = 0f;

    private float speed = 4; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 2f)
        {
            elapsed = elapsed % 1f;
            speed++;
        } 
        transform.Translate(Vector3.up*speed*Time.deltaTime); 

        if(speed > 7)
        {
            speed = 6;
        }
    }
}

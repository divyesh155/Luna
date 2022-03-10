using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    public int Respawn;
    private Animator anim;
    //public PlaySound playSound;
    // Start is called before the first frame update
    void Start()
    {
       // playSound = GetComponent<PlaySound>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
     void OnCollisionEnter2D(Collision2D col)
    {
       /*  if(col.gameObject.tag.Equals("EnemyBullet") || col.gameObject.tag.Equals("DeathZone") || col.gameObject.tag.Equals("Enemy"))
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        } */

         if(col.gameObject.tag.Equals("EnemyBullet"))
        {
            anim.SetTrigger("Dead");
          //  playSound.Play(3);
        //  playSound.Play(0);

            Destroy(col.gameObject);
            
           // Destroy(gameObject);
            Respawn=1;
            //SceneManager.LoadScene(1);
        }
       
           
        
       
    }
}

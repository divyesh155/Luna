using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Vector3 spawnPos;


    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position;
    }

    // Update is called once per frame
    void SpawnBullet()
    {
        if (Input.GetKeyDown(KeyCode.E)){
        Instantiate( bulletPrefab, spawnPos, bulletPrefab.transform.rotation);
        
    }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

	public int maxHealth = 100;
	public int currentHealth;
    private float elapsed = 0f;

	public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
     if (elapsed >= 1f) {
         elapsed = elapsed % 1f;
			TakeDamage(3);
     }
    }

	void TakeDamage(int damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);
	}
}

using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 1;
    public GameObject deathEffect;
 
    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}

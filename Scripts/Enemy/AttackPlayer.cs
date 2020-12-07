using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public int damage = 1;
    public GameObject deathEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Destroy(gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
    }
}

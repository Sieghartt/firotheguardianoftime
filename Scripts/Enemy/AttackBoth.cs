using UnityEngine;

public class AttackBoth  : MonoBehaviour
{
    public int damage = 1;
    public GameObject deatheffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();

        if(playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Destroy(gameObject);
            Instantiate(deatheffect, transform.position, transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CrystalHealth crystalHealth = collision.GetComponent<CrystalHealth>();

        if(crystalHealth != null)
        {
            crystalHealth.TakeDamage(damage);
            Destroy(gameObject);
            Instantiate(deatheffect, transform.position, transform.rotation);
        }
    }
}

using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime;
    public float distance;
    public int damage = 1;

    public Rigidbody2D rigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody.velocity = transform.up * speed;
        Invoke("DestroyProjectile", lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

        if(enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}

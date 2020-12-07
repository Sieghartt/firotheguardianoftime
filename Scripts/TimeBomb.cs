using UnityEngine;

public class TimeBomb : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public GameObject timeField;
    public float speed;
    public float delay;
    float countdown;
    bool hasExploded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
        rigidBody.velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if(countdown <= 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool enemy = collision.gameObject.CompareTag("Enemy");
        
        if(enemy)
        {
            Explode();
        }
    }

    void Explode()
    {
        Instantiate(timeField, transform.position, transform.rotation);
        FindObjectOfType<AudioManager>().Play("Time Field");
        Destroy(gameObject);
    }
}

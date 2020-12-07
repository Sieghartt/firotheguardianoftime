using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    private Animator playerAnimator;
    private Animator cameraShake;
    private Score score;

    public Transform meleePoint;
    public GameObject AttackEffect;

    public float attackRange;
    public float delayTime;
    public LayerMask whatAreEnemies;
    public int damage;
    private bool facingRight, meleeEnabled = true;


    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
            facingRight = true;
        if (Input.GetAxisRaw("Horizontal") < 0)
            facingRight = false;

        StartCoroutine(MeleeAttack());
    }

    IEnumerator MeleeAttack()
    {
        if (Input.GetButtonDown("Melee") && meleeEnabled)
        {          
            playerAnimator.SetTrigger("Attack");

            Instantiate(AttackEffect, meleePoint.position, meleePoint.rotation);
            FindObjectOfType<AudioManager>().Play("AttackSFX");

            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(meleePoint.position, attackRange, whatAreEnemies);

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                
                FindObjectOfType<AudioManager>().Play("HitSFX");

                if (facingRight)
                    cameraShake.SetTrigger("Right Shake");
                if (!facingRight)
                    cameraShake.SetTrigger("Left Shake");

                score.killScore += 1;
            }

            meleeEnabled = false;
            yield return new WaitForSeconds(delayTime);
            meleeEnabled = true;
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleePoint.position, attackRange);
    }
}

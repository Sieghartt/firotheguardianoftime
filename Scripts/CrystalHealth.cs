using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;
using System.Collections;
using UnityEngine.SceneManagement;

public class CrystalHealth : MonoBehaviour
{
    public float startHealth = 20;
    float health;
    public GameObject crystal;
    public Image healthBar;
    private Animator damageEffect;

    private void Start()
    {
        damageEffect = GetComponent<Animator>();
        health = startHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    public void TakeDamage(int damage)
    {
        damageEffect.SetTrigger("Damaged");
        CameraShaker.Instance.ShakeOnce(7f, 10f, 0.1f, 2f);
        health -= damage;
        healthBar.fillAmount = health / startHealth;
        FindObjectOfType<AudioManager>().Play("Crystal Damage");
        if (health <= 0)
        {
            Destroy(crystal);
            StartCoroutine(GameOverScene());
        }
    }

    IEnumerator GameOverScene()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(2);
    }
}

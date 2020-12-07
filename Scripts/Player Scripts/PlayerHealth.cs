using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using EZCameraShake;

public class PlayerHealth : MonoBehaviour
{

    public int health = 10;
    bool damaged;

    public GameObject deathExplode;
    public Image damageImage;
    public Image[] hearts;
    public Sprite fullHeart;
    private Animator musicFade;
    private Renderer playerRender;
    private Color color;

    float duration = 2f;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 9, false);
        playerRender = GetComponent<Renderer>();
        musicFade = GameObject.FindGameObjectWithTag("BGM").GetComponent<Animator>();
        color = playerRender.material.color;
    }

    private void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
        }else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].enabled = true;
            }else
            {
                hearts[i].enabled = false;
            }
        }
    }

    IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        color.a = 0.6f;
        playerRender.material.color = color;
        yield return new WaitForSeconds(duration);
        Physics2D.IgnoreLayerCollision(8, 9, false);
        color.a = 1f;
        playerRender.material.color = color;
    }

    public void TakeDamage(int damage)
    {
        damaged = true;
        health -= damage;
        CameraShaker.Instance.ShakeOnce(7f, 10f, 0.1f, 2f);

        if (health <= 0)
        {
            FindObjectOfType<AudioManager>().Play("DeathSFX");
            musicFade.SetTrigger("MainThemeFadeOut");
            Destroy(gameObject);
            Instantiate(deathExplode, transform.position, transform.rotation);
            StartCoroutine(GameOverScene());
        }

        StartCoroutine(Invulnerability());
    }

    IEnumerator GameOverScene()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("GameOver");
    }

    public void Heal(int heal)
    {
        if (health == 10)
            return;

        health += heal;
    }
}

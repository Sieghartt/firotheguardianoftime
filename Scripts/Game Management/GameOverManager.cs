using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    PlayerHealth playerHealth;
    WaveSpawner waveSpawner;
    Animator transitionAnim;

    private void Start()
    {
        waveSpawner = GameObject.FindGameObjectWithTag("GM").GetComponent<WaveSpawner>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerHealth>();
        transitionAnim = GameObject.FindGameObjectWithTag("LevelChanger").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.health <= 0)
        {
            waveSpawner.enabled = false;
            StartCoroutine(LevelTransition());
        }
    }

    IEnumerator LevelTransition()
    {
        yield return new WaitForSeconds(5f);
        transitionAnim.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(1);
    }
}

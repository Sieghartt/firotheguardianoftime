using UnityEngine;
using UnityEngine.UI;
using Pathfinding;
using System.Collections;


public class Ultimate : MonoBehaviour
{
    //AIDestinationSetter destinationSetter;
    //AIPath aiPath;
    WaveSpawner waveSpawner;
    HeartSpawner heartSpawner;
    //Animator enemyAnimator;

    public Image ultIcon;

    public float ultCoolDown;
    public float ultimateDuration;
    float currentCoolDown;

    void Start()
    {
        waveSpawner = GameObject.FindGameObjectWithTag("GM").GetComponent<WaveSpawner>();
        heartSpawner = GameObject.FindGameObjectWithTag("GM").GetComponent<HeartSpawner>();
    }

    void FixedUpdate()
    {
        if (currentCoolDown >= ultCoolDown)
        {
            if (Input.GetButtonDown("Ultimate"))
            {
                currentCoolDown = 0f;
                waveSpawner.enabled = false;
            }
        }
    }

    void Update()
    {
        if (currentCoolDown < ultCoolDown)
        {
            currentCoolDown += Time.deltaTime;
            ultIcon.fillAmount = currentCoolDown / ultCoolDown;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class BombThrow : MonoBehaviour
{
    public GameObject timeBomb;
    public Transform bombPoint;
    public Image bombIcon;

    public float bombCoolDown;
    float currentCoolDown;

    private void FixedUpdate()
    {
        if(currentCoolDown >= bombCoolDown)
        {
            if (Input.GetButtonDown("Time Bomb"))
            {
                Instantiate(timeBomb, bombPoint.position, transform.rotation);
                FindObjectOfType<AudioManager>().Play("Bomb Throw");
                currentCoolDown = 0f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (currentCoolDown < bombCoolDown)
        {
            currentCoolDown += Time.deltaTime;
            bombIcon.fillAmount = currentCoolDown / bombCoolDown;
        }
    }
}

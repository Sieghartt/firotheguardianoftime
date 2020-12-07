using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RangeAttack : MonoBehaviour
{
    public int rounds;
    public float rangeCoolDown = 3;
    public float offset;
    public Image[] ammoIcon;

    private bool rangeEnabled = true;

    public GameObject Projectile;
    public Transform projectilePoint;

    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation + offset);

        StartCoroutine(Range());

        for (int i = 0; i < ammoIcon.Length; i++)
        {
            if (i < rounds)
            {
                ammoIcon[i].enabled = true;
            }
            else
            {
                ammoIcon[i].enabled = false;
            }
        }
    }

    IEnumerator Range()
    {
        if (Input.GetButtonDown("Fire1") && rangeEnabled)
        {
            --rounds;
            Instantiate(Projectile, projectilePoint.position, transform.rotation);
        }

        if (rounds == 0)
        {
            rangeEnabled = false;
            yield return new WaitForSeconds(rangeCoolDown);
            rounds = 5;
            rangeEnabled = true;
        }
    }
}

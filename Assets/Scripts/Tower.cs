using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{

    const string bullet = "Bullet";
    const string gunTools = "GunTools";

    void Awake()
    {
        Building();
    }

    public void Building()
    {
        StartCoroutine(TowerBuild());
    }

    IEnumerator TowerBuild()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
            }
        }

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            foreach(Transform grandChild in child)
            {
                if(!grandChild.CompareTag(bullet) && !grandChild.CompareTag(gunTools))
                {
                    grandChild.gameObject.SetActive(true);
                }
            }
        }
    }
}

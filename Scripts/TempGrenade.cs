using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGrenade : MonoBehaviour
{
    public float explosionForce = 100;
    public float explosionRadius = 5;
    public float timer = 0;

    public GameObject explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    public void detonate()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        SoundManager.Instance.playSound("Grenade_Explode");
        Collider[] cols = Physics.OverlapSphere(this.transform.position, 5f);
        int i = 0;
        while (i < cols.Length)
        {
            if (cols[i].gameObject.tag == "Enemy")
            {
                cols[i].gameObject.GetComponent<TempEnemy>().grenadeHit(this.transform.position, explosionForce, explosionRadius);
            }
            i++;
        }

        Destroy(this.gameObject);
    }
}

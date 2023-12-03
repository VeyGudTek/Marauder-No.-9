using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLootSplash : MonoBehaviour
{
    public int spawnAmount = 10;
    public GameObject loot;
    public float explosionForce = 100;
    public float explosionRadius = 1;
    public List<GameObject> loot_list = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //spawns loot randomly around transform and adds explosion force
    public void spawnLoot()
    {
        Vector3 spawnPosition;
        for (int i = 0; i < spawnAmount; i ++)
        {
            spawnPosition = transform.position + new Vector3(Random.Range(-2f, 2f), .5f, 0f);
            GameObject clone = Instantiate(loot, spawnPosition, Quaternion.identity);
            clone.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, this.transform.position, explosionRadius);
            loot_list.Add(clone);
        }
    }
}

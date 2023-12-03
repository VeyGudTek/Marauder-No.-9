using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGrenadeThrow : MonoBehaviour
{
    [SerializeField] private NewPlayerScript p;
    [SerializeField] private List<GameObject> grenade_list = new List<GameObject>();
    [SerializeField] private GameObject grenade;
    [SerializeField] private float throwForce = 100;
    [SerializeField] private float throwDelay = 1;
    [SerializeField] private float grenadeDuration = 3;
    public float throwTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        throwTimer = throwDelay + 1;
    }

    // Update is called once per frame
    void Update()
    { 
        timeOutGrenades();

        //UpdateTimer
        if (throwTimer <= throwDelay)
        {
            throwTimer += Time.deltaTime;
        }

        //Input
        if (p.pi.inputGrenade)
        {
            onClick();
        }
    }

    //called whenever the player presses the throw grenade input
    public void onClick()
    {
        if (throwTimer <= throwDelay)
        {
            detonateGrenades();
        }
        else
        {
            throwGrenade();
            throwTimer = 0;
        }
    }

    //detonate grenades after a time
    void timeOutGrenades()
    {
        foreach (GameObject clone in new List<GameObject>(grenade_list))
        {
            if (clone.GetComponent<TempGrenade>().timer > grenadeDuration)
            {
                grenade_list.Remove(clone);
                clone.GetComponent<TempGrenade>().detonate();
            }
        }
    }

    //detonate all existing grenades
    void detonateGrenades()
    {
        foreach(GameObject clone in new List<GameObject>(grenade_list))
        {
            grenade_list.Remove(clone);
            clone.GetComponent<TempGrenade>().detonate();
        }
    }

    //instantiate a new grenade
    void throwGrenade()
    {
        GameObject clone = Instantiate(grenade, this.transform.position + transform.forward, Quaternion.identity);
        grenade_list.Add(clone);
        clone.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce);
    }
}

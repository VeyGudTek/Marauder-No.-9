using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damage;
    public float range;
    public Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Delete itself after certain distance
        if (Vector3.Distance(initialPosition, this.transform.position) > range)
        {
            Destroy(this.gameObject);
        }
    }
}

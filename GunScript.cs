using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Auto, Semi, Burst };
public class GunScript : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    public int ammo = 30;
    [SerializeField] private float bulletBaseForce;
    [SerializeField] private float bulletRange;
    [SerializeField] private float fireRate;
    [SerializeField] private int damage = 10;
    [SerializeField] private WeaponType type = WeaponType.Auto;
    bool canFire;
    float fireTimer;

    //On Death Variables
    [SerializeField] private WeaponType typeOnDeath = WeaponType.Auto;
    [SerializeField] private int ammoOnDeath = 20;
    [SerializeField] private int damageOnDeath = 10;
    [SerializeField] private float fireRateOnDeath = .25f;

    //Burst Variables
    [SerializeField] private int burstAmount = 3;
    public int burstCount;
    public bool bursting;
    float tempPlayerVelocity;
    string tempBulletTag;

    void Start()
    {
        fireTimer = 100;
        canFire = true;
        burstCount = 0;
        bursting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer <= fireRate)
        {
            fireTimer += Time.deltaTime;
        }
        if(bursting)
        {
            burst();
        }
    }

    public void onFireUp()
    {
        canFire = true;
    }

    public void onClick(float playerVelocity, string bulletTag)
    {
        //Control Rate of Fire
        if (fireTimer > fireRate && canFire && !bursting && ammo > 0)
        {
            FireGun(playerVelocity, bulletTag);
            ammo -= 1;
            if (type == WeaponType.Auto)
            {
                canFire = true;
            }
            else if (type == WeaponType.Semi)
            {
                canFire = false;
            }
            else if (type == WeaponType.Burst)
            {
                burstCount += 1;
                tempPlayerVelocity = playerVelocity;
                tempBulletTag = bulletTag;
                bursting = true;
                canFire = false;
            }
            fireTimer = 0f;
        }
    }

    void burst()
    {
        if (fireTimer > fireRate && ammo > 0)
        {
            FireGun(tempPlayerVelocity, tempBulletTag);
            burstCount += 1;
            ammo -= 1;
            if (burstCount > burstAmount)
            {
                bursting = false;
                burstCount = 0;
            }
            fireTimer = 0f;
        }
        else if (ammo <= 0)
        {
            bursting = false;
            burstCount = 0;
            fireTimer = 0f;
        }
    }

    public void FireGun(float playerVelocity, string bulletTag)
    {
        SoundManager.Instance.playSound("Shoot_Lazer");
        //Set bullet force and position based on player direction and speed
        Vector3 initialPosition;
        Vector3 bulletForce;
        initialPosition = transform.position + transform.forward;
        bulletForce = transform.forward * (bulletBaseForce + Mathf.Abs(playerVelocity * 50)); 

        //Instantiate bullet and set variables
        GameObject clone = Instantiate(bullet, initialPosition, Quaternion.identity);
        clone.gameObject.tag = bulletTag;
        clone.GetComponent<Rigidbody>().AddForce(bulletForce);

        //EDIT BULLET SCRIPT
        clone.GetComponent<BulletScript>().range = bulletRange + Mathf.Abs(playerVelocity) * 1.5f;           //Scale Range with player velocity or else range will appear shorter
        clone.GetComponent<BulletScript>().initialPosition = initialPosition;
        clone.GetComponent<BulletScript>().damage = damage;
    }

    public void onEnemyDeath()
    {
        type = typeOnDeath;
        ammo = ammoOnDeath;
        fireRate = fireRateOnDeath;
        damage = damageOnDeath;
    }
}

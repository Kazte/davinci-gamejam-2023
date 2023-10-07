using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    
    public GameObject BulletPrefab;
    public float BulletSpeed = 10f;
    public float ShootCooldown = 0.20f;
    public int StartingAmmo = 5;
    public AudioSource iaojfawoiw;
  
    private bool gorduraActivate = false;
    private int currentAmmo;
    private float lastShootTime;

    
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = StartingAmmo;
        lastShootTime = -ShootCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && (Time.time - lastShootTime) >= ShootCooldown&& currentAmmo > 0)
        {
            Shooting();
            lastShootTime = Time.time;
            if (gorduraActivate == false)
            {
                currentAmmo--;
            }
        }
    }
    void Shooting()
    {
        GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);

        Rigidbody bulletRigidBody = bullet.GetComponent<Rigidbody>();

        bulletRigidBody.velocity = transform.forward * BulletSpeed;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CartuchoObject"))
        {
            IncreaseAmmo(1);
            Destroy(other.gameObject); //Destroy CartuchoObject
        }
    }

    private void IncreaseAmmo(int amount) {
        currentAmmo =currentAmmo+ amount;
    }

    public void SetGordura(bool flag)
    {
        gorduraActivate = flag;
    }


}

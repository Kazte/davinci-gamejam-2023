using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float BulletSpeed = 10f;
    public float ShootCooldown = 0.20f;
    public int StartingAmmo = 5;

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
        if (Input.GetMouseButtonDown(0) && (Time.time - lastShootTime) >= ShootCooldown && (currentAmmo > 0 || gorduraActivate))
        {
            Shooting();
            lastShootTime = Time.time;
            if (gorduraActivate == false)
            {
                ModifyAmmo(-1);
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
        if (other.CompareTag("Ammo"))
        {
            ModifyAmmo(1);
            Destroy(other.gameObject); //Destroy CartuchoObject
            
            GameManager.Instance.RemoveGarbage();
        }
    }

    private void ModifyAmmo(int amount)
    {
        // Para que nunca sea menor a cero ni mayor al maximo
        currentAmmo = Mathf.Clamp(currentAmmo + amount, 0, StartingAmmo);

        // Actualizar el HUD
        HUDManager.Instance.SetAmmo(currentAmmo);
    }

    public void SetGordura(bool flag)
    {
        gorduraActivate = flag;
    }
}
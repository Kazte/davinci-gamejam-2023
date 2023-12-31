using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float BulletSpeed = 10f;
    public float ShootCooldown = 0.20f;
    public int StartingAmmo = 5;

    public ParticleSystem eatParticleSystem;

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
        if (GameManager.Instance.IsPause)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && currentAmmo <= 0 && !gorduraActivate &&
            (Time.time - lastShootTime) >= ShootCooldown)
        {
            lastShootTime = Time.time;
            AudioManager.Instance.Play("No_Shoot");
            this.gameObject.GetComponentInChildren<Animator>().SetTrigger("Shooting");
            HUDManager.Instance.NoAmmo();
        }

        if (Input.GetMouseButtonDown(0) && (Time.time - lastShootTime) >= ShootCooldown &&
            (currentAmmo > 0 || gorduraActivate))
        {
            Shooting();
            lastShootTime = Time.time;
            this.gameObject.GetComponentInChildren<Animator>().SetTrigger("Shooting");
            if (gorduraActivate == false)
            {
                ModifyAmmo(-1);
            }
        }
    }

    void Shooting()
    {
        AudioManager.Instance.Play("Player_Disparo");

        GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);

        Rigidbody bulletRigidBody = bullet.GetComponent<Rigidbody>();

        bulletRigidBody.velocity = transform.forward * BulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ammo"))
        {
            AudioManager.Instance.Play("Take_Trash");
            ModifyAmmo(1);
            Destroy(other.gameObject); //Destroy CartuchoObject

            eatParticleSystem.Play();

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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxDistance = 30f;
    private Vector3 initialPosition;
    public Rigidbody Rb;
    public Transform Body;

    public ParticleSystem bulletDestroyParticleSystem;

    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = -Rb.velocity;

        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

        Body.rotation = Quaternion.Slerp(Body.rotation, rotation, 25f * Time.deltaTime);

        float distance = Vector3.Distance(initialPosition, transform.position);
        if (distance >= maxDistance)
        {
            Destroy(gameObject);
            Instantiate(bulletDestroyParticleSystem, transform.position, Quaternion.identity).Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Instantiate(bulletDestroyParticleSystem, transform.position, Quaternion.identity).Play();
    }
}
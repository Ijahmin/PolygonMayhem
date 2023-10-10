using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireRate = .3f;
    private float fireTime = 0;

    private void Start()
    {
        fireTime = fireRate;
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && fireTime >= fireRate) 
        {
            fireTime = 0;
            Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        }

        if (fireTime < fireRate)
        {
            fireTime += Time.deltaTime;
        }
    }
}

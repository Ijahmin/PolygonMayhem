using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject pivotPoint;
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
            Vector2 fireDirection = (firePoint.transform.position - pivotPoint.transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(pivotPoint.transform.position, fireDirection, Vector2.Distance(pivotPoint.transform.position, firePoint.transform.position), LayerMask.GetMask("Terrain"));
            
            if (hit.collider == null)
            {
                Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            }
        }

        if (fireTime < fireRate)
        {
            fireTime += Time.deltaTime;
        }
    }
}

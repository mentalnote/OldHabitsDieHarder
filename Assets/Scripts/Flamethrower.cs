using UnityEngine;
using System.Collections;

public class Flamethrower : Weapon
{
    [SerializeField]
    private GameObject firePrefab;

    [SerializeField]
    private GameObject firePoint;

    [SerializeField]
    private float fireRate = 4.0f;

    [SerializeField]
    private float firingForce = 10f;

    private float timeTilNextFlame = 0.0f;

    private bool isFiring = false;

    public override void StartUseWeapon()
    {
        isFiring = true;
    }

    public override void EndUseWeapon()
    {
        isFiring = false;
    }

    void Update ()
    {
        this.timeTilNextFlame = this.timeTilNextFlame <= 0.0f ? 0.0f : this.timeTilNextFlame - Time.deltaTime;

        if (this.isFiring && this.timeTilNextFlame <= 0.0f)
        {
            this.timeTilNextFlame = 1.0f / this.fireRate;

            GameObject fire = (GameObject)Instantiate(
                this.firePrefab,
                this.firePoint.transform.position,
                Quaternion.LookRotation(-this.transform.forward, this.transform.up));

            Rigidbody fireRigid = fire.GetComponentInChildren<Rigidbody>();

            if (fireRigid != null)
            {
                fireRigid.AddForce(this.transform.forward * this.firingForce);
            }

            ItBurnsAhhhhhhh fireComp = fire.GetComponentInChildren<ItBurnsAhhhhhhh>();

            if (fireComp != null)
            {
                fireComp.Target = this.gameObject;
            }
        }
    }
}

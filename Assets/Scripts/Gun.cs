using UnityEngine;
using System.Collections;

public class Gun : Weapon {

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject firePoint;

    [SerializeField]
    private float fireRate = 4.0f;

    [SerializeField]
    private float firingForce = 10f;

    [SerializeField]
    private AudioSource gunFireAudio;

    private float timeTilNextShot = 0.0f;

    private bool isFiring = false;

    public override void StartUseWeapon()
    {
        base.StartUseWeapon();
        this.gunFireAudio.Play();
        this.isFiring = true;
    }

    public override void EndUseWeapon()
    {
        base.EndUseWeapon();
        this.gunFireAudio.Stop();
        this.isFiring = false;
    }

    void Update()
    {
        this.timeTilNextShot = this.timeTilNextShot <= 0.0f ? 0.0f : this.timeTilNextShot - Time.deltaTime;

        if (this.isFiring && this.timeTilNextShot <= 0.0f)
        {
            this.timeTilNextShot = 1.0f / this.fireRate;

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(this.firePoint.transform.position, this.transform.forward, out hit, Mathf.Infinity))
            {
                GameObject bullet = (GameObject)Instantiate(
                this.bulletPrefab,
                hit.point,
                Quaternion.LookRotation(this.transform.forward, this.transform.up));

                Destroy(bullet, 0.1f);

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(this.transform.forward * this.firingForce);   
                }
            }

        }
    }
}

using UnityEngine;
using System.Collections;

public class ItBurnsAhhhhhhh : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    public bool IsFireMaster { get; set; }

    public bool IsCombining { get; set; }

    private bool hitGround = false;

    public GameObject Target
    {
        get
        {
            return this.target;
        }

        set
        {
            this.target = value;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ItBurnsAhhhhhhh ahhhh = collision.gameObject.GetComponentInChildren<ItBurnsAhhhhhhh>();
        if (ahhhh != null && this.IsFireMaster && !this.IsCombining && !ahhhh.IsCombining)
        {
            ahhhh.IsFireMaster = false;

            GameObject newFire = (GameObject)Instantiate(
                this.transform.root.gameObject,
                (this.transform.root.position + ahhhh.transform.root.position) * 0.5f,
                Quaternion.identity);

            newFire.transform.localScale = this.transform.root.localScale + ahhhh.transform.root.localScale;

            this.IsCombining = true;
            ahhhh.IsCombining = true;

            Destroy(this.transform.root.gameObject);
            Destroy(ahhhh.transform.root.gameObject);
        }
        else if (!this.hitGround && ahhhh == null && collision.gameObject.name != "Player")
        {
            this.hitGround = true;
            Rigidbody rigidbody = this.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.velocity = Vector3.zero;
            }
        }
    }

    private void Start()
    {
        this.IsFireMaster = true;
    }

    private void Update () {
        this.transform.LookAt(new Vector3(this.target.transform.position.x, this.transform.position.y, this.target.transform.position.z));
    }
}

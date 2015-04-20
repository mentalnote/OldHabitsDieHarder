using UnityEngine;
using System.Collections;

public class Dog : MonoBehaviour
{
    [SerializeField]
    private AudioSource audio = null;

    [SerializeField]
    private Hands playerHands = null;

    [SerializeField]
    private Poop poopPrefab = null;

    [SerializeField]
    private Vector3 poopOffset = Vector3.zero;

    private bool hasPooped = false;

    public bool HasPooped
    {
        get
        {
            return this.hasPooped;
        }
    }

	private void OnCollisionEnter(Collision collision)
	{
        Weapons weaponType = Weapon.GetWeaponType(collision.gameObject);
        if (weaponType != Weapons.None)
        {
            ScenarioManager.GetCurrentScenario().SetFlag(weaponType, DogRoomScenario.Flags.DogDestroyed, true);
		}
	}

    private void Update()
    {
        if (!this.HasPooped && this.playerHands.HeldWeapon != null && this.playerHands.HeldWeapon.WeaponType == Weapons.Gun && Input.GetMouseButtonDown(0))
        {
            Instantiate(poopPrefab, this.transform.position + this.poopOffset, Quaternion.identity);

            this.hasPooped = true;

            this.audio.Stop();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position + this.poopOffset, 0.3f);
    }
}

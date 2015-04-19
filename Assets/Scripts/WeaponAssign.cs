using System.Xml.Serialization;

using UnityEngine;
using System.Collections;

public class WeaponAssign : MonoBehaviour
{

    [SerializeField]
    private Hands hands;

    [SerializeField]
    private Weapon weaponPrefab;

    private void Start()
    {
        if (this.hands != null && this.weaponPrefab != null)
        {
            this.hands.HeldWeapon = this.weaponPrefab;
        }
    }
}

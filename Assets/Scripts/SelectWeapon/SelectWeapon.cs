// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectTool.cs" company="2BitStudios">
//   2015
// </copyright>
// <summary>
//  Script called when weapon is selected from the user interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    [SerializeField]
    private Hands hands;

    [SerializeField]
    private Weapon selectedWeapon;

    public void SelectionFired()
    {
        if (this.selectedWeapon != null & this.hands != null)
        {
            this.hands.HeldWeapon = this.selectedWeapon;
        }
    }

    private void Start()
    {
        if (this.hands == null)
        {
            this.hands = FindObjectOfType<Hands>();    
        }
    }

}

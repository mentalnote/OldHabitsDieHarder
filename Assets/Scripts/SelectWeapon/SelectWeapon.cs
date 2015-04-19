// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectTool.cs" company="2BitStudios">
//   2015
// </copyright>
// <summary>
//  Script called when weapon is selected from the user interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    [SerializeField]
    private Weapons selectedWeapon;

    public void SelectionFired()
    {
        Debug.Log(this.selectedWeapon);
    }

}

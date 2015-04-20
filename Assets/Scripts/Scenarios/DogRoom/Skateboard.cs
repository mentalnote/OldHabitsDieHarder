using UnityEngine;

public sealed class Skateboard : MonoBehaviour
{
    [SerializeField]
    private float resetSpeed = 5.0f;

    [SerializeField]
    private Vector3 resetPosition = Vector3.zero;

    [SerializeField]
    private Vector3 resetRotation = Vector3.zero;

    public bool IsResetting { get; set; }

    private void OnCollisionEnter(Collision collision)
    {
        Weapons weaponType = Weapon.GetWeaponType(collision.gameObject);
        if (weaponType == Weapons.BoxingGloves)
        {
            ScenarioManager.GetCurrentScenario().SetFlag(weaponType, DogRoomScenario.Flags.BoardKnockedAway, true);

            this.resetPosition += new Vector3(-100.0f, 0.0f, 0.0f);

            this.IsResetting = true;
        }
    }

    private void Update()
    {
        if (this.IsResetting)
        {
            Vector3 resetPositionDisplacement = resetPosition - this.transform.position;
            float resetPositionDisplacementMagnitude = resetPositionDisplacement.magnitude;

            float scaledMovementSpeed = this.resetSpeed * Time.deltaTime;

            if (resetPositionDisplacementMagnitude <= scaledMovementSpeed)
            {
                scaledMovementSpeed = resetPositionDisplacementMagnitude;

                this.transform.rotation = Quaternion.Euler(this.resetRotation);

                this.IsResetting = false;
            }
            else
            {
                this.transform.rotation = Quaternion.LookRotation(resetPositionDisplacement.normalized);
            }
            
            this.transform.position += resetPositionDisplacement * (scaledMovementSpeed / resetPositionDisplacementMagnitude);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(this.resetPosition, 0.5f);
    }
}

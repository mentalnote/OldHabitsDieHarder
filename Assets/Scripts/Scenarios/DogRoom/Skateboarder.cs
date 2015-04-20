using UnityEngine;

public sealed class Skateboarder : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 1.0f;

    [SerializeField]
    private Vector3[] waypoints = null;

    private int currentWaypointIndex = 0;

    [SerializeField]
    private Skateboard skateboard = null;

    private void OnCollisionEnter(Collision collision)
    {
        Weapons weaponType = Weapon.GetWeaponType(collision.gameObject);
        if (weaponType == Weapons.Bat)
        {
            ScenarioManager.GetCurrentScenario().SetFlag(weaponType, DogRoomScenario.Flags.BoardRetrieved, true);

            this.skateboard.transform.parent = null;
            this.skateboard.IsResetting = true;

            this.transform.rotation *= Quaternion.Euler(0.0f, 0.0f, -90.0f);

            MonoBehaviour.Destroy(this);
        }
    }

    private void Update()
    {
        Vector3 currentWaypoint = this.waypoints[this.currentWaypointIndex];

        Vector3 currentWaypointDisplacement = currentWaypoint - this.transform.position;
        float currentWaypointDisplacementMagnitude = currentWaypointDisplacement.magnitude;

        float scaledMovementSpeed = this.movementSpeed * Time.deltaTime;

        if (currentWaypointDisplacementMagnitude <= scaledMovementSpeed)
        {
            scaledMovementSpeed = currentWaypointDisplacementMagnitude;

            ++this.currentWaypointIndex;

            if (this.currentWaypointIndex >= waypoints.Length)
            {
                this.currentWaypointIndex = 0;
            }
        }

        this.transform.position += currentWaypointDisplacement * (scaledMovementSpeed / currentWaypointDisplacementMagnitude);
        this.transform.rotation = Quaternion.LookRotation(currentWaypointDisplacement);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < this.waypoints.Length; ++i)
        {
            Gizmos.DrawWireSphere(this.waypoints[i], 0.5f);
        }
    }
}

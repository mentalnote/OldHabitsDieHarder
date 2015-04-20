using UnityEngine;

public sealed class Poop : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Skateboard>() == null)
        {
            ScenarioManager.GetCurrentScenario().SetFlag(Weapons.Gun, DogRoomScenario.Flags.PoopMissed, true);
        }
        else
        {
            ScenarioManager.GetCurrentScenario().SetFlag(Weapons.Gun, DogRoomScenario.Flags.PoopCaught, true);

            this.transform.parent = collision.transform;
            this.transform.GetComponent<Rigidbody>().isKinematic = true;
        }

        Destroy(this);
    }
}

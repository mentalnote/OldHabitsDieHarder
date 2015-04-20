using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animation))]
public class Table : MonoBehaviour {
    //Which curtain flag we set
    public CandleRoomScenario.Flags curtainFlag;

    [SerializeField]
    private Chair chair = null;

    [SerializeField]
    private string moveAnimationName = "";

    [SerializeField]
    private string shakeAnimationName = "";

    private bool moved = false;

    public bool Moved
    {
        get
        {
            return this.moved;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Weapon.GetWeaponType(collision.gameObject) == Weapons.BoxingGloves)
        {
            if (!moved)
            {
                if (chair.Broken)
                {
                    this.GetComponent<Animation>().Play(moveAnimationName);

                    moved = true;

                    //Set the flag to indicate this curtain is on fire
                    ScenarioManager.GetCurrentScenario().SetFlag(Weapons.BoxingGloves, this.curtainFlag, true);
                }
                else
                {
                    this.GetComponent<Animation>().Play(shakeAnimationName);
                }
            }
        }
    }
}

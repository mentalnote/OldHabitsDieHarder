using UnityEngine;
using UnityEngine.UI;

using UnityStandardAssets.Characters.FirstPerson;

public sealed class MoveTools : MonoBehaviour
{
    [SerializeField]
    private RectTransform root = null;

    [SerializeField]
    private Vector2 normalizedPosition = Vector2.zero;
    private bool show = true;

    [SerializeField]
    private Image buttonImage;

    [SerializeField]
    private Sprite showButton;

    [SerializeField]
    private Sprite hideButton;

    [SerializeField]
    private FirstPersonController firstPersonController;

    public Vector2 NormalizedPosition
    {
        get
        {
            return this.normalizedPosition;
        }

        set
        {
            this.normalizedPosition = value;
        }
    }

    public Vector3 NormalizedPosition3D
    {
        get
        {
            return this.normalizedPosition;
        }

        set
        {
            this.normalizedPosition = value;
        }
    }

    public void Move()
    {
        this.normalizedPosition = this.show ? new Vector2(0, 0) : new Vector2(0, -0.75f);
        this.buttonImage.overrideSprite = this.show ? this.hideButton : this.showButton;
        this.firstPersonController.enabled = !this.show;
        this.show = !this.show;
    }

    private void Start()
    {
        this.Update();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            this.Move();
        }

        this.root.anchoredPosition = new Vector2(
            Screen.width * this.normalizedPosition.x,
            Screen.height * this.normalizedPosition.y);
    }

}

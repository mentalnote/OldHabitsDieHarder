using UnityEngine;

public sealed class MoveTools : MonoBehaviour
{
    [SerializeField]
    private RectTransform root = null;

    [SerializeField]
    private Vector2 normalizedPosition = Vector2.zero;
    private bool show = true;

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
        this.show = !this.show;
    }

    private void Start()
    {
        this.Update();
    }

    private void Update()
    {
        this.root.anchoredPosition = new Vector2(
            Screen.width * this.normalizedPosition.x,
            Screen.height * this.normalizedPosition.y);
    }

}

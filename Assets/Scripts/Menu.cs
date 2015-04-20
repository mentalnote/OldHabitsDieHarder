using UnityEngine;

public sealed class Menu : MonoBehaviour
{
    public void LoadLevel(string levelName)
    {
        Application.LoadLevel(levelName);
    }
}

using UnityEngine;

public sealed class Menu : MonoBehaviour
{
    public void LoadLevel(string levelName)
    {
        Application.LoadLevel(levelName);
    }
	
	private void Start()
	{
		Screen.lockCursor = false;
	}
}

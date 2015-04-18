using UnityEngine;
using System.Collections;

public class SelectTool : MonoBehaviour {
	private string selectedTool;
	// Use this for initialization
	void Start () {
		
	}	

	public void UpdateSelection(string selection){
		selectedTool = selection;
		Debug.Log (selectedTool);
	}
	// Update is called once per frame
	void Update () {
	}
}

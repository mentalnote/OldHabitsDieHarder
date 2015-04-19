using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{
	[SerializeField]
	private GameObject target;

	private void Update() {
		this.transform.LookAt(new Vector3(this.target.transform.position.x, this.transform.position.y, this.target.transform.position.z));
	}
}

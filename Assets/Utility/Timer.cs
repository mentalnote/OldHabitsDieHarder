using UnityEngine;
using System.Collections;

public delegate void TimerDelegate();

public class Timer : MonoBehaviour
{
	public static void SetTimer(float timerDuration, GameObject parent, TimerDelegate handler)
	{
		Timer t = parent.AddComponent<Timer>();
		t.targetTime = Time.time + timerDuration;
		t.handler = handler;
	}

	protected float targetTime = 0.0f;
	protected TimerDelegate handler;
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time >= this.targetTime)
		{
			this.handler();
			Destroy(this);
		}
	}
}

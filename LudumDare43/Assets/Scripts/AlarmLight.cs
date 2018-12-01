using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLight : MonoBehaviour {

    public float fadeSpeed = 2f;
    public float highIntensity = 2f;
    public float lowIntensity = 0.5f;
    public float changeMargin = 0.2f;
	public AnimationCurve curve;

    public bool alarmOn;

    private float targetIntensity;
    private Light l;
	private float x = 0.0f;
	public float frequency = 1.00f;
    private void Awake()
    {
        l = GetComponent<Light>();
        l.intensity = 0f;
        targetIntensity = highIntensity; //Inicialmente es 0
    }

    private void Update()
    {
		x = Mathf.Sin(frequency * Time.time);

        if (alarmOn)
        {
			l.intensity = curve.Evaluate(x);//Mathf.Lerp(l.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
			//CheckTargetIntensity();
		}
		else
		{
			l.intensity = Mathf.Lerp(l.intensity, 0f, fadeSpeed * Time.deltaTime);
		}
    }

	void CheckTargetIntensity()
	{
		if (Mathf.Abs(targetIntensity - l.intensity) < changeMargin)
		{
			if (targetIntensity == highIntensity)
			{
				targetIntensity = lowIntensity;
			}
			else
			{
				targetIntensity = highIntensity;
			}
		}
	}
}

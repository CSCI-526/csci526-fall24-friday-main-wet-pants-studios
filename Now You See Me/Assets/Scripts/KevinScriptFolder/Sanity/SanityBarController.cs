using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityBarController : MonoBehaviour
{
    public Slider slider;

	// Start is called before the first frame update
	public void SetMaxHealth(float health)
	{
		slider.maxValue = health;
		slider.value = health;

		//fill.color = gradient.Evaluate(1f);
	}

	public void SetHealth(float health)
	{
		slider.value = health;

		//fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}

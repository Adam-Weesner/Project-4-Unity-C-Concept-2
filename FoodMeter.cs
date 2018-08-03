using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodMeter : MonoBehaviour {

    public GameObject slider;
    [HideInInspector] public bool isWaiting = false;

	// Use this for initialization
	void Start () {
        slider.GetComponent<Slider>().value = 100;
        slider.GetComponent<Slider>().interactable = false;
        StartCoroutine(CountDown());
	}

    IEnumerator CountDown()
    {
        for (slider.GetComponent<Slider>().value = 100; slider.GetComponent<Slider>().value > 0; slider.GetComponent<Slider>().value--)
        {
            yield return new WaitForSeconds(1);
            while (isWaiting)
                yield return new WaitForSeconds(2);
            yield return new WaitForSeconds(1);
        }

        print("Game Over!");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialMeter : MonoBehaviour
{
    public Slider slider;

    public void SetMaxMeter(int socialPoint)
    {
        slider.maxValue = socialPoint;
        slider.value = socialPoint;
    }

    public void SetMeter(int socialPoint)
    {
        slider.value = socialPoint;
    }
}

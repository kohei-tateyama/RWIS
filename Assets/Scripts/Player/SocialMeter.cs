using UnityEngine;
using UnityEngine.UI;

public class SocialMeter : MonoBehaviour
{
    public Slider slider;

    public void SetMaxSocialPoints(int maxSocialPoints){
        slider.maxValue = maxSocialPoints;
        slider.value    = maxSocialPoints;
    }

    public void SetSocialPoints(int socialPoints){
        slider.value = socialPoints;
    }

}

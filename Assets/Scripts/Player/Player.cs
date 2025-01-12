using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CapsuleCollider2D capsuleCollider { get; private set; }
    public PlayerMovement movement { get; private set; }
    public PlayerSpriteRenderer playerSpriteRenderer;
    
    public SocialMeter socialMeter;
    private int maxSocialPoints, currentSocialPoints, updatedSocialPoints;
    
    public BatteryBar batteryBar;
    public int maxHearingDeviceBattery = 100;
    private int currentHearingDeviceBattery;


    private void Start(){
        maxSocialPoints = ((Ink.Runtime.IntValue) DialogueManager.Instance.GetVariableState("social_meter")).value;
        currentSocialPoints = maxSocialPoints;
        socialMeter.SetMaxSocialPoints(maxSocialPoints);
        
        currentHearingDeviceBattery = maxHearingDeviceBattery;
        batteryBar.SetMaxBattery(maxHearingDeviceBattery);
    }

    private void Update(){
        // Add here the logic to handle when socialBattery/hearingDeviceBattery go to 0
        
        // if (currentSocialPoints <= 0){
        //     Death();
        // }

        updatedSocialPoints = ((Ink.Runtime.IntValue) DialogueManager.Instance.GetVariableState("social_meter")).value;
        if (updatedSocialPoints != currentSocialPoints)
        {
            currentSocialPoints = updatedSocialPoints;
            socialMeter.SetSocialPoints(currentSocialPoints);
        }
    }
    
    private void Battery(int battery){
        currentHearingDeviceBattery += battery;
        batteryBar.SetBattery(currentHearingDeviceBattery);
    }
    
    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        movement = GetComponent<PlayerMovement>();
    }

}

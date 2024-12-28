using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CapsuleCollider2D capsuleCollider { get; private set; }
    public PlayerMovement movement { get; private set; }
    public DeathAnimation deathAnimation { get; private set; }

    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    private PlayerSpriteRenderer activeRenderer;

    public bool big => bigRenderer.enabled;
    public bool dead => deathAnimation.enabled;
    public int maxHealth = 100;
    public int currentHealth;
    public SocialMeter socialMeter;
    
    public BatteryBar batteryBar;
    public int maxBattery = 100;
    public int currentBattery;


    private void Start(){
        currentHealth = maxHealth;
        socialMeter.SetMaxHealth(maxHealth);
        currentBattery = maxBattery;
        batteryBar.SetMaxBattery(maxBattery);

        // Subscribe to the event to get updates when SocialMeterValue changes
        DialogueManager.Instance.OnSocialValueChangedEvent += OnSocialValueChanged;
    }

    private void Update(){
        if (currentHealth <= 0){
            Death();
        }
        
        // if (InputManager.Instance.GetInteractPressed()){
        //     // SocialPoints(20);
        //     Battery(-20);
        // }
    }

    private void SocialPoints(int points){
        currentHealth += points;
        socialMeter.SetHealth(currentHealth);
    }
    
    private void Battery(int battery){
        currentBattery += battery;
        batteryBar.SetBattery(currentBattery);
    }
    
    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        movement = GetComponent<PlayerMovement>();
        deathAnimation = GetComponent<DeathAnimation>();
        activeRenderer = smallRenderer;
    }

    public void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);
    }



    void OnSocialValueChanged(int newSocialMeterValue)
    {
        SocialPoints(-20);
    }

    void OnDestroy()
    {
        // Unsubscribe from the event to prevent memory leaks
        DialogueManager.Instance.OnSocialValueChangedEvent -= OnSocialValueChanged;
    }

}

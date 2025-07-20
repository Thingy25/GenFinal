using System;
using UnityEngine;
using TMPro;
public class PlayerOxygen : MonoBehaviour, IDamageable
{
    public delegate void PlayerDeathEvent();
    public static event PlayerDeathEvent OnPlayerDeath;
    public delegate void UpdateUI(int currentOxygen);
    public static event UpdateUI OnUIValueChanged;

    private float oxygen = 100;
    readonly float maxOxygen = 100;
    //[SerializeField] private TextMeshProUGUI oxygenText;
    [SerializeField]
    GameObject helmet; //Put helmet on + update UI text & add recharge functionality

    bool hasHelmet = false;
    int health = 100;
    
    
    void Start()
    {
        PlayerHelmet.OnHelmetEnabled += PutHelmetOn;
        UpdateOxygenText();
    }
    
    void Update()
    {
        if (!hasHelmet)
        {
            oxygen -= 0.5f * Time.deltaTime;
            UpdateOxygenText();
        }
        
    }

    void UpdateOxygenText()
    {
        int oxygenInt = (int)oxygen;
        OnUIValueChanged?.Invoke(oxygenInt);
        //oxygenText.text = oxygenInt + " %";
    }

    void IDamageable.ReceiveDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        Mathf.Clamp(health, 0, 100);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        OnPlayerDeath?.Invoke();
        HudManager.Instance.ActivateDeathPanel();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            oxygen -= 3;
        }
    }

    void PutHelmetOn()
    {
        helmet.SetActive(true);
        hasHelmet = true;
    }

    private void OnDisable()
    {
        PlayerHelmet.OnHelmetEnabled -= PutHelmetOn;
    }
}

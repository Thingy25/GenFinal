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
    GameObject helmet; 

    bool hasHelmet = false;
    
    
    void Start()
    {
        PlayerHelmet.OnHelmetEnabled += PutHelmetOn;
        UpdateOxygenText();
    }
    
    void Update()
    {
        if (!hasHelmet)
        {
            oxygen -= 1.2f * Time.deltaTime;
            Mathf.Clamp(oxygen, 0, maxOxygen);
            UpdateOxygenText();
            if (oxygen <= 0)
            {
                Die();
            }
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
        oxygen -= damage;
        Mathf.Clamp(oxygen, 0, maxOxygen);
        if (oxygen <= 0)
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
        //if (other.gameObject.CompareTag("Fire"))
        //{
        //    oxygen -= 3;
        //}
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

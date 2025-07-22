using System;
using UnityEngine;
using TMPro;
using System.Collections;
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
            oxygen -= 0.5f * Time.deltaTime;
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
        Mathf.Clamp(oxygenInt, 0, maxOxygen);
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

    void PutHelmetOn()
    {
        helmet.SetActive(true);
        hasHelmet = true;
        StartCoroutine(ReplenishOxygen());
    }

    private void OnDisable()
    {
        PlayerHelmet.OnHelmetEnabled -= PutHelmetOn;
    }

    IEnumerator ReplenishOxygen()
    {
        while (oxygen < maxOxygen)
        {
            yield return new WaitForSeconds(0.5f);
            oxygen++;
            Mathf.Clamp(oxygen, 0, maxOxygen);
            UpdateOxygenText();
        }
    }
}

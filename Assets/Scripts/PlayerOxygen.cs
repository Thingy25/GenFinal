using System;
using UnityEngine;
using TMPro;
public class PlayerOxygen : MonoBehaviour
{
    private float oxygen = 100;
    [SerializeField] private TextMeshProUGUI oxygenText;
    
    
    void Start()
    {
        UpdateOxygenText();
    }
    
    void Update()
    {
        oxygen -= 0.5f * Time.deltaTime;
        UpdateOxygenText();
    }

    void UpdateOxygenText()
    {
        int oxygenInt = (int)oxygen;
        oxygenText.text = oxygenInt + " %";
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            oxygen -= 3;
        }
    }
}

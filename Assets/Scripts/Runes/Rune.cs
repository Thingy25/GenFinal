using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    public bool isActivate = false;
    [Range(1, 5)] public int runeValue;

    public float minIntensity = -10f;
    public float maxIntensity = 1.5f;
    public float duration = 1f;
    
    private Material material;
    private Color baseEmissionColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
        
        material.EnableKeyword("_EMISSION");
        
        baseEmissionColor = material.GetColor("_EmissionColor");
        StartCoroutine(FadeEmission(minIntensity, duration));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable") && other.TryGetComponent<RuneKey>(out RuneKey runeKey))
        {
            if (runeKey.value == runeValue)
            {
                isActivate = true;
                StartCoroutine(FadeEmission(maxIntensity, duration));
                RunesManager.Instance.OneMoreActivated();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable") && other.TryGetComponent<RuneKey>(out RuneKey runeKey))
        {
            if (runeKey.value == runeValue)
            {
                isActivate = false;
                StartCoroutine(FadeEmission(minIntensity, duration));
                RunesManager.Instance.OneMoreActivated();
            }
        }
    }

    IEnumerator FadeEmission(float intensityOffset, float duration)
    {
        Color startColor = material.GetColor("_EmissionColor");
        Color endColor = baseEmissionColor * Mathf.Pow(2, intensityOffset); 
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            Color lerped = Color.Lerp(startColor, endColor, t);
            material.SetColor("_EmissionColor", lerped);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        
        material.SetColor("_EmissionColor", endColor);
    }
    
    
}

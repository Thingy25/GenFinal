using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    [SerializeField] private TMP_Text interactionText;
    
    public static HudManager Instance { get; private set; }
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public void EnableInteraction(string text)
    {
        interactionText.text = text;
        interactionText.gameObject.SetActive(true);
    }
    public void DisableInteractionText()
    {
        interactionText.gameObject.SetActive(false);
    }
  
}

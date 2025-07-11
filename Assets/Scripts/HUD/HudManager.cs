using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HudManager : MonoBehaviour
{
    [SerializeField] private TMP_Text interactionText;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject crossHair;
    public bool isGamePaused = false;
    public bool canPause = true;
    
    
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

    public void PauseGame()
    {
        if (canPause)
        {
            if (!isGamePaused)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0f;
                isGamePaused = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1f;
                isGamePaused = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
       
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ActivateCrossHair()
    {
        crossHair.SetActive(true);
    }

    public void DesactiveCrossHair()
    {
        crossHair.SetActive(false);
    }
    
  
}

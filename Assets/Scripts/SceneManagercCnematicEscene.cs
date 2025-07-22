using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagercCnematicEscene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        Time.timeScale = 1;
        if (Time.timeScale != 1)
        {
        }
        SceneManager.LoadScene("Nivel1");
    }
}

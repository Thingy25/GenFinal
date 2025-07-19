using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public delegate void LevelBeatChange(int levelBeat);
    public static event LevelBeatChange OnLevelBeatChange;

    int currentLvlBeat = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    public void CallNextLevelBeat()
    {
        currentLvlBeat++;
        OnLevelBeatChange?.Invoke(currentLvlBeat);
    }
}

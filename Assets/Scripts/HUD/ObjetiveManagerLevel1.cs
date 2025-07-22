using System;
using Unity.VectorGraphics;
using UnityEngine;

public class ObjetiveManagerLevel1 : MonoBehaviour
{
    public Sprite extintorSprite;
    public Sprite puzzleSprite;
    public Sprite cascoSprite;
    public Sprite panelSprite;

    public SVGImage objectiveImage;

    private int helmetCounter = 0;

    public static ObjetiveManagerLevel1 Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectiveImage.sprite = extintorSprite;
    }

    public void SetPuzzleSprite()
    {
        objectiveImage.sprite = puzzleSprite;
    }
    public void SetHelmetSprite()
    {
        helmetCounter += 1;
        if (helmetCounter == 3)
        {
            objectiveImage.sprite = cascoSprite;
        }
       
    }
    public void SetPanelSprite()
    {
        objectiveImage.sprite = panelSprite;
    }
}

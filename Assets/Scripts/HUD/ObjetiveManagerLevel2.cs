using System;
using UnityEngine;
using Unity.VectorGraphics;

public class ObjetiveManagerLevel2 : MonoBehaviour
{
    
    public Sprite runeSprite;
    public Sprite motorSprite;

    public SVGImage objectiveImage;
    
    public static ObjetiveManagerLevel2 Instance;

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

    private void Start()
    {
        SetRuneSprite();
    }

    private void SetRuneSprite()
    {
        objectiveImage.sprite = runeSprite;
    }

    public void SetMotorSprite()
    {
        objectiveImage.sprite = motorSprite;
    }
}

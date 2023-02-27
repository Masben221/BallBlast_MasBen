using UnityEngine;
using System.Collections;

public class StoneColorChange : MonoBehaviour
{
    [SerializeField] private Color[] stoneColors;      
    [SerializeField] SpriteRenderer spriteRenderer;    
    
    public void ChangeColor()
    {
        int index = Random.Range(0, stoneColors.Length);
        spriteRenderer.color = stoneColors[index];                
    }   
}


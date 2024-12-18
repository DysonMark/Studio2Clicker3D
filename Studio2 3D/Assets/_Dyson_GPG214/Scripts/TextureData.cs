using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class TextureData : MonoBehaviour
{
    public Image imageRenderer;
    public Texture2D originalTexture;
    private Texture2D invertedTexture;

    private void Start()
    {
        if (imageRenderer == null || originalTexture == null)
        {
            Debug.Log("No texture or renderer assigned");
            return;
        }
        
        //create a new texture that is the same size as original
        invertedTexture = new Texture2D(originalTexture.width, originalTexture.height);
        
        //invert the colors
        
        InvertTexture(originalTexture, invertedTexture);
        
        //then apply the texture to my sprite renderer

        imageRenderer.sprite = TextureToSprite(invertedTexture);
    }

    void InvertTexture(Texture2D original, Texture2D inverted)
    {
        for (int y = 0; y < original.height; y++)
        {
            for (int x = 0; x < original.width; x++)
            {
                Color originalColor = original.GetPixel(x, y); // read a texture
                Color invertedColor = new Color(1f - originalColor.r, 1f -  originalColor.g, 1f - originalColor.b, originalColor.a);
                invertedTexture.SetPixel(x,y, invertedColor); // write to a texture
            }
        }
        inverted.Apply();
    }
    private Sprite TextureToSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }

}

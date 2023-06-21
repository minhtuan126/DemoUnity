using System;
using System.Collections.Generic;
using UnityEngine;

public static class JsonHelper
{
    public static TextureData SerializeTexture(Sprite sprite)
    {
        Texture2D tex = sprite.texture;
        var exportObj = new TextureData();
        exportObj.x = tex.width;
        exportObj.y = tex.height;
        exportObj.bytes = ImageConversion.EncodeToPNG(tex);
        return exportObj;

    }

    public static Sprite DeSerializeTexture(TextureData importObj)
    {
        Texture2D tex = new Texture2D(importObj.x, importObj.y);
        ImageConversion.LoadImage(tex, importObj.bytes);
        Sprite mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), Vector2.one);
        return mySprite;
    }
}



[Serializable]
public class TextureData
{
    [SerializeField]
    public int x;
    [SerializeField]
    public int y;
    [SerializeField]
    public byte[] bytes;
}


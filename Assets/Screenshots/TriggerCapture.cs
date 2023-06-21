// The MIT License (MIT)
// Copyright (c) 2014 Brad Nelson and Play-Em Inc.
// CaptureScreenshot is based on Brad Nelson's MIT-licensed AnimationToPng: http://wiki.unity3d.com/index.php/AnimationToPNG
// AnimationToPng is based on Twinfox and bitbutter's Render Particle to Animated Texture Scripts.

using UnityEngine;
using System.IO;
using System;

// [ExecuteInEditMode]
public class TriggerCapture : MonoBehaviour
{
    public Camera camera;
    public bool UseSimple = false;
    public void OnTakeScreen(string name)
    {
        string filename = string.Format("Assets/_Game/Assets/Sprites/FishAvatars/FishAvatar/{0}.png",  name);
        Debug.Log(filename);
        int width = Screen.width;
        int height = Screen.height;
        if (UseSimple)
        {
            CaptureScreenshot.SimpleCaptureTransparentScreenshot(camera, width, height, filename);
        }
        else
        {
            CaptureScreenshot.CaptureTransparentScreenshot(camera, width, height, filename);
        }
    }
}

public static class CaptureScreenshot
{
    public static void CaptureTransparentScreenshot(Camera cam, int width, int height, string screengrabfile_path)
    {
        // This is slower, but seems more reliable.
        var bak_cam_targetTexture = cam.targetTexture;
        var bak_cam_clearFlags = cam.clearFlags;
        var bak_RenderTexture_active = RenderTexture.active;

        var tex_white = new Texture2D(width, height, TextureFormat.ARGB32, false);
        var tex_black = new Texture2D(width, height, TextureFormat.ARGB32, false);
        var tex_transparent = new Texture2D(width, height, TextureFormat.ARGB32, false);
        // Must use 24-bit depth buffer to be able to fill background.
        var render_texture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32);
        var grab_area = new Rect(0, 0, width, height);

        RenderTexture.active = render_texture;
        cam.targetTexture = render_texture;
        cam.clearFlags = CameraClearFlags.SolidColor;

        cam.backgroundColor = Color.black;
        cam.Render();
        tex_black.ReadPixels(grab_area, 0, 0);
        tex_black.Apply();

        cam.backgroundColor = Color.white;
        cam.Render();
        tex_white.ReadPixels(grab_area, 0, 0);
        tex_white.Apply();

        // Create Alpha from the difference between black and white camera renders
        for (int y = 0; y < tex_transparent.height; ++y)
        {
            for (int x = 0; x < tex_transparent.width; ++x)
            {
                float alpha = tex_white.GetPixel(x, y).r - tex_black.GetPixel(x, y).r;
                alpha = 1.0f - alpha;
                Color color;
                if (alpha == 0)
                {
                    color = Color.clear;
                }
                else
                {
                    color = tex_black.GetPixel(x, y) / alpha;
                }
                color.a = alpha;
                tex_transparent.SetPixel(x, y, color);
            }
        }

        // Encode the resulting output texture to a byte array then write to the file
        byte[] pngShot = ImageConversion.EncodeToPNG(tex_transparent);
        File.WriteAllBytes(screengrabfile_path, pngShot);

        cam.clearFlags = bak_cam_clearFlags;
        cam.targetTexture = bak_cam_targetTexture;
        RenderTexture.active = bak_RenderTexture_active;
        RenderTexture.ReleaseTemporary(render_texture);

        Texture2D.DestroyImmediate(tex_black);
        Texture2D.DestroyImmediate(tex_white);
        Texture2D.DestroyImmediate(tex_transparent);
    }

    public static void SimpleCaptureTransparentScreenshot(Camera cam, int width, int height, string screengrabfile_path)
    {
        // Depending on your render pipeline, this may not work.
        var bak_cam_targetTexture = cam.targetTexture;
        var bak_cam_clearFlags = cam.clearFlags;
        var bak_RenderTexture_active = RenderTexture.active;

        var tex_transparent = new Texture2D(width, height, TextureFormat.ARGB32, false);
        // Must use 24-bit depth buffer to be able to fill background.
        var render_texture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32);
        var grab_area = new Rect(0, 0, width, height);

        RenderTexture.active = render_texture;
        cam.targetTexture = render_texture;
        cam.clearFlags = CameraClearFlags.SolidColor;

        // Simple: use a clear background
        cam.backgroundColor = Color.clear;
        cam.Render();
        tex_transparent.ReadPixels(grab_area, 0, 0);
        tex_transparent.Apply();

        // Encode the resulting output texture to a byte array then write to the file
        byte[] pngShot = ImageConversion.EncodeToPNG(tex_transparent);
        File.WriteAllBytes(screengrabfile_path, pngShot);

        cam.clearFlags = bak_cam_clearFlags;
        cam.targetTexture = bak_cam_targetTexture;
        RenderTexture.active = bak_RenderTexture_active;
        RenderTexture.ReleaseTemporary(render_texture);

        Texture2D.DestroyImmediate(tex_transparent);
    }
}

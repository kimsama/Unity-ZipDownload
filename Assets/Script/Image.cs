using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// A simple image loader class.
/// </summary>
public class Image 
{
    /// <summary>
    /// Load png file with the given path.
    /// </summary>
    public static Texture2D LoadPNG(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);

            // automatically resize the texture by its dimensions.
            tex.LoadImage(fileData); 
        }
        return tex;
    }
}

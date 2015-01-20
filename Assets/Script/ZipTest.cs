using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

/// <summary>
/// 
/// </summary>
public class ZipTest : MonoBehaviour
{
    public MeshRenderer renderer;
    public string url = "http://www.yoursite.com/files/test.zip";
    public string imgFile = "";

    private bool isUnzipped = false;

    delegate void OnFinish();
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Download(url, GetFileName(url), OnDownloadDone));
    }

    void OnDownloadDone()
    {
        if (renderer != null)
        {
            // load unziped image file and assign it to the material's main texture.
            string path = Application.persistentDataPath + "/" + imgFile;
            renderer.material.mainTexture = LoadPNG(path);

            isUnzipped = false;
        }
    }

    /// <summary>
    /// Retrieves only file name from the given url.
    /// e.g. get 'test.zip' from 'http://www.yoursite.com/files/test.zip'
    /// </summary>
    string GetFileName(string url)
    {
        string file = string.Empty;
        Uri u = new Uri(url);
        file = Path.GetFileName(u.AbsolutePath);
        return file;
    }

    IEnumerator Download(string url, string file, OnFinish onFinish)
    {
        WWW www = new WWW(url);

        yield return www;

        if (www.isDone && !isUnzipped)
        {
            Debug.Log("Downloading of " + file + " is completed.");

            byte[] data = www.bytes;

            string docPath = Application.persistentDataPath;
            docPath += "/" + file;

            Debug.Log("Downloaded file path: " + docPath);

            System.IO.File.WriteAllBytes(docPath, data);

            using (ZipInputStream s = new ZipInputStream(File.OpenRead(docPath)))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    Console.WriteLine(theEntry.Name);

                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    // create directory
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    if (fileName != String.Empty)
                    {
                        string filepath = Path.GetDirectoryName(docPath);
                        filepath += "/";
                        filepath += theEntry.Name;
                        Debug.Log("Unzipping: " + filepath);
                        using (FileStream streamWriter = File.Create(filepath))
                        {
                            int size = 2048;
                            byte[] fdata = new byte[2048];
                            while (true)
                            {
                                size = s.Read(fdata, 0, fdata.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(fdata, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                isUnzipped = true;
            }

            if (onFinish != null)
            {
                onFinish();
            }

            // delete zip file.
            System.IO.File.Delete(docPath);
        }
    }

    /// <summary>
    /// Load png file with the given path.
    /// </summary>
    Texture2D LoadPNG(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;
 
        if (File.Exists(filePath))
        {
             fileData = File.ReadAllBytes(filePath);
             tex = new Texture2D(2, 2);
             tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
         }
         return tex;
    }

}

using UnityEngine;
using System;
using System.Collections;
using System.IO;

/// <summary>
/// Downloading zipped image files and do unzip under persistence data path 
/// then load the specified image file.
/// </summary>
public class SimpleZipDownloader : MonoBehaviour
{
    public MeshRenderer renderer;
    public string url = "http://www.yoursite.com/files/test.zip";
    public string imgFile = "";

    delegate void OnFinish();

    void Start()
    {
        StartCoroutine(Download(url, OnDownloadDone, true));
    }

    /// <summary>
    /// Called when the downloaded zip file is unzipping is finished.
    /// </summary>
    void OnDownloadDone()
    {
        if (renderer != null)
        {
            // load unzipped image file and assign it to the material's main texture.
            string path = Application.persistentDataPath + "/" + imgFile;
            renderer.material.mainTexture = Image.LoadPNG(path);
        }
    }

    /// <summary>
    /// Download ZIP file from the given URL and do calling passed delegate.
    /// 
    /// NOTE: This does not resolve an error such as '404 Not Found'.
    /// </summary>
    IEnumerator Download(string url, OnFinish onFinish, bool remove)
    {
        WWW www = new WWW(url);

        yield return www;

        if (www.isDone)
        {
            byte[] data = www.bytes;

            string file = UriHelper.GetFileName(url);
            Debug.Log("Downloading of " + file + " is completed.");

            string docPath = Application.persistentDataPath;
            docPath += "/" + file;

            Debug.Log("Downloaded file path: " + docPath);

            ZipFile.UnZip(docPath, data);

            if (onFinish != null)
            {
                onFinish();
            }

            if (remove)
            {
                // delete zip file.
                System.IO.File.Delete(docPath);
            }
        }
    }
}

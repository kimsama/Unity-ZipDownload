using UnityEngine;
using System.Collections;
using UniRx;

/// <summary>
/// 
/// </summary>
public class UniRxZipDownloader : TypedMonoBehaviour
{
    public MeshRenderer renderer;
    public string url = "http://www.yoursite.com/files/test.zip";
    public string imgFile = "";

	public override void Start () 
    {
        var query = ObservableWWW.GetAndGetBytes(url);
        query.Subscribe(bytes => {

            string file = UriHelper.GetFileName(url);
            Debug.Log("Downloading of " + file + " is completed.");

            string docPath = Application.persistentDataPath;
            docPath += "/" + file;
            Debug.Log("Downloaded file path: " + docPath);

            ZipFile.UnZip(docPath, bytes);

            if (renderer != null)
            {
                // load unzipped image file and assign it to the material's main texture.
                string path = Application.persistentDataPath + "/" + imgFile;
                renderer.material.mainTexture = Image.LoadPNG(path);
            }

        });
	}
	
}

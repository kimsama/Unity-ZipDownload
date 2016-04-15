using UnityEngine;
using System.Collections;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

/// <summary>
/// A simple zip file helper class.
/// </summary>
public class ZipFile 
{
    /// <summary>
    /// Write the given bytes data under the given filePath. 
    /// The filePath should be given with its path and filename. (e.g. c:/tmp/test.zip)
    /// </summary>
    public static void UnZip(string filePath, byte[] data)
    {
        using (ZipInputStream s = new ZipInputStream(new MemoryStream(data)))
        {
            ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                #if UNITY_EDITOR
                Debug.LogFormat("Entry Name: {0}", theEntry.Name);
                #endif

                string directoryName = Path.GetDirectoryName(theEntry.Name);
                string fileName = Path.GetFileName(theEntry.Name);

                // create directory
                if (directoryName.Length > 0)
                {
                    var dirPath = Path.Combine (filePath, directoryName);

                    #if UNITY_EDITOR
                    Debug.LogFormat("CreateDirectory: {0}", dirPath);
                    #endif

                    Directory.CreateDirectory(dirPath);
                }

                if (fileName != string.Empty)
                {
                    // retrieve directory name only from persistence data path.
                    var entryFilePath = Path.Combine(filePath, theEntry.Name);
                    using (FileStream streamWriter = File.Create(entryFilePath))
                    {
                        int size = 2048;
                        byte[] fdata = new byte[size];
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
            } //end of while
        } //end of using
    }
}

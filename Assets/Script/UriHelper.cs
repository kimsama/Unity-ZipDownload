
using System.Collections;
using System;
using System.IO;

public class UriHelper 
{

    /// <summary>
    /// Retrieves only file name from the given url.
    /// e.g. get 'test.zip' from 'http://www.yoursite.com/files/test.zip'
    /// </summary>
    public static string GetFileName(string url)
    {
        string file = string.Empty;
        Uri u = new Uri(url);
        file = Path.GetFileName(u.AbsolutePath);
        return file;
    }

}

# Unity-ZipDownload

## Overview

A sample which download a ZIP file from a remote web server with the given url and save it to the persistence folder then unzip all image files that were in zip file. 
After finishing unzip, it loads one of image file and assign it to a mesh renderer's main texture.

Note that the path manipulation done here is with [Application.persistentDataPath](http://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html), so it works all known platforms of Unity3D.

## References

* [SharpZipLib](http://icsharpcode.github.io/SharpZipLib/) is used to unzip the downloaded file.
* See [Mongoose](https://code.google.com/p/mongoose/) if you need to quickly setup a web-server.

## License

This code is distributed under the terms and conditions of the MIT license.


Copyright (c) 2013 Kim, Hyoun Woo

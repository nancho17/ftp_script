using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Cache;
using System;
using System.Net;
using System.IO;
using System.Collections.Generic;


namespace scripts
{
class Program
{

static string remoteUri = "ftp://firmware.ppstinc.net%257Cfirmware.admin@firmware.ppstinc.net/SW/Installers/dependencies/Installers/";

static void Main(string[] args)
{
	Console.WriteLine("Dependency downloader");
	//string fileName = "PPS-Logo.ico";

	SearchFTPfiles("");
	//DisplayFileFromServer(remoteUri, fileName);


}

public static bool SearchFTPfiles(String Addres)
{
	System.Console.Write("Searching: ");
	System.Console.WriteLine(remoteUri+Addres);

	FtpWebRequest request = (FtpWebRequest)WebRequest.Create(remoteUri+Addres);
	request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

	request.Credentials = new NetworkCredential ("firmware.ppstinc.net|firmware.admin","Kydb4yvhrYQnsK8");

	FtpWebResponse response = (FtpWebResponse)request.GetResponse();


	Stream responseStream = response.GetResponseStream();
	StreamReader reader = new StreamReader(responseStream);
	//Console.WriteLine(reader.ReadToEnd());
	String a = reader.ReadToEnd();
	Console.WriteLine($"Directory List Complete, status {response.StatusDescription}");

	List<string> dirList = new List<string>();

	string[] words = a.Split("\r\n",System.StringSplitOptions.RemoveEmptyEntries);

	foreach (var word in words)
	{

		string[] be = word.Split(' ',System.StringSplitOptions.RemoveEmptyEntries);
		//System.Console.WriteLine( be[be.Length -1]);
		if (be.Length> 4) {
			for (int i = 4; i < be.Length; i++)
			{
				be[3] +=" "+ be[i];
			}
			System.Console.WriteLine("is {0}", be[3]);
		}
		if(word.Contains("<DIR>"))
		{

			dirList.Add(be[3]);
		}
		else
		{
			//System.Console.WriteLine("{0}", be[3]);
			DisplayFileFromServer(Addres,be[3]);

		};

	}

	reader.Close();
	response.Close();

	foreach (String i in dirList)
	{
		//System.Console.WriteLine("{0}", i);
		SearchFTPfiles(Addres+i+"/");
	}

	return true;
}

public static bool DisplayFileFromServer(String Addres, String Files)
{
	string myStringWebResource = remoteUri+ Addres + Files;
	System.Console.WriteLine("{0}", myStringWebResource );

	Addres="dependencies/"+Addres;
	if (!Directory.Exists(Addres))
	{
		Directory.CreateDirectory(Addres);
	}
	if (Files==" ")
	{
		return false;
	}
	Files=Addres+Files;
	Uri serverUri = new Uri (myStringWebResource);
	// The serverUri parameter should start with the ftp:// scheme.
	if (serverUri.Scheme != Uri.UriSchemeFtp)
	{
		return false;
	}
	// Get the object used to communicate with the server.
	WebClient request = new WebClient();

	// This example assumes the FTP site uses anonymous logon.
	request.Credentials = new NetworkCredential ("firmware.ppstinc.net|firmware.admin","Kydb4yvhrYQnsK8");
	try
	{
		//byte [] newFileData = request.DownloadData (serverUri.ToString());
		Console.WriteLine("Downloading {0}", myStringWebResource);
		request.DownloadFile(myStringWebResource,Files);
		//string fileString = System.Text.Encoding.UTF8.GetString(newFileData);
		//Console.WriteLine(fileString);
	}
	catch (WebException e)
	{
		Console.WriteLine(e.ToString());
	}
	return true;
}







}
}



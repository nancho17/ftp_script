using System.Security.Cryptography.X509Certificates;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Cache;
using System;
using System.Net;
using System.IO;



namespace scripts
{
class Program
{
static void Main(string[] args)
{
	Console.WriteLine("Dependency downloader");
	string remoteUri = "ftp://firmware.ppstinc.net%257Cfirmware.admin@firmware.ppstinc.net/SW/Installers/dependencies/";
	string fileName = "PPS-Logo.ico";

	SearchFTPfiles(remoteUri, fileName);
	DisplayFileFromServer(remoteUri, fileName);


}

public static bool SearchFTPfiles(String Addres, String Files)
{
	FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Addres);
	request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

	request.Credentials = new NetworkCredential ("firmware.ppstinc.net|firmware.admin","Kydb4yvhrYQnsK8");

	FtpWebResponse response = (FtpWebResponse)request.GetResponse();


	Stream responseStream = response.GetResponseStream();
	StreamReader reader = new StreamReader(responseStream);
	//Console.WriteLine(reader.ReadToEnd());
	String a = reader.ReadToEnd();
	Console.WriteLine($"Directory List Complete, status {response.StatusDescription}");
	Console.WriteLine("the dirs: {0}",a.IndexOf("<DIR>"));

	string[] words = a.Split('\n');

	foreach (var word in words)
	{
		System.Console.WriteLine($"<{word}>");
	}


	string[] be = words[0].Split(' ',System.StringSplitOptions.RemoveEmptyEntries);
	Console.WriteLine($"Dir {0}",1);
	foreach (var i in be)
	{
		System.Console.WriteLine(i);

	}

	System.Console.WriteLine(be[be.Length -1]);



	reader.Close();
	response.Close();
	return true;
}

public static bool DisplayFileFromServer(String Addres, String Files)
{
	string myStringWebResource = Addres + Files;
	if (!Directory.Exists("dependencies"))
	{
		Directory.CreateDirectory("dependencies");
	}
	Files="dependencies/"+Files;
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



using System;
using System.Net;




namespace scripts
{
class Program
{
static void Main(string[] args)
{
	Console.WriteLine("Hello World!");
	string remoteUri = "ftp://firmware.ppstinc.net%257Cfirmware.admin@firmware.ppstinc.net/SW/Installers/dependencies/";
	string fileName = "PPS-Logo.ico", myStringWebResource = null;
	myStringWebResource = remoteUri + fileName;
	Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n", fileName, myStringWebResource);
    Uri target = new Uri (myStringWebResource);
	DisplayFileFromServer(target);
	//Console.WriteLine("Successfully Downloaded File \"{0}\" from \"{1}\"", fileName, myStringWebResource);
}

public static bool DisplayFileFromServer(Uri serverUri)
{
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
		byte [] newFileData = request.DownloadData (serverUri.ToString());
		string fileString = System.Text.Encoding.UTF8.GetString(newFileData);
		Console.WriteLine(fileString);
	}
	catch (WebException e)
	{
		Console.WriteLine(e.ToString());
	}
	return true;
}

}





}



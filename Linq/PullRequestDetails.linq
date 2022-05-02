<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	string html = string.Empty;
	string url = @"https://dev.azure.com/exxat-team/Framework/_apis/git/repositories?api-version=6.0";
	var accessToken = "m5kwtdqjsii6lnzmx4g75mrcep32cxd2nnorxzvsk7g25fy2lwdq";
	var projects = new[] { "Framework" };
	var base64AuthInfo = string.Format("Basic:{0}",System.Convert.ToBase64String(Encoding.ASCII.GetBytes(accessToken)));
		
	var request = (HttpWebRequest)WebRequest.Create(new Uri(url));
	request.UseDefaultCredentials = true;
	request.PreAuthenticate = true;
	request.Credentials = CredentialCache.DefaultCredentials;
	request.Method = "GET";
	request.Headers.Add(HttpRequestHeader.Authorization, base64AuthInfo);
	request.Headers.Add(HttpRequestHeader.ContentType, "application/json");
	
	using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
	using (Stream stream = response.GetResponseStream())
	using (StreamReader reader = new StreamReader(stream))
	{
		html = reader.ReadToEnd();
	}

	Console.WriteLine(html);
}

// You can define other methods, fields, classes and namespaces here
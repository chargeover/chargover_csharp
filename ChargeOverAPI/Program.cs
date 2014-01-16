using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.IO;

using ChargeOverAPI;

namespace ChargeOverAPI
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Example1 ();
			Example2 ();

			#if DEBUG
			Console.WriteLine("Press enter to close...");
			Console.ReadLine();
			#endif
		}

		public static void Example1()
		{
			// Here's an example using the ChargeOverAPI class I put together
			Console.WriteLine ("EXAMPLE #1");

			string endpoint = "http://dev.chargeover.com/signup/api/v3.php";
			string username = "7sutWFEO2zKVYIGmZMJ3Nij5hfLxDRb8";
			string password = "9vCJbmdZKSieVchyrRItFQw8MBN4lOH3";

			ChargeOverAPI api = new ChargeOverAPI (endpoint, username, password);

			Customer cust = new Customer ();
			cust.company = "Test C# Company";
			cust.bill_addr1 = "72 E Blue Grass Road";
			cust.bill_city = "Mt Pleasant";
			cust.bill_state = "MI";
			cust.bill_postcode = "48858";

			Response resp = api.create (cust);

			Console.WriteLine ("Status back was: " + resp.status + ", new customer id is " + resp.id);
			Console.Write ("\n\n\n");
		}

		public static void Example2()
		{
			// Here's a bit of a more "raw" example 
			Console.WriteLine ("EXAMPLE #2");

			string endpoint = "http://dev.chargeover.com/signup/api/v3.php";
			string username = "7sutWFEO2zKVYIGmZMJ3Nij5hfLxDRb8";
			string password = "9vCJbmdZKSieVchyrRItFQw8MBN4lOH3";

			string data = @"{
							    ""company"": ""Test Company Name"",
							    ""bill_addr1"": ""16 Dog Lane"",
							    ""bill_addr2"": ""Suite D"",
							    ""bill_city"": ""Storrs"",
							    ""bill_state"": ""CT"",
							    ""email"": ""keith@chargeover.com""
							}";

			string httpMethod = "POST";

			HttpWebRequest request = (HttpWebRequest) WebRequest.Create(endpoint + "/customer"); 

			request.KeepAlive = false;
			request.ProtocolVersion = HttpVersion.Version10;
			request.Method = httpMethod;

			byte[] postBytes = Encoding.ASCII.GetBytes(data);

			request.ContentType = "application/json";
			request.ContentLength = postBytes.Length;

			request.Credentials = new NetworkCredential(username, password);

			Stream requestStream = request.GetRequestStream();

			requestStream.Write(postBytes, 0, postBytes.Length);
			requestStream.Close();

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			string httpResponse = new StreamReader(response.GetResponseStream()).ReadToEnd();

			Console.WriteLine ("Response from ChargeOver API: " + httpResponse);
			Console.Write ("\n\n\n");
		}
	}
}
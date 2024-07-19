﻿using ItSoftware.Core.Extensions;
using ItSoftware.Core.ID;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using ItSoftware.Core.Log;
using System.Globalization;
//using ItSoftware.Core.HttpHost;

namespace ItSoftware.Core.TestApplication
{
	internal class Tests
	{
		private Stopwatch m_swatch = new Stopwatch();

		internal void RunTests()
		{
			Console.WriteLine("> Test Application - Started <");

			TestItsStopwatchStart();

			try
			{
				this.TestItsRegularExpressions();
				this.TestItsLog();
				this.TestItsHash();
				this.TestItsDataSizeString();
				this.TestItsWidthExpand();
				this.TestItsDbClient();
				this.TestItsID();
				this.TestItsRenderTimeSpan();
				this.TestItsRenderException();
				this.TestItsHttpHost();
				this.TestItsRandom();
				this.TestItsToWords();
				this.TestItsToNumbers();
                this.TestItsToDouble();
				this.TestItsToHexNumbers();
            }
			catch (System.Exception y)
			{
				Console.WriteLine(y.ItsRenderException());
			}
			finally
			{
				TestItsStopwatchStop();
			}

			Console.WriteLine("> Test Application - Finished <");
		}

		private void PrintTestHeader(string name)
		{
			Console.WriteLine();
			Console.WriteLine($" {name} ".ItsWidthExpand(80, '_', ItsWidthExpandDirection.Middle));
		}

		private void TestItsStopwatchStart()
		{
			PrintTestHeader("ItsStopwatch Started");
			this.m_swatch.Start();
		}

		private void TestItsStopwatchStop()
		{
			PrintTestHeader("ItsStopwatch Stopped");

			this.m_swatch.Stop();

			Console.WriteLine($"> Elapsed time: {new TimeSpan(0, 0, 0, 0, (int)this.m_swatch.ItsGetStopWatchMilliSeconds()).ItsRenderTimeSpan(true)}");
			Console.WriteLine();
		}

        private void TestItsToWords()
        {
            PrintTestHeader("ItsToWords Started");
            foreach (var word in System.IO.File.ReadLines("poem.txt").ItsToWords(false))
            {
                Console.WriteLine($"'{word}'");
            }

            Console.WriteLine($"Count All: {System.IO.File.ReadLines("poem.txt").ItsToWords(false).Count()}");
            Console.WriteLine($"Count Distinct: {System.IO.File.ReadLines("poem.txt").ItsToWords(true).Count()}");
        }

        private void TestItsToNumbers()
        {
            PrintTestHeader("ItsToNumbers Started");
            var lines = "ABC 0130 DEF 2010. \n2000 1920.20191 XYZ!";
            foreach (var n in lines.Split('\n').AsEnumerable<string>().ItsToNumbers(false))
            {
                Console.WriteLine($"'{n}'");
            }
        }

        private void TestItsToDouble()
        {
            PrintTestHeader("ItsToDouble Started");
            var lines = "ABC 0130, DEF 2010.\n2000 1920.20191 0130 XYZ!";
            foreach (var n in lines.Split('\n').AsEnumerable<string>().ItsToNumbers(false).ItsToDouble(false, new System.Globalization.CultureInfo("en-US")))
            {
                Console.WriteLine(n.ToString(new CultureInfo("en-US")));
            }
        }

        private void TestItsToHexNumbers()
        {
            PrintTestHeader("ItsToHexNumbers Started");
            var lines = "ABC 0130, DEF 2010.\n\n2000 1920.20191 0130 XYZ! af4b af5x";
            foreach (var n in lines.Split('\n').AsEnumerable<string>().ItsToHexNumbers(false))
            {
                try
                {
                    Console.WriteLine($"'{n}'\t{Convert.ToInt32(n, 16)}");
                }
                catch (System.Exception e)
                {
                    Console.WriteLine($"'{n}'\tERROR: {e.GetType().FullName}, {e.Message}");
                }
            }
        }

        private void TestItsRegularExpressions()
		{
			PrintTestHeader("ItsRegularExpressions");

			Console.WriteLine(@"a\r\naaKJETIL KRISTOFFER SOLBERGbbba\r\naaYES MANbbb"".ItsRegExPatternMatches(@""a(\s*)aa(?<bingo>[\w ]+)bbb"")");
			var match2 = "a\r\naaKJETIL KRISTOFFER SOLBERGbbba\r\naaYES MANbbb".ItsRegExPatternMatches(@"a(\s*)aa(?<bingo>[\w ]+)bbb");
			foreach (Match s in match2)
			{
				Console.WriteLine($"> {s.Groups["bingo"].Value}");
			}
			Console.WriteLine();

			Console.WriteLine(@"a\r\naaKJETIL KRISTOFFER SOLBERGbbba\r\naaYES MANbbb"".ItsRegExPatternMatchesGroupAsArray(""bingo"", @""a(\s *)aa(?< bingo >[\w] +)bbb"");");
			var match3 = "a\r\naaKJETIL KRISTOFFER SOLBERGbbba\r\naaYES MANbbb".ItsRegExPatternMatchesGroupAsArray("bingo", @"a(\s*)aa(?<bingo>[\w ]+)bbb");
			foreach (var s in match3)
			{
				Console.WriteLine($"> {s}");
			}

			Console.WriteLine();
		}

		private void TestItsLog()
		{
			PrintTestHeader("ItsLog");

			ItsLog log = new ItsLog("D:\\ItsLog.xml", "ItSoftware.NDN.Core", false);
			log.LogInformation("Information Title", "Information text");
			log.LogWarning("Warning Title", "Warning text");
			log.LogError("Error Title", "Error text");
			log.LogDebug("Debug Title", "Debug text", "Some detail here");
			log.LogOther("Other Title", "Other text");

			Console.WriteLine(log.ToString());

			Console.WriteLine();
		}

		private void TestItsHash()
		{
			PrintTestHeader("ItsHash");

			Console.WriteLine("'kjetil' hashed MD5");
			Console.WriteLine($"> {"kjetil".ItsHashMD5(Encoding.ASCII)}");
			Console.WriteLine("'kjetil' hashed SHA1");
			Console.WriteLine($"> {"kjetil".ItsHashSHA1(Encoding.ASCII)}");
			Console.WriteLine("'kjetil' hashed SHA256");
			Console.WriteLine($"> {"kjetil".ItsHashSHA256(Encoding.ASCII)}");
			Console.WriteLine("'kjetil' hashed SHA384");
			Console.WriteLine($"> {"kjetil".ItsHashSHA384(Encoding.ASCII)}");
			Console.WriteLine("'kjetil' hashed SHA512");
			Console.WriteLine($"> {"kjetil".ItsHashSHA512(Encoding.ASCII)}");

			Console.WriteLine();
		}

		private void TestItsDataSizeString()
		{
			PrintTestHeader("ItsDataSizeString");

			Console.WriteLine("1_000_000_000");
			Console.WriteLine("> " + 1_000_000_000.ItsToDataSizeString(2, new System.Globalization.CultureInfo("en-US")));
			Console.WriteLine("int.MaxValue");
			Console.WriteLine("> " + int.MaxValue.ItsToDataSizeString(2, new System.Globalization.CultureInfo("en-US")));
			Console.WriteLine("uint.MaxValue");
			Console.WriteLine("> " + uint.MaxValue.ItsToDataSizeString(2, new System.Globalization.CultureInfo("en-US")));
			Console.WriteLine("long.MaxValue");
			Console.WriteLine("> " + long.MaxValue.ItsToDataSizeString(2, new System.Globalization.CultureInfo("en-US")));
			Console.WriteLine("ulong.MaxValue");
			Console.WriteLine("> " + ulong.MaxValue.ItsToDataSizeString(2, new System.Globalization.CultureInfo("en-US")));
			Console.WriteLine("decimal.MaxValue");
			Console.WriteLine("> " + decimal.MaxValue.ItsToDataSizeString(2, new System.Globalization.CultureInfo("en-US")));

			Console.WriteLine();
		}

		private void TestItsWidthExpand()
		{
			PrintTestHeader("ItsWidthExpand");

			string target = "Kjetil";
			Console.WriteLine(target.ItsWidthExpand(30, '_', ItsWidthExpandDirection.Left));
			Console.WriteLine(target.ItsWidthExpand(30, '_', ItsWidthExpandDirection.Middle));
			Console.WriteLine(target.ItsWidthExpand(30, '_', ItsWidthExpandDirection.Right));

			Console.WriteLine();
		}

		private void TestItsDbClient()
		{
			PrintTestHeader("ItsDbClient");
			//using (ItsDbClient dbClient = new ItsDbClient("data source=(local);initial catalog=birken;integrated security=True;MultipleActiveResultSets=True;", true))//ItsDbClient.DefaultConnectionString, true))
			/*using (ItsDbClient dbClient = new ItsDbClient(ItsDbClient.ActiveConnectionString, true))
			{
				SqlDataReader sdr = dbClient.ExecuteQuery("SELECT [navn], [kjønn], [alder] FROM [deltaker] ORDER BY [navn] ASC");
				while (sdr.Read())
				{
					Console.WriteLine($"{sdr[0].ToString().ItsWidthExpand(30, ' ', ItsWidthExpandDirection.Right)}\t{sdr[1]}\t{sdr[2]} år");
				}
				object count = dbClient.ExecuteScalar("SELECT COUNT(*) FROM [deltaker]");
				Console.WriteLine($"Total number of rows: {count.ToString()}");
			}*/

			Console.WriteLine();
		}

		private void TestItsID()
		{
			PrintTestHeader("ItsID");

			Console.WriteLine($"ItsID.ItsCreateID(12, ItsCreateIDOptions.LowerAndUpperCase, false)");
			Console.WriteLine($"> {ItsID.ItsCreateID(12, ItsCreateIDOptions.LowerAndUpperCase, false)}");
			Console.WriteLine($"ItsID.ItsCreateID(12, ItsCreateIDOptions.LowerAndUpperCase, true)");
			Console.WriteLine($"> {ItsID.ItsCreateID(12, ItsCreateIDOptions.LowerAndUpperCase, true)}");
			Console.WriteLine($"ItsID.ItsCreateID(12, ItsCreateIDOptions.UpperCase, false)");
			Console.WriteLine($"> {ItsID.ItsCreateID(12, ItsCreateIDOptions.UpperCase, false)}");
			Console.WriteLine($"ItsID.ItsCreateID(12, ItsCreateIDOptions.UpperCase, true)");
			Console.WriteLine($"> {ItsID.ItsCreateID(12, ItsCreateIDOptions.UpperCase, true)}");
			Console.WriteLine($"ItsID.ItsCreateID(12, ItsCreateIDOptions.LowerCase, false)");
			Console.WriteLine($"> {ItsID.ItsCreateID(12, ItsCreateIDOptions.LowerCase, false)}");
			Console.WriteLine($"ItsID.ItsCreateID(12, ItsCreateIDOptions.LowerCase, true)");
			Console.WriteLine($"> {ItsID.ItsCreateID(12, ItsCreateIDOptions.LowerCase, true)}");
			Console.WriteLine($"ItsID.ItsCreateID(64, ItsCreateIDOptions.LowerAndUpperCase, false)");
			Console.WriteLine($"> {ItsID.ItsCreateID(64, ItsCreateIDOptions.LowerAndUpperCase, false)}");
			Console.WriteLine($"ItsID.ItsCreateID(64, ItsCreateIDOptions.LowerAndUpperCase, true)");
			Console.WriteLine($"> {ItsID.ItsCreateID(64, ItsCreateIDOptions.LowerAndUpperCase, true)}");

			Console.WriteLine();
		}

		private void TestItsRenderTimeSpan()
		{
			PrintTestHeader("ItsRenderTimeSpan");

			Console.WriteLine("TimeSpan.FromSeconds(487_965_892);");
			TimeSpan ts = TimeSpan.FromSeconds(487_965_892);
			Console.WriteLine($"> {ts.ItsRenderTimeSpan(false)}");

			Console.WriteLine();
		}

		private void TestItsRenderException()
		{
			PrintTestHeader("ItsRenderException");

			var x = new ArgumentException("yes", new NullReferenceException());
			x.Data.Add("StringKey", "StringValue");
			x.Data.Add("TestNULL", null);
			x.Data.Add(544, 545);
			x.Data.Add(new object(), 1001);
			Console.WriteLine(x.ItsRenderException());

			Console.WriteLine();
		}

		private void TestItsHttpHost()
		{
			PrintTestHeader("ItsHttpHost");

			/*using (ItsHttpHost host = new ItsHttpHost(5454))
			{
				host.Start(new List<ItsMiddleware> { new Middleware1(), new Middleware2() });
				try
				{
					var p = new Process();
					p.StartInfo.UseShellExecute = true;
					p.StartInfo.FileName = "http://localhost:5454";
					p.Start();
				}
				catch (Exception x)
				{
					Console.WriteLine(x.Message);
				}
				Console.WriteLine("Server Ready at http://localhost:5454");
				System.Threading.Thread.Sleep(1500);
			}*/

			Console.WriteLine();
		}

		private void TestItsRandom()
        {
			PrintTestHeader("ItsRandom");

			var list = new List<string>() { "Anne", "Beta", "Bob", "Charlie", "Isabel", "John" };

			for (int i = 0; i < 10; i++)
			{
				Console.WriteLine(list.ItsRandom());
			}
		}
	}
}

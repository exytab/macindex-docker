using MacIndex.Web.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace MacIndex.Web.Controllers
{
	public class HomeController : Controller
	{
		public async Task<IActionResult> Index()
		{
			Price price = new Price();
			price.MacPro = await getPrice("http://stylus.com.ua/ru/products/apple_mac_pro/sortpro/3/up/2/index.html");
			price.MacBookPro = await getPrice("http://stylus.com.ua/ru/products/apple_macbook/sortpro/3/up/2/index.html");

      //todo: di: https://weblogs.asp.net/scottgu/introducing-asp-net-5
      //todo: simple redis: https://github.com/migueldeicaza/redis-sharp

			return View(price);
		}
		public IActionResult Error()
		{
			return View("~/Views/Shared/Error.cshtml");
		}

		private async Task<string> getPrice(string url)
		{
			string price1 = null;

			using (var httpClient = new HttpClient())
			{

				//myWebClient.Headers["User-Agent"] = "MOZILLA/5.0 (WINDOWS NT 6.1; WOW64) APPLEWEBKIT/537.1 (KHTML, LIKE GECKO) CHROME/21.0.1180.75 SAFARI/537.1";
				//myWebClient.Encoding = System.Text.Encoding.;

				string page = await httpClient.GetStringAsync(url);

				//HtmlDocument doc = new HtmlDocument();
				//doc.LoadHtml(page);

				//todo:
				//string inputData = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'price') and contains(@class, 'pricevisible')]").InnerText;

				//redis: http://piotrwalat.net/using-redis-with-asp-net-web-api/

				//            string pattern = @"^\d+";
				//RegexOptions regexOptions = RegexOptions.None;
				//Regex regex = new Regex(pattern, regexOptions);
				//foreach (Match match in regex.Matches(inputData))
				//{
				//	if (match.Success)
				//	{
				//		price1 = match.Value;
				//		//TODO processing results
				//	}
				//}

				price1 = parseValue(page);
			}

			return price1;
		}

		private string parseValue(string text)
		{
			string pattern = @"<span class=""price pricevisible""><strong>([0-9]*)";
			RegexOptions regexOptions = RegexOptions.None;
			Regex regex = new Regex(pattern, regexOptions);
			string inputData = text;
			foreach (Match match in regex.Matches(inputData))
			{
				if (match.Success)
				{
					return match.Groups[1].Value;
					//TODO processing results
				}
			}

			return null;
		}
	}
}
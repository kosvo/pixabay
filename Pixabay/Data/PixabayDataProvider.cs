using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pixabay
{
	
	public class PixabayDataProvider : IImageDataProvider
	{
		private readonly string key = "3226416-4f806f783d04d88ca5c0bca86";
		private readonly string type = "photo";
		private readonly string domain = "https://pixabay.com";
		public async Task<List<string>> FindImagesAsync(string query)
		{
			var encodedQuery = WebUtility.UrlEncode(query);
			var stream = await new WebClient().OpenReadTaskAsync($"{domain}/api/?key={key}&q={encodedQuery}&image_type={type}");
			var reader = new StreamReader(stream);
			var result = JsonConvert.DeserializeObject<PixabayResponse>(await reader.ReadToEndAsync());
			return result?.hits?.Select(d => d.userImageURL).ToList();
		}
	}
}

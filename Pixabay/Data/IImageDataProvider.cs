using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pixabay
{
		
	public interface IImageDataProvider
	{
		Task<List<string>> FindImagesAsync(string query);
	}
	
}

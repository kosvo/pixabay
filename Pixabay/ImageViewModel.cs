using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace Pixabay
{
	public class ImageViewModel:ViewModelBase
	{
		private readonly IImageDataProvider _imageProvider;
		string searchQuery;

		public string SearchQuery
		{
			get
			{
				return searchQuery;
			}

			set
			{
				if (searchQuery != value)
				{
					searchQuery = value;
					RaisePropertyChanged(() => SearchQuery);
				}
			}
		}
		public List<string> Images { get; set; } = new List<string>();
		public ImageViewModel(IImageDataProvider imageProvider)
		{
			_imageProvider = imageProvider;
			PropertyChanged += async (s, e) =>
			{
				if (e.PropertyName == "SearchQuery")
				{
					Images = await _imageProvider.FindImagesAsync(searchQuery);
					RaisePropertyChanged(() => Images);
				}
			};
		}
	}
}


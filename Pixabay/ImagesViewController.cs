using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pixabay
{
    public partial class ImagesViewController : UIViewController
    {
		class ImagesDataSource : UICollectionViewDataSource
		{
			public List<string> Images { get; set; }
			public ImagesDataSource(List<string> data)
			{
				Images = data;
			}
			public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
			{
				var cell = (ImageViewCell)collectionView.DequeueReusableCell(ImageViewCell.Key, indexPath);

				var image = Images[indexPath.Row];

				Task.Run(async () => { 
					 cell.Image = await FromUrl(image);
				});


				return cell;
				//return 9;
			}

			public override nint GetItemsCount(UICollectionView collectionView, nint section)
			{
				return Images?.Count ?? 0;
			}

			public static async Task<UIImage> FromUrl(string uri)
			{
				return await Task.Run(() =>
			   {
				   try
				   {
					   using (var url = new NSUrl(uri))
					   using (var data = NSData.FromUrl(url))
						   return Task.FromResult(UIImage.LoadFromData(data));
				   }
				   catch
				   {
					   return Task.FromResult(new UIImage());
				   }
			   });
			}
		}
        public ImagesViewController (IntPtr handle) : base (handle)
        {
        }
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			ImagesCollectionView.RegisterClassForCell(typeof(ImageViewCell), ImageViewCell.Key);
			ImagesCollectionView.DataSource = new ImagesDataSource(new List<string> { "https://media.giphy.com/media/43h9ZbsC6otlm/giphy.gif" });
			ImagesCollectionView.ReloadData();                                                      
		}
    }
}
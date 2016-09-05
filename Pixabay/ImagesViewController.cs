using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreGraphics;
using GalaSoft.MvvmLight.Ioc;

namespace Pixabay
{
	public class CustomImageFlowLayout : UICollectionViewFlowLayout

	{
		public CustomImageFlowLayout()
		{
			SetupLayout();
		}
		public override CGSize ItemSize
		{
			set
			{

			}
			get
			{
				float numberOfColumns = 3;

				var itemWidth = (this.CollectionView.Frame.Width - (numberOfColumns - 1)) / numberOfColumns;
				return new CGSize(itemWidth, itemWidth);

			}
		}
		private void SetupLayout()
		{
			MinimumInteritemSpacing = 1;

			MinimumLineSpacing = 1;

			ScrollDirection = UICollectionViewScrollDirection.Vertical;
		}

	}
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
				var cell = (ImageCell)collectionView.DequeueReusableCell("ImageCell", indexPath);

				//var image = Images[indexPath.Row];

				cell.Image = UIImage.FromFile("img.png");
			

				return cell;
				//return 9;
			}

			public override nint GetItemsCount(UICollectionView collectionView, nint section)
			{
				return 9;//Images?.Count ?? 0;
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
		public ImageViewModel ViewModel => SimpleIoc.Default.GetInstance<ImageViewModel>();
        public ImagesViewController (IntPtr handle) : base (handle)
        {
        }
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			ImagesCollectionView.CollectionViewLayout = new CustomImageFlowLayout();
			ImagesCollectionView.DataSource = new ImagesDataSource(new List<string> { "https://media.giphy.com/media/43h9ZbsC6otlm/giphy.gif" });
			ImagesCollectionView.ReloadData();                                                      
		}
    }
}
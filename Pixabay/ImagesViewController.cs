using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreGraphics;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Helpers;

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

				var image = Images[indexPath.Row];

				try
				{
					using (var url = new NSUrl(image))
					using (var data = NSData.FromUrl(url))
						cell.Image = UIImage.LoadFromData(data);
				}
				catch
				{
				//	return Task.FromResult(new UIImage());
				}

				//cell.Image = UIImage.FromFile("img.png");
			

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
		public ImageViewModel ViewModel => SimpleIoc.Default.GetInstance<ImageViewModel>();
		private List<Binding> bindings = new List<Binding>();

		public ImagesViewController (IntPtr handle) : base (handle)
        {
        }
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			bindings.Add(searchBar.SetBinding(() => searchBar.Text)
					 .ObserveSourceEvent<UISearchBarTextChangedEventArgs>("TextChanged")
						 .WhenSourceChanges(() =>
						 {
							 ViewModel.SearchQuery = searchBar.Text;
						 }));
			bindings.Add(this.SetBinding(() => ViewModel.Images).WhenSourceChanges(() =>
		   {

			   //(ImagesCollectionView.DataSource as ImagesDataSource).Images = ViewModel.Images;
			   if (ImagesCollectionView.DataSource is ImagesDataSource)
			   {
				   (ImagesCollectionView.DataSource as ImagesDataSource).Images = ViewModel.Images;
			   }
				ImagesCollectionView.ReloadData();

		   }));

			ImagesCollectionView.CollectionViewLayout = new CustomImageFlowLayout();
			ImagesCollectionView.DataSource = new ImagesDataSource(ViewModel.Images);
			ImagesCollectionView.ReloadData();                                                      
		}
    }
}
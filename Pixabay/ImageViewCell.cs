using System;

using Foundation;
using UIKit;

namespace Pixabay
{
	public partial class ImageViewCell : UICollectionViewCell
	{
		public static readonly NSString Key = new NSString("ImageViewCell");
		public static readonly UINib Nib;

		static ImageViewCell()
		{
			Nib = UINib.FromName("ImageViewCell", NSBundle.MainBundle);
		}

		public UIImage Image { set
			{
				imageView.Image = value;
			}}
		protected ImageViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
	}
}

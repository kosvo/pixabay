using Foundation;
using System;
using UIKit;

namespace Pixabay
{
    public partial class ImageCell : UICollectionViewCell
    {
        public ImageCell (IntPtr handle) : base (handle)
        {
        }
		public UIImage Image
		{
			set
			{
				imageView.Image = value;
			}
		}
    }
}
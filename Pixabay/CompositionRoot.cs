using System;
using GalaSoft.MvvmLight.Ioc;
namespace Pixabay
{
	public class CompositionRoot
	{
		public CompositionRoot()
		{
			SimpleIoc.Default.Register<IImageDataProvider, PixabayDataProvider>();
			SimpleIoc.Default.Register<ImageViewModel>();
		}
	}
}


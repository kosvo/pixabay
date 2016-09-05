// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Pixabay
{
    [Register ("ImagesViewController")]
    partial class ImagesViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISearchBar searchBar { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (searchBar != null) {
                searchBar.Dispose ();
                searchBar = null;
            }
        }
    }
}
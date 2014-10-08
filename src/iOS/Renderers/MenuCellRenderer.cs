using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CouchbaseConnect2014.iOS.Renderers;
using MonoTouch.UIKit;
using CouchbaseConnect2014.Controls;

[assembly: ExportRenderer (typeof(MenuCell), typeof(MenuCellRenderer))]

namespace CouchbaseConnect2014.iOS.Renderers
{
    public class MenuCellRenderer : ViewCellRenderer
    {
        UIView _selectedBackgroundView;

        public override UITableViewCell GetCell (Cell item, UITableView tv)
        {
            tv.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            var cell = base.GetCell (item, tv);

            if (_selectedBackgroundView == null) {
                _selectedBackgroundView = new UIView(cell.SelectedBackgroundView.Bounds);

                _selectedBackgroundView.Layer.BackgroundColor = 
                    Colors.MenuBackground.ToCGColor ();
            }

            cell.SelectedBackgroundView = _selectedBackgroundView;
            return cell;
        }
    }
}


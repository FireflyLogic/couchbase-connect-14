using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CouchbaseConnect2014.Views;
using CouchbaseConnect2014.iOS.Renderers;
using MonoTouch.UIKit;
using CouchbaseConnect2014.Controls;

[assembly: ExportRenderer (typeof(ContactCell), typeof(NoSelectViewCellRenderer))]
[assembly: ExportRenderer (typeof(TwitterCell), typeof(NoSelectViewCellRenderer))]

namespace CouchbaseConnect2014.iOS.Renderers
{
    public class NoSelectViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell (Cell item, UITableView tv)
        {
            var cell = base.GetCell (item, tv);
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            return cell;
        }
    }
}


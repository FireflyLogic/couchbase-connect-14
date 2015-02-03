using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CouchbaseConnect2014.Views;
using CouchbaseConnect2014.iOS.Renderers;
using UIKit;
using CouchbaseConnect2014.Controls;

[assembly: ExportRenderer (typeof(ContactCell), typeof(NoSelectViewCellRenderer))]
[assembly: ExportRenderer (typeof(TwitterCell), typeof(NoSelectViewCellRenderer))]

namespace CouchbaseConnect2014.iOS.Renderers
{
    public class NoSelectViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell (Cell item, UITableViewCell reusableItem, UITableView tv)
        {
            var cell = base.GetCell (item, reusableItem, tv);
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            return cell;
        }
    }
}


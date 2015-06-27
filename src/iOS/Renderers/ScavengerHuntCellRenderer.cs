using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CouchbaseConnect2014;
using CouchbaseConnect2014.iOS.Renderers;

[assembly: ExportRenderer (typeof(ScavengerHuntCell), typeof(ScavengerHuntCellRenderer))]

namespace CouchbaseConnect2014.iOS.Renderers
{
	public class ScavengerHuntCellRenderer : ViewCellRenderer
	{
        public override UITableViewCell GetCell (Cell item, UITableViewCell reusableItem, UITableView tv)
		{
			tv.SeparatorStyle = UITableViewCellSeparatorStyle.None;

            var cell = base.GetCell (item, reusableItem, tv);
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;

			return cell;
		}
	}
}


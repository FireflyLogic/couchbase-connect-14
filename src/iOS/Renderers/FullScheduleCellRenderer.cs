using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CouchbaseConnect2014.Views;
using CouchbaseConnect2014.iOS.Renderers;
using UIKit;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.ValueConverters;
using System.Globalization;
using CoreGraphics;
using System.ComponentModel;

[assembly: ExportRenderer (typeof(FullScheduleCell), typeof(FullScheduleCellRenderer))]

namespace CouchbaseConnect2014.iOS.Renderers
{
    public class FullScheduleCellRenderer : CellRenderer
    {
        const int Padding = 10;
        const int TitleWidth = 224;
        const int TitleHeight = 58;

        public override UITableViewCell GetCell (Cell item, UITableViewCell reusableItem, UITableView tv)
        {
            var viewModel = (FullScheduleCellViewModel)item.BindingContext;
            var cellTableViewCell = tv.DequeueReusableCell ("cell") as CellTableViewCell
                ?? new CellTableViewCell (UITableViewCellStyle.Default, "cell");
            cellTableViewCell.Cell = item;
            cellTableViewCell.SelectionStyle = UITableViewCellSelectionStyle.None;

            AddSubviews (viewModel, cellTableViewCell);

            this.UpdateBackground (cellTableViewCell, item);
            return cellTableViewCell;
        }

        void AddSubviews (FullScheduleCellViewModel viewModel, CellTableViewCell cellTableViewCell)
        {
            var textColor = ((Xamarin.Forms.Color)new TrackTextColorConverter ()
                .Convert (viewModel.Track, typeof(Xamarin.Forms.Color), 
                    null, CultureInfo.CurrentCulture))
                .ToUIColor ();

            cellTableViewCell.AddSubview (CreateBackgroundView (cellTableViewCell, viewModel));
            cellTableViewCell.AddSubview (CreateTitleLabel (viewModel, textColor));
            cellTableViewCell.AddSubview (CreateLocationLabel (viewModel, textColor));
            cellTableViewCell.AddSubview (CreateTrackLabel (viewModel, textColor));

            if (viewModel.IsOptional) cellTableViewCell.AddSubview (
                CreateSelectButton (viewModel, textColor, cellTableViewCell));
        }

        UIView CreateBackgroundView (CellTableViewCell cell, FullScheduleCellViewModel viewModel)
        {
            return new UIView {
                AutoresizingMask = UIViewAutoresizing.FlexibleHeight,
                Frame = cell.Bounds,
                BackgroundColor = ((Xamarin.Forms.Color)new TrackBackgroundColorConverter ()
                    .Convert (viewModel.Track, typeof(Xamarin.Forms.Color), 
                        null, CultureInfo.CurrentCulture))
                    .ToUIColor ()
                };
        }

        UIView CreateTitleLabel (FullScheduleCellViewModel viewModel, UIColor textColor)
        {
            var label = new UILabel {
                Text = viewModel.Title,
                TextColor = textColor,
                Frame = new CGRect {
                    X = Padding,
                    Y = Padding,
                    Width = TitleWidth,
                    Height = TitleHeight
                },
                Font = UIFont.FromName (Fonts.OpenSansLight, 14),
                UserInteractionEnabled = false,
                LineBreakMode = UILineBreakMode.WordWrap,
                Lines = 0
            };
            return label;
        }

        UIView CreateLocationLabel (FullScheduleCellViewModel viewModel, UIColor textColor)
        {
            var label = new UILabel {
                Text = viewModel.Location.ToUpper (),
                TextColor = textColor,
                Frame = new CGRect {
                    X = Padding,
                    Y = Padding + TitleHeight,
                    Width = TitleWidth / 2,
                    Height = 20
                },
                Font = UIFont.FromName (Fonts.OpenSansBold, 12)
            };

            return label;
        }

        UIView CreateTrackLabel (FullScheduleCellViewModel viewModel, UIColor textColor)
        {
            var label = new UILabel {
				Text = new TrackValueConverter().Convert(viewModel.Track, typeof(string), null, CultureInfo.CurrentCulture) as string,
                TextColor = textColor,
                Frame = new CGRect {
                    X = Padding + TitleWidth / 2,
                    Y = Padding + TitleHeight,
                    Width = TitleWidth / 2,
                    Height = 20
                },
                Font = UIFont.FromName (Fonts.OpenSansLight, 12)
            };

            return label;
        }

        UIView CreateSelectButton (
            FullScheduleCellViewModel viewModel, 
            UIColor textColor, 
            UITableViewCell cell)
        {
            const int ButtonWidth = 50;
            var button = new UIButton {
                Frame = new CGRect {
                    X = cell.Bounds.Width - ButtonWidth,
                    Y = 0,
                    Height = cell.Bounds.Height,
                    Width = ButtonWidth
                },
                AutoresizingMask = UIViewAutoresizing.FlexibleHeight,
                TintColor = textColor,
                Font = UIFont.FromName (Fonts.OpenSansLight, 22)
            };
            UpdateButtonTitle (button, viewModel);
            button.SetTitleColor (textColor, UIControlState.Normal);
            button.TouchUpInside += (object sender, EventArgs e) => {
                viewModel.ToggleSelection.Execute (null);
            };
            viewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) => {
                if (e.PropertyName == "IsSelected") {
                    UpdateButtonTitle (button, viewModel);
                }
            };
            return button;
        }

        static void UpdateButtonTitle (UIButton button, FullScheduleCellViewModel viewModel) {
            button.SetTitle (viewModel.IsSelected ? "✓" : "○", UIControlState.Normal);
        }
    }
}


using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CouchbaseConnect2014.Views;
using CouchbaseConnect2014.iOS.Renderers;
using MonoTouch.UIKit;
using CouchbaseConnect2014.ViewModels;
using System.Drawing;
using CouchbaseConnect2014.ValueConverters;
using System.Globalization;
using System.Reflection;

[assembly:ExportRenderer(typeof(AgendaCell), typeof(AgendaCellRenderer))]

namespace CouchbaseConnect2014.iOS.Renderers
{
    public class AgendaCellRenderer : CellRenderer
    {
        const int Padding = 10;
        const int TitleWidth = 224;
        const int TitleHeight = 58;
        const int TimeTop = TitleHeight - 6;
        const int TimeWidth = 64;
        const int PlusTop = 65;

        public override UITableViewCell GetCell (Cell item, UITableView tv)
        {
            var viewModel = (AgendaCellViewModel)item.BindingContext;
            var cellTableViewCell = tv.DequeueReusableCell ("cell") as CellTableViewCell
                ?? new CellTableViewCell (UITableViewCellStyle.Default, "cell");
            cellTableViewCell.Cell = item;
            cellTableViewCell.SelectionStyle = UITableViewCellSelectionStyle.None;

            var textColor = ((Xamarin.Forms.Color)new TrackTextColorConverter ()
                .Convert (viewModel.Track, typeof(Xamarin.Forms.Color), 
                    null, CultureInfo.CurrentCulture))
                .ToUIColor ();

            cellTableViewCell.AddSubview (CreateBackgroundView (cellTableViewCell.Bounds, viewModel));

            if (viewModel.IsBooked) {
                cellTableViewCell.AddSubview (CreateTitleLabel (viewModel, textColor));
                cellTableViewCell.AddSubview (CreateLocationLabel (viewModel, textColor));
                cellTableViewCell.AddSubview (CreateTrackLabel (viewModel, textColor));
            } else {
                cellTableViewCell.AddSubview (CreateChooseLabel ());
                cellTableViewCell.AddSubview (CreateAddIcon ());
            }

            cellTableViewCell.AddSubview (CreateTimeLabel (viewModel, textColor));
            cellTableViewCell.AddSubview (CreateAMPMLabel (viewModel, textColor));

            this.UpdateBackground (cellTableViewCell, item);
            return cellTableViewCell;
        }

        UIView CreateAddIcon ()
        {
            return new UIImageView {
                Frame = new RectangleF {
                    X = Padding,
                    Y = PlusTop,
                    Width = 20,
                    Height = 20
                },
                Image = UIImage.FromBundle ("plus.png")
            };
        }

        UIView CreateChooseLabel ()
        {
            return new UILabel {
                Frame = new RectangleF {
                    X = Padding + 30, 
                    Y = PlusTop,
                    Width = TitleWidth - 40,
                    Height = 20
                },
                Text = "Choose a Session",
                Font = UIFont.FromName (Fonts.OpenSansLight, 14),
                TextColor = UIColor.FromRGB (0x33, 0x33, 0x33)
            };
        }

        UIView CreateBackgroundView (RectangleF bounds, AgendaCellViewModel viewModel)
        {
            return new UIView {
                AutoresizingMask = UIViewAutoresizing.FlexibleHeight,
                Frame = bounds,
                BackgroundColor = ((Xamarin.Forms.Color)new TrackBackgroundColorConverter ()
                    .Convert (viewModel.Track, typeof(Xamarin.Forms.Color), null, CultureInfo.CurrentCulture))
                    .ToUIColor ()
            };
        }

        UIView CreateTitleLabel (AgendaCellViewModel viewModel, UIColor textColor)
        {
            var label = new UILabel {
                Text = viewModel.Title,
                TextColor = textColor,
                Frame = new RectangleF {
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

        UIView CreateLocationLabel (AgendaCellViewModel viewModel, UIColor textColor)
        {
            var label = new UILabel {
                Text = viewModel.Location.ToUpper (),
                TextColor = textColor,
                Frame = new RectangleF {
                    X = Padding,
                    Y = Padding + TitleHeight,
                    Width = TitleWidth / 2,
                    Height = 20
                },
                Font = UIFont.FromName (Fonts.OpenSansBold, 12)
            };

            return label;
        }

        UIView CreateTrackLabel (AgendaCellViewModel viewModel, UIColor textColor)
        {
            var label = new UILabel {
				Text = new TrackValueConverter().Convert(viewModel.Track, typeof(string), null, CultureInfo.CurrentCulture) as string,
                TextColor = textColor,
                Frame = new RectangleF {
                    X = Padding + TitleWidth / 2,
                    Y = Padding + TitleHeight,
                    Width = TitleWidth / 2,
                    Height = 20
                },
                Font = UIFont.FromName (Fonts.OpenSansLight, 12)
            };

            return label;
        }

        UIView CreateTimeLabel (AgendaCellViewModel viewModel, UIColor textColor)
        {
            var label = new UILabel {
                Text = viewModel.Time.ToString ("h:mm"),
                TextColor = textColor,
                Frame = new RectangleF {
                    X = Padding + TitleWidth,
                    Y = TimeTop,
                    Width = TimeWidth,
                    Height = 40
                },
                Font = UIFont.FromName (Fonts.OpenSansLight, 24),
                TextAlignment = UITextAlignment.Right
            };

            return label;
        }

        UIView CreateAMPMLabel (AgendaCellViewModel viewModel, UIColor textColor)
        {
            var label = new UILabel {
                Text = viewModel.Time.ToString ("tt"),
                TextColor = textColor,
                Frame = new RectangleF {
                    X = Padding + TitleWidth + TimeWidth,
                    Y = TimeTop + 16,
                    Width = 16,
                    Height = 20
                },
                Font = UIFont.FromName (Fonts.OpenSansBold, 9),
                TextAlignment = UITextAlignment.Right
            };

            return label;
        }
    }
}


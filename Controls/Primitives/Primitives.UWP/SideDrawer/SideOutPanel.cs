﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Telerik.UI.Xaml.Controls.Primitives
{
    /// <summary>
    /// A custom panel that holds the elements in a <see cref="RadSideDrawer"/> control.
    /// </summary>
    public class SideOutPanel : Panel
    {
        internal RadSideDrawer Owner { get; set; }

        public SideOutPanel()
        {
            this.Loaded += SideOutPanel_Loaded;
        }

        void SideOutPanel_Loaded(object sender, RoutedEventArgs e)
        {
            // Blind fix for bug in Release configuration after Denim update
            this.Owner.InvalidateMeasure();
        }
        /// <inheritdoc/>
        protected override Size MeasureOverride(Size availableSize)
        {
            double maxHeight = 0;
            double maxWidth = 0;
            foreach (UIElement child in this.Children)
            {
                child.Measure(new Size(availableSize.Width, availableSize.Height));

                maxHeight = Math.Max(child.DesiredSize.Height, maxHeight);
                maxWidth = Math.Max(child.DesiredSize.Width, maxWidth);
            }

            if (double.IsInfinity(availableSize.Height))
            {
                availableSize.Height = maxHeight;
            }
            if (double.IsInfinity(availableSize.Width))
            {
                availableSize.Width = maxWidth;
            }

            return availableSize;
        }

        /// <inheritdoc/>
        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement child in this.Children)
            {
                child.Arrange(new Rect(new Point(0, 0), finalSize));
            }

            return base.ArrangeOverride(finalSize);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.UI.Xaml.Controls.Primitives.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Telerik.UI.Xaml.Controls.Grid.Primitives
{
    [TemplatePart(Name = "PART_SelectCheckBox", Type = typeof(CheckBox))]
    public partial class DataGridFlyoutColumnHeader : DataGridFlyoutHeader
    {
        internal event EventHandler SelectionCheck;
        internal event EventHandler SelectionUncheck;

        private CheckBox selectCheckBox;

        public DataGridFlyoutColumnHeader()
        {
            this.DefaultStyleKey = typeof(DataGridFlyoutColumnHeader);
        }

        /// <summary>
        /// Raises the <see cref="SelectionCheck"/> event. Exposed for testing purposes, do not call elsewhere but in test projects.
        /// </summary>
        internal void RaiseSelectionCheck()
        {
            var eh = this.SelectionCheck;
            if (eh != null)
            {
                eh(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the <see cref="SelectionUncheck"/> event. Exposed for testing purposes, do not call elsewhere but in test projects.
        /// </summary>
        internal void RaiseSelectionUncheck()
        {
            var eh = this.SelectionUncheck;
            if (eh != null)
            {
                eh(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Called when the Framework <see cref="M:OnApplyTemplate" /> is called. Inheritors should override this method should they have some custom template-related logic.
        /// This is done to ensure that the <see cref="P:IsTemplateApplied" /> property is properly initialized.
        /// </summary>
        protected override bool ApplyTemplateCore()
        {
            bool applied = base.ApplyTemplateCore();

            this.selectCheckBox = this.GetTemplatePartField<CheckBox>("PART_SelectCheckBox");
            applied = applied && this.selectCheckBox != null;

            return applied;
        }

        /// <summary>
        /// Occurs when the <see cref="M:OnApplyTemplate" /> method has been called and the template is already successfully applied.
        /// </summary>
        protected override void OnTemplateApplied()
        {
            base.OnTemplateApplied();

            this.selectCheckBox.Checked += SelectCheckBox_Checked;
            this.selectCheckBox.Unchecked += SelectCheckBox_Unchecked;
        }

        private void SelectCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.RaiseSelectionUncheck();
        }

        private void SelectCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.RaiseSelectionCheck();
        }

        /// <inheritdoc/>
        protected override void UnapplyTemplateCore()
        {
            base.UnapplyTemplateCore();
            this.selectCheckBox.Checked -= SelectCheckBox_Checked;
            this.selectCheckBox.Unchecked -= SelectCheckBox_Unchecked;
        }

        protected override DataGridFlyoutHeader CreateHeader()
        {
            DataGridFlyoutColumnHeader header = new DataGridFlyoutColumnHeader();
            header.Width = this.ActualWidth;
            header.OuterBorderVisibility = Visibility.Visible;
            return header;
        }

        private void OnContentTapped(object sender, TappedRoutedEventArgs e)
        {
            this.RaiseDescriptorContentTap();
        }

    }
}


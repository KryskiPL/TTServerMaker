// <copyright file="NonManualToggleButton.xaml.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.CustomControls
{
    using System.Windows;
    using System.Windows.Controls.Primitives;

    /// <summary>
    /// Interaction logic for NonManualToggleButton.xaml.
    /// </summary>
    public partial class NonManualToggleButton : ToggleButton
    {
        /// <summary>
        /// Using a DependencyProperty as the backing store for LockToggle. This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty LockToggleProperty =
            DependencyProperty.Register("LockToggle", typeof(bool), typeof(NonManualToggleButton), new UIPropertyMetadata(false));

        /// <summary>
        /// Gets or sets a value indicating whether the button can be toggled by the user.
        /// </summary>
        public bool LockToggle
        {
            get { return (bool)this.GetValue(LockToggleProperty); }
            set { this.SetValue(LockToggleProperty, value); }
        }

        /// <inheritdoc/>
        protected override void OnToggle()
        {
            if (!this.LockToggle)
            {
                base.OnToggle();
            }
        }
    }
}
﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DemoRedux.Tests.Pages
{
    public class UnitPlatform : IPlatform
    {
        readonly Func<VisualElement, double, double, SizeRequest> _getNativeSizeFunc;
        readonly bool _useRealisticLabelMeasure;

        public UnitPlatform (Func<VisualElement, double, double, SizeRequest> getNativeSizeFunc = null, bool useRealisticLabelMeasure = false)
        {
            this._getNativeSizeFunc = getNativeSizeFunc;
            this._useRealisticLabelMeasure = useRealisticLabelMeasure;
        }

        public SizeRequest GetNativeSize (VisualElement view, double widthConstraint, double heightConstraint)
        {
            if (_getNativeSizeFunc != null)
                return _getNativeSizeFunc (view, widthConstraint, heightConstraint);
            // EVERYTHING IS 100 x 20

            var label = view as Label;
            if (label != null && _useRealisticLabelMeasure) {
                var letterSize = new Size (5, 10);
                var w = label.Text.Length * letterSize.Width;
                var h = letterSize.Height;
                if (!double.IsPositiveInfinity (widthConstraint) && w > widthConstraint) {
                    h = ((int) w / (int) widthConstraint) * letterSize.Height;
                    w = widthConstraint - (widthConstraint % letterSize.Width);

                }
                return new SizeRequest (new Size (w, h), new Size (Math.Min (10, w), h));
            }

            return new SizeRequest(new Size (100, 20));
        }
    }
}
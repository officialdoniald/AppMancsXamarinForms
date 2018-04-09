using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Content.Res;
using AppMancsXamarinForms.CustomControls;
using AppMancsXamarinForms.Droid.CustomRenderers;

[assembly: ExportRenderer (typeof(CustomEntry), typeof(CustomEntryRenderer))]

namespace AppMancsXamarinForms.Droid.CustomRenderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        //protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        //{
        //    base.OnElementChanged(e);

        //    if (Control != null)
        //    {
        //        GradientDrawable gd = new GradientDrawable();

        //        //Below line is useful to give border color
        //        gd.SetColor(global::Android.Graphics.Color.White);

        //        //this.Control.SetBackgroundDrawable(gd);
        //        this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
        //        Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.White));

        //    }
        //}

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var nativeEditText = (global::Android.Widget.EditText)Control;
                var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
                shape.Paint.Color = Xamarin.Forms.Color.White.ToAndroid();
                shape.Paint.SetStyle(Android.Graphics.Paint.Style.Stroke);
                nativeEditText.Background = shape;
            }
        }
    }
}

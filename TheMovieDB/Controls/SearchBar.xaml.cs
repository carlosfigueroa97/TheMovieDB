using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TheMovieDB.Controls
{
    public partial class SearchBar : ContentView
    {
        #region Public Properties

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: nameof(Text),
            returnType: typeof(string),
            declaringType: typeof(string),
            defaultValue: string.Empty,
            propertyChanged: OnText);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty ReturnCommandProperty = BindableProperty.Create(
            propertyName: nameof(ReturnCommand),
            returnType: typeof(Command),
            declaringType: typeof(Command),
            defaultValue: null,
            propertyChanged: OnReturnCommand);

        public Command ReturnCommand
        {
            get => (Command)GetValue(ReturnCommandProperty);
            set => SetValue(ReturnCommandProperty, value);
        }

        public static readonly BindableProperty ReturnCommandParameterProperty = BindableProperty.Create(
            propertyName: nameof(ReturnCommandParameter),
            returnType: typeof(object),
            declaringType: typeof(object),
            defaultValue: null,
            propertyChanged: OnReturnCommandParameter);

        public object ReturnCommandParameter
        {
            get => (object)GetValue(ReturnCommandParameterProperty);
            set => SetValue(ReturnCommandParameterProperty, value);
        }

        #endregion

        #region Constructor

        public SearchBar()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        private static void OnText(BindableObject bindable, object oldVal, object newVal)
        {
            var newBindable = bindable as SearchBar;
            newBindable.search.Text = newVal.ToString();
        }

        private static void OnReturnCommand(BindableObject bindable, object oldVal, object newVal)
        {
            var newBindable = bindable as SearchBar;
            newBindable.search.ReturnCommand = (Command)newVal;
        }

        private static void OnReturnCommandParameter(BindableObject bindable, object oldVal, object newVal)
        {
            var newBindable = bindable as SearchBar;
            newBindable.search.ReturnCommandParameter = newVal;
        }

        #endregion
    }
}

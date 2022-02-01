using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace MediaPlayerFinalizerHang.ViewModels
{
    internal abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public ViewModelBase()
        {
            Dispatcher = Dispatcher.CurrentDispatcher;
        }

        protected Dispatcher Dispatcher { get; }

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        protected void VerifyDispatcher()
        {
            if (Dispatcher.CurrentDispatcher != Dispatcher)
                throw new InvalidOperationException("Dispatcher does not own current view model");
        }

        protected T VerifyDispatcher<T>(T value)
        {
            VerifyDispatcher();
            return value;
        }

        protected bool SetPropertyField<T>(T value, ref T field, [CallerMemberName] string propertyName = "")
        {
            if (value?.Equals(field) ?? field?.Equals(value) ?? true)
                return false;

            field = value;
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CanardEcarlate.Models
{
    public class ClasseParfaite : IDisposable, INotifyPropertyChanged, ICloneable
    {

        private bool _disposed = false;

        public event PropertyChangedEventHandler PropertyChanged;

        //public static ClasseParfaite Create(Int32 val)
        //{
        //    if(val > 100)
        //        //throw new ArgumentException();
        //        return null;

        //    return new ClasseParfaite(val);
        //}


        public ClasseParfaite()
        {

        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void Dispose()
        {
            //Toujours au début
            if (_disposed)
            {
                return;
            }

            //Code

            //Toujours à la fin
            GC.SuppressFinalize(this);
            _disposed = true;
        }

        ~ClasseParfaite()
        {
            Dispose();
        }


        public void RaisePropertyChanged<T>(ref T prop, T val, [CallerMemberName] String propName = "")
        {
            if (
                (prop == null && val != null)
                ||
                (prop != null && val != null && !(prop.Equals(val))))
            {
                prop = val;
                RaisePropertyChanged(propName);
            }
        }

        private void RaisePropertyChanged(String propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
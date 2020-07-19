using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Collections;
using ClassicAssist.Annotations;
using ClassicAssist.UI.Misc;

namespace ClassicAssist.Data.Skills
{
    public class SkillManager : INotifyPropertyChanged
    {
        private static readonly object _lock = new object();
        private static SkillManager _instance;
        private AvaloniaList<SkillEntry> _items = new AvaloniaList<SkillEntry>();

        private SkillManager()
        {
        }

        public AvaloniaList<SkillEntry> Items
        {
            get => _items;
            set => SetProperty( ref _items, value );
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static SkillManager GetInstance()
        {
            // ReSharper disable once InvertIf
            if ( _instance == null )
            {
                lock ( _lock )
                {
                    if ( _instance != null )
                    {
                        return _instance;
                    }

                    _instance = new SkillManager();
                    return _instance;
                }
            }

            return _instance;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged( [CallerMemberName] string propertyName = null )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }

        public void SetProperty<T>( ref T obj, T value, [CallerMemberName] string propertyName = "" )
        {
            obj = value;
            OnPropertyChanged( propertyName );
        }
    }
}
using System;
using Avalonia.Collections;
using ClassicAssist.Data.Hotkeys;
using ClassicAssist.UI.Misc;

namespace ClassicAssist.Data.Organizer
{
    public class OrganizerEntry : HotkeyEntry
    {
        private bool _complete;
        private AvaloniaList<OrganizerItem> _items = new AvaloniaList<OrganizerItem>();
        private bool _stack = true;

        public Func<bool> IsRunning;

        public bool Complete
        {
            get => _complete;
            set => SetProperty( ref _complete, value );
        }

        public int DestinationContainer { get; set; }

        public AvaloniaList<OrganizerItem> Items
        {
            get => _items;
            set => SetProperty( ref _items, value );
        }

        public int SourceContainer { get; set; }

        public bool Stack
        {
            get => _stack;
            set => SetProperty( ref _stack, value );
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
﻿#region License

// Copyright (C) 2020 Reetus
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

#endregion

using System.Threading;
using ClassicAssist.UI.Views;

namespace ClassicAssist.Data.Hotkeys.Commands
{
    [HotkeyCommand( Category = "Commands", Name = "Show GIF Capture" )]
    public class ShowGIFCaptureWindow : HotkeyCommand
    {
        private GIFRecorderWindow _window;

        public override void Execute()
        {
            Thread t = new Thread( () =>
            {
                _window = new GIFRecorderWindow();
                _window.ShowDialog(Assistant.Engine.MainWindow);
            } );

            t.SetApartmentState( ApartmentState.STA );
            t.IsBackground = true;
            t.Start();
        }
    }
}
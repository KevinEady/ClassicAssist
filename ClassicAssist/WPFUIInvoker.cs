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

using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ClassicAssist.Shared;
using ClassicAssist.UI.Views;

namespace ClassicAssist
{
    public class WPFUIInvoker : IUIInvoker
    {
        private readonly Dispatcher _dispatcher;

        public WPFUIInvoker( Dispatcher dispatcher )
        {
            _dispatcher = dispatcher;
        }

        public Task Invoke( string typeName, object[] ctorParam = null, Type dataContextType = null,
            object[] dataContextParam = null )
        {
            Type type = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault( t => t.Name == typeName && t.IsSubclassOf( typeof( Window ) ) );

            if ( type == null )
            {
                throw new ArgumentNullException( $"Cannot find type: ${typeName}" );
            }

            Window window = (Window) Activator.CreateInstance( type, ctorParam );

            if ( window == null )
            {
                throw new ArgumentNullException( $"Failed to create window of type: ${typeName}" );
            }

            if ( dataContextType != null )
            {
                object dc = Activator.CreateInstance( dataContextType, dataContextParam );
                window.DataContext = dc;
            }

            _dispatcher.Invoke( () => { window.Show(); } );

            return Task.CompletedTask;
        }

        public Task InvokeDialog<T>( string typeName, object[] ctorParam = null, T dataContext = default ) where T: class
        {
            Type type = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault( t => t.Name == typeName && t.IsSubclassOf( typeof( Window ) ) );

            if ( type == null )
            {
                throw new ArgumentNullException( $"Cannot find type: ${typeName}" );
            }

            Window window = (Window) Activator.CreateInstance( type, ctorParam );

            if ( window == null )
            {
                throw new ArgumentNullException( $"Failed to create window of type: ${typeName}" );
            }

            if ( dataContext != null )
            {
                window.DataContext = dataContext;
            }

            _dispatcher.Invoke( () => { window.ShowDialog(); } );

            return Task.CompletedTask;
        }

        public async Task<int> GetHueAsync()
        {
            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();
            Thread thread = new Thread( () =>
            {
                try
                {
                    HuePickerWindow window = new HuePickerWindow();
                    window.ShowDialog();

                    tcs.SetResult( window.SelectedHue );
                }
                catch ( Exception e )
                {
                    tcs.SetException( e );
                }
            } );

            thread.SetApartmentState( ApartmentState.STA );
            thread.Start();

            return await tcs.Task;
        }

        public void SetClipboardText( string text )
        {
            Clipboard.SetText( text );
        }

        public string GetClipboardText()
        {
            return Clipboard.GetText();
        }
    }
}
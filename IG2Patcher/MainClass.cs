/*
The MIT License(MIT)

Copyright(c) 2015 Daniel Schick

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

namespace IG2Patcher {
    public static class MainClass {
        [STAThread]
        public static int Main(string[] args) {
            AppDomain.CurrentDomain.UnhandledException += (o, e) => {
                var ex = (Exception)e.ExceptionObject;
                MessageBox.Show(string.Format("An unexpected error occurred and the application has to exit. Message: {0}", ex.ToString()), "Unexpected error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(666);
            };

            var wndMain = new Ui.WndMain();
            var app = new Application {
                ShutdownMode = ShutdownMode.OnMainWindowClose
            };

            app.Run(wndMain);

            return 0;
        }
    }
}

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

using IG2Patcher.Resources;
using Microsoft.Win32;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Globalization;
using System.Reflection;

namespace IG2Patcher.Ui {
    public class MenuButtonsEnumConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var button = (IG2MenuButtons)value;
            var attribs = (DisplayAttribute[])typeof(IG2MenuButtons).GetField(button.ToString()).GetCustomAttributes(typeof(DisplayAttribute), false);
            if(attribs.Length == 1) {
                return StringLUT.ResourceManager.GetString(attribs[0].Name);
            }

            return button.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public partial class WndMain : Window {
        private IG2BinInfo _originalIG2Info = null;
        private IG2BinInfo _currentIG2Info = null;

        public WndMain() {
            InitializeComponent();

            // We don't do any bindings in XAML. I'd have to create dependency properties
            // and value change handlers that I just skip to save some time. It's a small
            // app afterall :)
            cmbSurrogateButton.SelectedValue = IG2MenuButtons.None;

            Version v = Assembly.GetEntryAssembly().GetName().Version;
            Title = string.Format("{0} ({1})", StringLUT.Title_Application, v.ToString());
        }

        private void CloseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e) {
            Close();
        }

        private void btnSelectGamePath_Click(object sender, RoutedEventArgs e) {
            var dia = new OpenFileDialog {
                CheckFileExists = true,
                CheckPathExists = true,
                FileName = IG2BinInfo.BinaryName,
                Filter = StringLUT.OpenDialog_Filter_IG2Program + "|" + IG2BinInfo.BinaryName,
                Multiselect = false,
                Title = StringLUT.OpenDialog_IG2Title
            };

            var result = dia.ShowDialog(this);
            if((result != null) && (result.Value)) {
                _originalIG2Info = null;
                _currentIG2Info = null;
                txtbGamePath.Text = string.Empty;
                lblGameVersion.Content = string.Empty;
                cmbSurrogateButton.SelectedValue = IG2MenuButtons.None;
                cmbSurrogateButton.IsEnabled = false;

                if(!File.Exists(dia.FileName)) {
                    MessageBox.Show(string.Format(StringLUT.Error_FileNotFound, dia.FileName), StringLUT.Title_FileNotExisting, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var fileVersion = FileVersionInfo.GetVersionInfo(dia.FileName);
                var version = new Version(fileVersion.FileMajorPart, fileVersion.FileMinorPart, fileVersion.FileBuildPart, fileVersion.FilePrivatePart);
                var ig2info = IG2BinInfoPool.GetForVersion(version);
                if(ig2info == null) {
                    MessageBox.Show(string.Format(
                        StringLUT.Error_VersionNotSupported,
                        version.Major, version.Minor, version.Build, version.Revision),
                        StringLUT.Title_VersionNotSupported, MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }

                var currentInfo = ig2info.LoadFromFile(dia.FileName);

                _originalIG2Info = ig2info;
                _currentIG2Info = currentInfo;
                txtbGamePath.Text = dia.FileName;
                lblGameVersion.Content = version.ToString();
                cmbSurrogateButton.SelectedValue = _currentIG2Info.MultiplayerSurrogateButton;
                cmbSurrogateButton.IsEnabled = true;
            }
        }

        private void cmbSurrogateButton_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(_currentIG2Info != null) {
                _currentIG2Info.MultiplayerSurrogateButton = (IG2MenuButtons)cmbSurrogateButton.SelectedValue;
            }
        }

        private void PatchCommand_Executed(object sender, ExecutedRoutedEventArgs e) {
            var result = MessageBox.Show(string.Format(StringLUT.Warning_Disclaimer, Environment.NewLine), StringLUT.Title_Disclaimer, MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try {
                if(result == MessageBoxResult.Yes) {
                    _currentIG2Info.Patch(txtbGamePath.Text);
                    MessageBox.Show(StringLUT.Info_PatchSuccess, StringLUT.Title_PatchSuccess, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } catch(Exception ex) {
                MessageBox.Show(string.Format(StringLUT.Error_PatchFailed, ex.Message), StringLUT.Title_PatchFailed, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void PatchCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = (!string.IsNullOrWhiteSpace(txtbGamePath.Text)) && (_currentIG2Info != null);
        }
    }
}

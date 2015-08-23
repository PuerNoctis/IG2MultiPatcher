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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IG2Patcher {
    internal class IG2BinInfo : ICloneable {
        private const BindingFlags DEFAULT_BINDING = BindingFlags.NonPublic | BindingFlags.Instance;

        internal static string BinaryName { get { return "ig2_AddOn.exe"; } }

        internal IG2MenuCallbacks Callbacks { get; set; }
        internal IG2MenuFnPtrs FnPtrs { get; set; }

        internal IG2BinInfo() {
        }

        internal IG2MenuButtons MultiplayerSurrogateButton {
            get { return GetMultiplayerSurrogateButton(); }
            set { SetMultiplayerSurrogateButton(value); }
        }

        private IG2MenuButtons GetMultiplayerSurrogateButton() {
            var memValue = typeof(MemoryLocation).GetProperty("Value", DEFAULT_BINDING);

            foreach(var prop in typeof(IG2MenuFnPtrs).GetProperties(DEFAULT_BINDING)) {
                var fnPtrMem = (MemoryLocation)prop.GetValue(FnPtrs, null);
                int callbackAddr = (int)memValue.GetValue(fnPtrMem, null);
                if(callbackAddr == Callbacks.Multiplayer) {
                    return (IG2MenuButtons)Enum.Parse(typeof(IG2MenuButtons), prop.Name);
                }
            }

            return IG2MenuButtons.None;
        }

        private void SetMultiplayerSurrogateButton(IG2MenuButtons button) {
            var currentSurrogate = MultiplayerSurrogateButton;

            // Reset the current surrogate button's function pointer.
            if(currentSurrogate != IG2MenuButtons.None) {
                var enumString = currentSurrogate.ToString();
                var propSurrogate = typeof(IG2MenuFnPtrs).GetProperty(enumString, DEFAULT_BINDING);
                var propCallback = typeof(IG2MenuCallbacks).GetProperty(enumString, DEFAULT_BINDING);
                var fnPtrMem = (MemoryLocation)propSurrogate.GetValue(FnPtrs, null);
                var callbackValue = (int)propCallback.GetValue(Callbacks, null);
                var memValue = typeof(MemoryLocation).GetProperty("Value", DEFAULT_BINDING);
                memValue.SetValue(fnPtrMem, callbackValue, null);
            }

            // Set the new surrogate button's function pointer to the Multiplayer callback.
            if(button != IG2MenuButtons.None) {
                var enumString = button.ToString();
                var propSurrogate = typeof(IG2MenuFnPtrs).GetProperty(enumString, DEFAULT_BINDING);
                var fnPtrMem = (MemoryLocation)propSurrogate.GetValue(FnPtrs, null);
                var memValue = typeof(MemoryLocation).GetProperty("Value", DEFAULT_BINDING);
                memValue.SetValue(fnPtrMem, Callbacks.Multiplayer, null);
            }
        }

        internal IG2BinInfo LoadFromFile(string fileName) {
            if(string.IsNullOrWhiteSpace(fileName)) {
                throw new ArgumentNullException("fileName");
            }

            if(!File.Exists(fileName)) {
                throw new FileNotFoundException(string.Format(StringLUT.Error_FileNotFound, fileName), fileName);
            }

            var current = (IG2BinInfo)this.Clone();

            // Patch the function pointer LUT of the current binary info with the actual
            // callback addresses present in the image.
            using(var br = new BinaryReader(File.Open(fileName, FileMode.Open))) {
                var memValue = typeof(MemoryLocation).GetProperty("Value", DEFAULT_BINDING);

                foreach(var prop in typeof(IG2MenuFnPtrs).GetProperties(DEFAULT_BINDING)) {
                    var fnPtrMem = (MemoryLocation)prop.GetValue(current.FnPtrs, null);
                    br.BaseStream.Seek(fnPtrMem.Address, SeekOrigin.Begin);
                    int callbackAddr = br.ReadInt32();
                    memValue.SetValue(fnPtrMem, callbackAddr, null);
                }
            }

            return current;
        }

        public void Patch(string fileName) {
            if(string.IsNullOrWhiteSpace(fileName)) {
                throw new ArgumentNullException("fileName");
            }

            if(!File.Exists(fileName)) {
                throw new FileNotFoundException(string.Format(StringLUT.Error_FileNotFound, fileName), fileName);
            }

            using(var br = new BinaryWriter(File.Open(fileName, FileMode.Open))) {
                // Dump all the cached function pointers to the file, no matter if they changed or not.
                var memValue = typeof(MemoryLocation).GetProperty("Value", DEFAULT_BINDING);

                foreach(var prop in typeof(IG2MenuFnPtrs).GetProperties(DEFAULT_BINDING)) {
                    var fnPtrMem = (MemoryLocation)prop.GetValue(FnPtrs, null);
                    br.BaseStream.Seek(fnPtrMem.Address, SeekOrigin.Begin);
                    br.Write((int)memValue.GetValue(fnPtrMem, null));
                }
            }
        }

        public object Clone() {
            return new IG2BinInfo {
                Callbacks = (IG2MenuCallbacks)this.Callbacks.Clone(),
                FnPtrs = (IG2MenuFnPtrs)this.FnPtrs.Clone()
            };
        }
    }
}

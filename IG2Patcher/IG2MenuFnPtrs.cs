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
using System.Linq;
using System.Text;

namespace IG2Patcher {
    internal class IG2MenuFnPtrs : ICloneable {
        internal MemoryLocation Singleplayer { get; set; }
        internal MemoryLocation Campaign { get; set; }
        internal MemoryLocation Load { get; set; }
        internal MemoryLocation Tutorial { get; set; }
        internal MemoryLocation Player { get; set; }
        internal MemoryLocation Highscores { get; set; }
        internal MemoryLocation Options { get; set; }
        internal MemoryLocation Resume { get; set; }
        internal MemoryLocation Exit { get; set; }
        internal MemoryLocation Credits { get; set; }

        public object Clone() {
            return new IG2MenuFnPtrs {
                Singleplayer = (MemoryLocation)this.Singleplayer.Clone(),
                Campaign = (MemoryLocation)this.Campaign.Clone(),
                Load = (MemoryLocation)this.Load.Clone(),
                Tutorial = (MemoryLocation)this.Tutorial.Clone(),
                Player = (MemoryLocation)this.Player.Clone(),
                Highscores = (MemoryLocation)this.Highscores.Clone(),
                Options = (MemoryLocation)this.Options.Clone(),
                Resume = (MemoryLocation)this.Resume.Clone(),
                Exit = (MemoryLocation)this.Exit.Clone(),
                Credits = (MemoryLocation)this.Credits.Clone()
            };
        }
    }
}

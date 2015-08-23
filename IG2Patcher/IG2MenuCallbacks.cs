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
    internal class IG2MenuCallbacks : ICloneable {
        internal int Singleplayer { get; set; }
        internal int Campaign { get; set; }
        internal int Load { get; set; }
        internal int Tutorial { get; set; }
        internal int Player { get; set; }
        internal int Highscores { get; set; }
        internal int Options { get; set; }
        internal int Resume { get; set; }
        internal int Exit { get; set; }
        internal int Credits { get; set; }
        internal int Multiplayer { get; set; }

        public object Clone() {
            return new IG2MenuCallbacks {
                Singleplayer = this.Singleplayer,
                Campaign = this.Campaign,
                Load = this.Load,
                Tutorial = this.Tutorial,
                Player = this.Player,
                Highscores = this.Highscores,
                Options = this.Options,
                Resume = this.Resume,
                Exit = this.Exit,
                Credits = this.Credits,
                Multiplayer = this.Multiplayer
            };
        }
    }
}

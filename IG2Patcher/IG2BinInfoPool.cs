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
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace IG2Patcher {
    internal static class IG2BinInfoPool {
        private static Dictionary<string, IG2BinInfo> _pool = new Dictionary<string, IG2BinInfo>();

        static IG2BinInfoPool() {
            // ----------- V 2.2.0.0 ------------------
            _pool.Add(
                new Version(2, 2, 0, 0).ToString(),
                new IG2BinInfo {
                    Callbacks = new IG2MenuCallbacks {
                        Singleplayer = 0x00528AC0,
                        Campaign = 0x00527670,
                        Load = 0x00528E00,
                        Tutorial = 0x0052A6D0,
                        Player = 0x00529180,
                        Highscores = 0x00528D30,
                        Options = 0x00528EC0,
                        Resume = 0x005291D0,
                        Exit = 0x005277E0,
                        Credits = 0x00527970,
                        Multiplayer = 0x00528E50
                    },

                    FnPtrs = new IG2MenuFnPtrs {
                        Singleplayer = new MemoryLocation { Address = 0x00128A76, Value = 0x00 },
                        Campaign = new MemoryLocation { Address = 0x00128AF6, Value = 0x00 },
                        Load = new MemoryLocation { Address = 0x00128B76, Value = 0x00 },
                        Tutorial = new MemoryLocation { Address = 0x00128BF6, Value = 0x00 },
                        Player = new MemoryLocation { Address = 0x00128CB6, Value = 0x00 },
                        Highscores = new MemoryLocation { Address = 0x00128D36, Value = 0x00 },
                        Options = new MemoryLocation { Address = 0x00128DB6, Value = 0x00 },
                        Resume = new MemoryLocation { Address = 0x00128E26, Value = 0x00 },
                        Exit = new MemoryLocation { Address = 0x00128EA6, Value = 0x00 },
                        Credits = new MemoryLocation { Address = 0x00128F26, Value = 0x00 }
                    }
                });
            // ----------- V 2.3.2.0 ------------------
            _pool.Add(
                new Version(2, 3, 2, 0).ToString(),
                new IG2BinInfo {
                    Callbacks = new IG2MenuCallbacks {
                        Singleplayer    = 0x00528F40,
                        Campaign        = 0x00527AF0,
                        Load            = 0x00529280,
                        Tutorial        = 0x0052AB50,
                        Player          = 0x00529600,
                        Highscores      = 0x005291B0,
                        Options         = 0x00529340,
                        Resume          = 0x00529650,
                        Exit            = 0x00527C60,
                        Credits         = 0x00527DF0,
                        Multiplayer     = 0x005292D0
                    },

                    FnPtrs = new IG2MenuFnPtrs {
                        Singleplayer    = new MemoryLocation { Address = 0x00128EF6, Value = 0x00 },
                        Campaign        = new MemoryLocation { Address = 0x00128F76, Value = 0x00 },
                        Load            = new MemoryLocation { Address = 0x00128FF6, Value = 0x00 },
                        Tutorial        = new MemoryLocation { Address = 0x00129076, Value = 0x00 },
                        Player          = new MemoryLocation { Address = 0x00129136, Value = 0x00 },
                        Highscores      = new MemoryLocation { Address = 0x001291B6, Value = 0x00 },
                        Options         = new MemoryLocation { Address = 0x00129236, Value = 0x00 },
                        Resume          = new MemoryLocation { Address = 0x001292A6, Value = 0x00 },
                        Exit            = new MemoryLocation { Address = 0x00129326, Value = 0x00 },
                        Credits         = new MemoryLocation { Address = 0x001293A6, Value = 0x00 }
                    }
                });

            // ----------- V 2.3.3.0 ------------------
            _pool.Add(
                new Version(2, 3, 3, 0).ToString(),
                _pool["2.3.2.0"]);

            // ----------------------------------------
        }

        internal static IG2BinInfo GetForVersion(FileVersionInfo v) {
            return GetForVersion(new Version(v.FileMajorPart, v.ProductMinorPart, v.FileBuildPart, v.FilePrivatePart));
        }

        internal static IG2BinInfo GetForVersion(Version v) {
            var vs = v.ToString();
            return _pool.ContainsKey(vs) ? _pool[vs] : null;
        }
    }
}

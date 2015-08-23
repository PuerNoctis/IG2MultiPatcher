﻿/*
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

using System.ComponentModel.DataAnnotations;
using IG2Patcher.Resources;

namespace IG2Patcher {
    internal enum IG2MenuButtons {
        [Display(ResourceType = typeof(StringLUT), Name = "MenuButtons_None")]
        None,
        [Display(ResourceType = typeof(StringLUT), Name = "MenuButtons_Singleplayer")]
        Singleplayer,
        [Display(ResourceType = typeof(StringLUT), Name = "MenuButtons_Campaign")]
        Campaign,
        [Display(ResourceType = typeof(StringLUT), Name = "MenuButtons_Load")]
        Load,
        [Display(ResourceType = typeof(StringLUT), Name = "MenuButtons_Tutorial")]
        Tutorial,
        [Display(ResourceType = typeof(StringLUT), Name = "MenuButtons_Player")]
        Player,
        [Display(ResourceType = typeof(StringLUT), Name = "MenuButtons_Highscores")]
        Highscores,
        [Display(ResourceType = typeof(StringLUT), Name = "MenuButtons_Options")]
        Options,
        [Display(ResourceType = typeof(StringLUT), Name = "MenuButtons_Resume")]
        Resume,
        [Display(ResourceType = typeof(StringLUT), Name = "MenuButtons_Exit")]
        Exit,
        [Display(ResourceType = typeof(StringLUT), Name = "MenuButtons_Credits")]
        Credits
    }
}
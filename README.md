# IG2 Multiplayer Patcher

## UPDATE
A much more simpler solution to the problem has been discovered. **This patcher is, as of now, deemed to be obsolete.** Please use this technique henceforth: http://steamcommunity.com/app/271360/discussions/0/364042262886421054/

Host a session:
`"C:\Programmi\Steam\steamapps\common\Industry Giant 2\ig2_AddOn.exe" -host -playername "HostName"`

Join a session:
`""C:\Programmi\Steam\steamapps\common\Industry Giant 2\ig2_AddOn.exe" -client xxx.xxx.xxx.xxx -playername "ClientName"`

Many thanks to the author of this post.

## Disclaimer
Patching the Industry Giant II program is at your own risk. The creator of the mentioned patcher as well as United Independent Entertainment GmbH are not responsible for any damage done to the product or your computer. United Independent Entertainment GmbH is not involved in the creation of this patcher in any way. All rights to Industry Giant II are reserved by United Independent Entertainment GmbH.

## Introduction
The Steam version of Industry Giant II currently doesn't contain the Multiplayer mode anymore. However, the acting developer UIG has just removed the button from the main menu but left all the code behind within the game. This patcher is able to re-enable the Multiplayer as well as the Skirmish modes which reside in this removed menu. With this patcher you can select one of the buttons in the main menu which will henceforth open the Multiplayer menu instead.

As soon as UIG enables Multiplayer in a regular way this project will be discontinued.

## Instructions
1. Clone the repository directly or download it using the "Download ZIP" button on the right side of the page.
2. Compile the code (see section "Build from source"). If you don't have the means to compile the code yourself, or you don't want to, the repository also contains pre-built binaries for each game version in the "Packages" directory.
3. Run the patcher application.
4. Select the 'ig2_AddOn.exe' using the "..." button.
5. Select any button in the main menu you want to redirect to the Multiplayer menu. Common sense should tell you not to bind it to the "Exit" or "Options" button, but to rather less used ones like "Highscores" or "Credits" ;)
6. Click on the "Patch" button.

Once done patching you can start the game as usual. If you then press the button you selected previously, the Multiplayer menu should come up instead. From there you can select either LAN or Skirmish as game mode. If you want to revert the changes the patcher has done, just redo the steps mentioned above and select '<none>' from the list of buttons.

## Supported game versions
The following Steam versions of the game are supported:
* 2.2.0.0
* 2.3.2.0
* 2.3.3.0

All other versions are not supported unless listed. Every time UIG releases a new update for the game the changes will be reverted and you'll need to wait for an update of this patcher in order to enable the Multiplayer mode again.

## Build from source
The IG2Patcher.sln file included was created using Visual Studio 2015 Community Edition. The only available configurations should be "Release|x86" and "Debug|x86" which you can both build out-of-the-box. In case you don't have Visual Studio 2015 installed, you should be able to compile the IG2Patcher.csproj with any version of Visual Studio 2010 and up since the project only references .NET 4.0 assemblies. In case you set up the project anew, please bear in mind to remove the "AnyCpu" platform from the configuration or adapt it's settings to prefer 32-Bit mode since the program relies on pointers being 4-byte in length.

## Troubleshooting
In case the patcher somehow corrupts the ig2_AddOn.exe file completely and the game is not able to start, or the game crashes when you clicked on a patched button, either try setting the Multiplayer mode to '<none>' again with the patcher. If this fails to repair the damage, just re-download the file by running a game cache integrity check in Steam (right click on the game -> Properties -> Local Files -> Verify integrity of game cache...). If you ever have to do this, please report this bug e.g. on the Steam forums.

## Q&A
**Q: What game modes will be enabled?**

*A: The patch will enable the general Multiplayer menu which provides modes for "Online", "LAN" and "Skirmish". The "Online" mode can not be played since it requires the deprecated Game Spy Arcade software to be installed (which will not work anymore). "LAN" is fully supported and basically replaces the online play if you are using tunneling software like Hamachi or Tunngle. "Skirmish" will also work and enables you to customize more game settings than the regular "Singleplayer" mode.*

**Q: Does saving/loading work in Multiplayer?**

*A: Yes it does. Current testing has not shown any problems with saving/loading Multiplayer games.*

**Q: How stable is it?**

*A: During testing with one other person, there were a few out-of-sync errors (which could have been caused by zooming out too far and slowing down the game - it is not optimized for that). But it was possible to close the error message with the ESC key and continue playing without any issues. Overall it seems not less stable than the original version.*

**Q: Why must I wait until the patcher supports every new game version that has been released? Can't I just use the same patcher everytime?**

*A: When a new game version is released and the ig2_AddOn.exe was changed by UIG, all the fixed positions of the buttons within the file will most probably change slightly and the patcher would override the wrong data. So every time a new game version has been released these positions have to be re-determined and added to the patcher's internal logic.*

**Q: Why must I patch an existing button in the menu? Can't you just add a dedicated Multiplayer button?**

*A: I could, but it would mean adding code to the ig2_AddOn.exe file, which would require more work to be done during the patch progress, and many other parts of the file have to be adapted to that addition. Changing the existing button's behaviour is a far less intrusive change and hence simpler.*

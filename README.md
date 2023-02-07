# babbdi-modding
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A babbdi debug mod for experimenting for speedrunning

## How to install (at least for windows)
1. Install [MelonLoader 0.5.7](https://github.com/LavaGang/MelonLoader/releases/tag/v0.5.7) by selecting the babbdi exe file [(Instructions here by MelonLoader if you need them)](https://github.com/LavaGang/MelonLoader#how-to-use-the-installer)
2. Launch the game once to confirm MelonLoader has been installed, you should see the MelonLoader console launch along with the game
3. Close your game and download the mod(s) you like either from [GitHub Releases](https://github.com/VasilisThePikachu/babbdi-modding/releases) or [from here](https://nextcloud.pikachu.systems/s/wDzwbPcHoZNKpng)
4. Throw said ```.dll``` files into the **mods** folder on the games directory
5. Launch the game and enjoy

## Setting up a development enviroment
1. ```git clone``` the project
2. Open the folder of the mod you want to work on (or create a new solution in .net 4.7 if you want to create something new)
3. Open the .sln file with your IDE of choice
4. Make sure to setup your references, all these should be in the ```shareddll``` folder from this repo or you could also select them from the games ```managed``` folder
5. Push your changes with an explaination on the changes

## Licensed under the MIT license
Read more [here](https://github.com/VasilisThePikachu/babbdi-modding/blob/master/LICENSE)

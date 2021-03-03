# DSP-CustomAvatar
DSP 自定义人物模型

此插件基于非侵入性的mod框架，可以避免替换人物模型所需进行的繁琐的，侵入性的手动修改dll的操作。  

用法  
1.安装BepInEx (https://github.com/BepInEx/BepInEx/wiki/Installation) 只需将下载的文件解压到游戏根目录即可，无需覆盖任何文件。  
2.下载此插件，将dll文件拖入到 <游戏根目录>/BepInEx/Plugins 文件夹里。  
3. 如果你是第一次使用自定义模型，在<游戏根目录>/DSPGAME_Data 下新建StreamingAssets文件夹，将下载的assetbundle资源包改名为player (无后缀)并拖入此文件夹中。  
4.重启游戏，载入成功的话模型就已经被自动替换了，以后你想更换模型的话，只需要把新模型拖入StreamingAssets文件夹里并重命名为player，然后读取存档或者退回到开始菜单重进即可。如果你看到原版模型，说明载入失败了，请用记事本打开 <游戏根目录>/BepInEx 下的log文件查看，如果看到 Loading customized avatar failed. 说明载入失败，请检查模型完整性，命名是否正确等。  
5.目前此插件不包括立绘的替换。但是你可以根据网上教程自行注入dll。  

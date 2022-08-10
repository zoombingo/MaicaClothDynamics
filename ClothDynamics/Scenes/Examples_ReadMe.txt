Example Scenes Setup:

1. Make sure you installed "Burst" in the Package Manager
1. Unpack the "Examples" package
2. Open the "FightScene" and add all Scenes from the Examples Folder to the Build Settings. (Drag & Drop)
3. Run the the "FightScene" and select the other Scenes via DropDown

URP:
4. Unpack the "URP" package, it will overwrite the Materials and add RenderAssets

HDRP:
5. Unpack the "HDRP" package, it will overwrite the Materials and the FightScene with a Sky and Fog Volume (this will affect the other scenes when you load them from the FightScene)

If you get shader errors reimport them via the ClothDynamics TopMenu.

See more under the TopMenu: ClothDynamics -> Show Instructions
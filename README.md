# XMUbisoft Project `zero`

Unity Version: `2020.3.19f1` [Direct Download (Windows)](https://download.unity3d.com/download_unity/68f137dc9bbe/Windows64EditorInstaller/UnitySetup64-2020.3.19f1.exe)

## About the Game

Game Name: *No Time To Die*

Game Type: Top-down Role-playing Dungeon Crawl

<details>
  <summary>Story</summary>
  TODO
</details>


## Commit Guidelines

**Keep commit simple but including details (use verb if possible), in English.**

In principle, each commit should only maintain one subject, do not mix differenct types of changes together in one commit.

Follow this syntax to commit as far as possible:

```
<type>(<scope>): <subject>
<BLANK LINE>
<body>
```
Commit description `<body>` is not a necessity but do provide if there is any outstanding detail.

For example,
```
feat(camera): add camera follow
```
```
fix(level-3): fix lost bathroom tiles
```
```
docs: add commit guidelines on readme
```

More details refer to [this page](https://github.com/ubilabs/react-geosuggest/blob/master/CONVENTIONS.md)


## Coding Style Guide

To keep the project tidy and avoid conflicts, we use git branches to separate the different works, such as `level-1`, `weapons`, `HUD`. Big difference or small fix could be happended in branches.

We will have at least 6 levels of the game, the **level assets** are separated into `Assets/Levels/Level {num}` (e.g. `Assets/Levels/Level 1` ) to keep a tidy environment for coding. Basically we copy the pure images into `Assets/Levels/Level {num}/Sprites/Floor` and create tilemaps in `Assets/Levels/Level X/Tilemaps/Floor` for example.

```
# This is an example
.
├── Assets
│   ├── Levels
│   │   ├── Level\ 1
│   │   │   ├── Sprites
│   │   │   ├── Tilemaps
│   │   │   │   ├── Floor
│   │   │   │   ├── Wall
├── Plugins
├── Prefabs
├── Scenes
│   ├── Level\ 1. unity
├── Scripts
│   ├── Core
│   ├── Camera
│   ├── Controller
├── Sprites
└── *.meta
```

The sprites can be originated from `Assets/Sprites` but please do not use it directly from the directory otherwise it could lead to conflicts on editing sprites.

> If all sprites are sliced in 16x16 (or 32x32, in a certain number) pixels then everything works out.

The `Prefabs` directory is used to deposit all prefabs we defined such as `camera`, `characters`, etc.

The `Scripts` directory holds all the scripts and every `.cs` file should be put in a “namespace” subdirectory.

The Unity C# coding convention and and style examples can be found in https://github.com/raywenderlich/c-sharp-style-guide

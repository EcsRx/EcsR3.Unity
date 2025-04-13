# EcsRx.Unity

[EcsR3](https://github.com/EcsRx/ecsr3) is a reactive take on the common ECS pattern with a well separated design, this library builds off that basis and adds unity specific helpers and functionality.

[![Discord](https://img.shields.io/discord/488609938399297536.svg)](https://discord.gg/bS2rnGz)

If you do not need support for Unity and just want to use [EcsR3](https://github.com/EcsRx/ecsr3) in .net projects then head on over to the [EcsR3](https://github.com/EcsRx/ecsr3) repo [https://github.com/EcsRx/ecsr3](https://github.com/EcsRx/ecsr3).

> This is based on `EcsRx.Unity` which uses `rx` or `unirx` under the hood instead of `R3`.

## Features

### Architecture
- Simple ECS interfaces to follow
- Fully reactive architecture
- Favours composition over inheritance
- Adheres to inversion of control
- Lightweight codebase 
- Built in support for events (raise your own and react to them)
- Built in support for pooling (easy to add your own implementation or wrap 3rd party pooling tools)
- Built in support for plugins (wrap up your own components and systems and share them with others)
- Fully testable and easy to mock

### Unity specific
- Simple unity conventions to follow (if you dont like it, easy enough to roll your own)
- Built in support for editor viewing and editing of components
- Built in support for dependency injection via Zenject (or other DI frameworks)
- Lots of extension and architectural helpers to help MonoBehaviours interact with the core framework
- Plays nicely with scene first style setups

## Examples

There are a lot of example projects within the repository which you can clone and look at if you wish, or if you want more complex examples look at:

- [ecsrx/ecsrx.roguelike2d](https://github.com/ecsrx/ecsrx.roguelike2d) (Unity Rogluelike 2d using EcsRx)

- [ecsrx/ecsrx.buffs](https://github.com/ecsrx/ecsrx.buffs) (Example EcsRx plugin)

There is also a supplementary data-binding framework [grofit/bindingsrx](https://github.com/grofit/bindingsrx) which can speed up development of UI related interactions by adding one/two way data binding on UI and other unity objects.

## Requirements

- Unity 2018 (.net 4.5)
- [R3 + Unity Plugin](https://github.com/Cysharp/R3)
- [Zenject/Extenject 6+](https://github.com/modesttree/Zenject)

## Installation

You can take the unitypackage installation file from the relevent release.

- EcsR3.Unity.unitypackage

The package contains a wrapper around the **Core** framework and some unity helpers. The core is provided as dlls which are built for release, if you want/need to access the debug versions pull down the [EcsRx/EcsR3](https://github.com/EcsRx/ecsr3) master version and build for debug and use those dlls.

## Quick Start

- Install EcsR3.Unity package
- Install Zenject/Extenject
- Install R3 (+ Unity Plugin) 

You will ultimately need to create a `SceneContext` from *Zenject* then create an your own implementation of `EcsR3ApplicationBehaviour` where you can setup your systems and entities.

Much like any other ECS implementation you have the notion of entities (`IEntity`), components (`IComponent`) and systems (`ISystem`), although there are a few types of systems which you can implement based upon your needs, check out the docs folder for more information on these subjects as its more than just a 1 liner.

## Docs

- [EcsR3 Core Docs](https://github.com/EcsRx/ecsr3/tree/master/docs)
- [EcsR3 Unity Docs](/docs)

If you have any problems, or feel that you are still unsure of something head on over to our [Discord Channel](https://discord.gg/bS2rnGz).

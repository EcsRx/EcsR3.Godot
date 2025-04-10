# EcsR3.Godot

This is the Godot flavour of EcsRx!

[![License][license-image]][license-url]
[![Join Discord Chat][discord-image]][discord-url]
[![Documentation][gitbook-image]][gitbook-url]

## What is it?

It is an ECS style framework which puts architecture, design and flexibility above most other concerns.

It builds on top of the existing [EcsRx](https://github.com/EcsRx/ecsrx) framework and adds conventions and bootstrappers for Godot specific scenarios.

## Getting started

I am still unclear as to how Godot expects `plugins` to be provided, so there is no nuget or custom distribution, but you can just copy the `Plugins/EcsR3.Godot` folder to your own project.

> As with all EcsRx engine integrations you will need to create your own application inheriting from `GodotApplication`, and then you add it as a node in the scene and off you go.

Look in the `Examples` folder for an Asteroids style example like the `EcsRx.Monogame` one, as well as a very simple `Hello World` example.

## Docs

There is a book available which covers the main parts for the core EcsRx framework which can be found here:

[![Documentation][gitbook-image]][gitbook-url]

[discord-image]: https://img.shields.io/discord/488609938399297536.svg
[discord-url]: https://discord.gg/bS2rnGz
[license-image]: https://img.shields.io/github/license/ecsrx/ecsrx.monogame.svg
[license-url]: https://github.com/EcsRx/ecsrx.monogame/blob/master/LICENSE
[gitbook-image]: https://img.shields.io/static/v1.svg?label=Documentation&message=Read%20Now&color=Green&style=flat
[gitbook-url]: https://ecsrx.gitbook.io/project/
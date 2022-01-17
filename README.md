# diplo

This repo is a backup for an old C# code written in 2012. The program
offers an interface and a resolution system for home made flavors of
the board game
[Diplomacy](https://boardgamegeek.com/boardgame/483/diplomacy).

A [Makefile](Makefile) allows to build the program on Linux, with the
help of Mono.

The syntax of the orders is as follow:

  * `- FOO` to move the unit to region `FOO`;

  * `s FOO - BAR` to support the attack from `FOO` to `BAR`;

  * `c FOO - BAR` to help move from `FOO` to `BAR`.

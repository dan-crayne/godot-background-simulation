# Background Simulation with the Godot game engine

Simple background simulation using the Godot Engine. Provides a proof of concept for simulating background things
in C# and simultaneously displaying the results in Godot.

This project aims to keep CPU and memory footprint low, so it can be used in real-time simulation games. It uses
object pooling to avoid unnecessary memory allocations and garbage collection as well as chunk loading to keep
displayed objects to a minimum.
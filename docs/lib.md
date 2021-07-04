# Library Documentation

There are two main ways to interface with the state of the program via scripting: firstly, through the program itself, which exposes select access to the application state; secondly, through the `core` Python package, which wraps most of the former in a more Pythonic format, and also provides additional functionality with brew creators in mind.

[toc]

---

## VCS

​	The `vcs.dll` assembly contains the compiled code that runs the main application. It also provides access to several classes and libraries through the `VCS` namespace.

### `VCS` namespace

​	`VCS.Data`

---

## core

​	The `core` package provides Pythonic wrapping for the `VCS` namespace, as well as its own packages, objects, and classes that are valuable for making a brew.
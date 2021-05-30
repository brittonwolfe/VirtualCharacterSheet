# 5E Core

This is the module for the official, Open Game License SRD for D&D fifth edition.
You can find legal information in `LEGAL.md`.

## Package Overview
One of the goals of VCS's included brews is to provide model implementation for anyone interested in creating a brew.

### Root

#### brew.py

`brew.py` is required for any brew to load: it acts as the main method for loading a brew, which means that it's also responsible for loading all the other packages it needs to run.

### shell.py

`shell.py` contains the shell-like CLI specific to the package.

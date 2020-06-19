# VirtualCharacterSheet
The Virtual Character Sheet is a project for better online and local TTRPG bookkeeping through robust, scriptable systems and modules, with support for things like Homebrew and importing relevant spells, items, etc. from books with minimal effort.

## About the Linux branch
While the Linux branch is still very much a work in progress, cross-platform compatibility is definitely on my roadmap, as well as web-application versions that can be hosted by the GM that add more flexibility to how the game can be played and interacted with. Additionally, developing for more platforms has been a goal of mine, and this is a great avenue to explore that.

### So what's the difference?
The Linux branch isn't currently planned to have GUI functionality, instead focusing on a TUI interface. It'll be cool, trust me.
It centers around the interactive Python console (powered by IronPython) that the main branch uses, but makes it a fully-featured terminal program with more functionality without breaking compatibility for brews (outside of the requirement of scripted TUI forms).
Due to the lower overhead for development, the Linux branch is likely going to have a quicker release cycle, since new features are much easier to push out to a TUI. The goal, however, has not changed: making bookkeeping less painful so we can all enjoy playing our favorite TTRPGs in a way that lets us explore their fullest potential.
I'll likely add the capability to have actual GUIs in the Linux branch at some point, and it'll likely be handled *through* Python rather than C#, so that the GUIs can be scripted--putting them in the brew's control. Having the UI be largely scriptable is a roadmap goal for the main branch too, and I think I'll see earlier success with it here.
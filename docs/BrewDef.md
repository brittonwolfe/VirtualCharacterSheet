# Creating a Brew Module

## An Important Remark

Brew setup scripts **must** be named `brew.py` for the file selector dialog to show them. If your file is not named `brew.py` it **will not** show up in the file selector dialog.

## `brew.def_brew(name)`

The first step in creating a brew—or anything, really—is to define it! Initialize it with a unique name (brews with duplicate names are not allowed), and ta-dah! You're ready to get going.

## Optional: Metadata

Every brew has a `Meta` object for stroring metadata. This includes the brew's Title (the human-readable description of what the brew is/does), a description.
It's heavily recommended that you set `{your brew object}.Meta.Dir = brew.Path` so the user can navigate directly to the brew's directory in their filesystem.

## Adding Character Injectors

Character Injectors allow you to dynamically add scripted behaviors to the Character class. It requires two parts: the first is to define a function that takes a Character as an argument, and adds functions as behaviors, and the second is to use your Brew object to add those injectors to the Character class.

An important note is that an injector gets run every time a character is defined, and the object's constructor passes that character as an argument to all the attached injector functions, so your parameter *is* a Character. You can directly call its `AddBehavior(string, function)` method as well as assigning data to its `Info` and `Meta` properties.

After setting up your injector functions, add them to the Character class's Injection list with `{your brew object}.AddCharacterInjector(function)`!

## Loading Your Other Scripts

After setting everything up, you're ready to build up the other things your brew has to offer—classes, items, races, feats, spells… all of this is easily set up with a simple `import` statement like any other Python program.
# Creating a Brew Module

The brew setup script should be named `brew.py`. This standardizes things so the user can just input the path to the folder the module is in.

## Defining the object

Two ways to provide brews are provided: the first is to import the `Brew` class from the application:

```python
from VirtualCharacterSheet import Brew
brew_object = Brew('unique name')
```

... and the second is to use the built-in `brew` object:

```python
brew_object = brew.def_brew('unique name')
```

## Optional: Metadata

Every brew has a `Meta` object for stroring metadata. This includes the brew's Title (the human-readable description of what the brew is/does), a description.

It's recommended to set the `Dir` property. If you don't, I'll be very upset with you and the user will have less information in case they run into errors with brews that have conflicting names.

```python
brew_object.Meta.Dir = brew.Path
```

There is no requirement for other `Meta` properties, but the following are recommended:

```python
brew_object.Meta.Title = 'the name of the module or game this brew implements'
brew_object.Meta.Description = 'an optional description'
brew_object.Meta.Owner = 'the owner of the intellectual property this brew implements'
brew_object.Meta.Website = 'the link to the repository/download site for this brew. This is mainly used for clients to be able to download brews they need to connect to a host game'
brew_object.Meta.GameSite = 'the link to the game or module this brew implements, if it has one'
```

## Adding Character Injectors

Character injectors let you inject properties and methods into `Character` objects. The `Brew` class's `AddCharacterInjector(function)` method takes a function that consumes a `Character` object, and should follow this structure:

```python
def inject_sample(character):
    character.AddBehavior('property_name', function(character))

brew_object.AddCharacterInjector(inject_sample)
```

You can add as many injectors as you'd like, so feel free to organize them as you wish. Just keep in mind that using spaces in the behavior name is discouraged since it limits the accessibility of those behaviors.


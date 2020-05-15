# Defining a Class through Python

## Setting up the Class

To initialize a Class, first define it:

```py
yourclass = def_class("yourclass")
yourclass.HitDie = 0
```

Remember to substitute `yourclass` with your classname and the value of `yourclass.HitDie` with the hit dice of your class!

## Optional: Add Book and Page Numbers

Additionally, you can add the book and pages for a class if it's from a book! With the fighter, for example…

```py
yourclass.Book = "Player's Handbook"
yourclass.Pages = "70-75"
```

These values are just stored as `string`s, so there's no strict formatting.

## Setting up Save Proficiencies


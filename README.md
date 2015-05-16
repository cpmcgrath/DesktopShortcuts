Desktop Shortcuts
=====================

Desktop Shortcuts is a simple utility to delete shortcuts that installers add.

## Why is this needed?

When you install programs they often add a shortcut to the desktop. To keep the desktop clean you delete it. And then you update the program and the shortcut comes back.

This is a problem with most updaters. They override a decision the user has explictly made.

## How does it work?

Before running an installer, run the command

**DesktopShortcuts /record**

After the installer runs, run the command

**DesktopShortcuts /record**

## What are all the command line options?

    /record
    /restore
    /discard
    /output
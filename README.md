# Skribbl.io Word Generator
__This is the command-line version of the skribbl.io word generator I made with Windows Forms, you can find that [here](https://github.com/xFuney/skribblio-word-generator).__

This program takes a newline-separated TXT file and converts it into a CSV format ready to be copy/pasted into a skribbl.io game.

This was thrown together to make taking an over 50-entry newline-separated word pool and converting it to a compatible CSV format much simpler, as I'm pretty lazy and wanted to get it done as quick as possible.

# Text File Syntax
This program accepts newline-separated text files, the syntax is below.
```
these
are
words
that
will
be
parsed
by
the
program
- this will not, as this is a comment.
- comments are delimited by a dash.
- this allows you to separate "categories" for better legibility
```
Some sample text files will be available here soon.
# Build Instructions
This was programmed using Visual Studio 2019, this should be as easy as:
```
git clone https://github.com/xFuney/skribblio-word-generator-cmd.git
```
Then open the Project up in VS2019 - settings should be set for proper building automatically, raise an issue if it isn't.

Please don't report issues if they're to do with your environment - "exotic" environments (non-VS2019) shouldn't have issues raised unless they are proven to affect non-exotic builds.
# Run Instructions
You'll find releases [here](https://github.com/xFuney/skribblio-word-generator-cmd/releases).

The easiest way to use this program is to drag and drop a proper text file into the executable from Windows Explorer, and get the CSV output from the program (these will be outputted to "output.csv" in your working directory, unless it already exists - you will recieve a confirmation prior to any data being overwritten).

You can also pass your filename/directory to the program directly using Command Prompt, and you could probably create a shortcut to call the program on demand - I haven't tried this so don't expect shortcutting to work out of the box.

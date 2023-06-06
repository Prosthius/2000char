### Paste text into ./input/input.txt, the text will be output into several files in ./output, each consisting of up to a user specified number of characters, including whitespace.

# Packages
- **Self Contained:** Includes the Dotnet 7 runtime in the package
- **Not Self Contained:** Requires installing the Dotnet 7 runtime separately

# Usage:
    ./LLM-Char [-c | --characters] [-nw | --no-whitespace] [-rc | --remove-char]
    ./LLM-Char [-h | --help]
    ./LLM-Char [-d | --delete]
    
# Options:
    -h  --help:             Show this screen
    -v  --version:          Show the version number
    -d  --delete:           Delete all output files that match 'output*.txt'
    -c  --characters:       Set the number of characters per output file (default: 2000)
    -nw --no-whitespace:    Remove all whitespace from the input text
    -rc --remove-char:      Remove all instances of a character from the input text. Supports inputting multiple characters one after the other

# Examples:
    # Output 1000 characters per file and remove all whitespace, commas and periods
    ./LLM-Char -c 1000 -nw -rc ,.

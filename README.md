### Paste text into ./input/input.txt, the text will be output into several files in ./output, each consisting of up to a user specified number of characters, including whitespace.

<ul>Choose the self-contained package if you do not have dotnet 7 installed.</ul>

# Usage:
    ./LLM-Char [-c | --characters] [-nw | --no-whitespace] [-rc | --remove-char]
    ./LLM-Char [-h | --help]
    ./LLM-Char [-d | --delete]
    
# Options:
    -h  --help:             Show this screen
    -v  --version:          Show version
    -d  --delete:           Delete all output files that match 'output*.txt'
    -c  --characters:       Set the number of characters per output file (default: 2000)
    -nw --no-whitespace:    Remove all whitespace from the input text
    -rc --remove-char:      Remove all instances of a character from the input text

# Examples:
    # Output 1000 characters per file and remove all whitespace and commas 
    ./LLM-Char -c 1000 -nw -rc , 

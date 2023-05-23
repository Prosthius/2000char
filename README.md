### Paste the text into ./input/input.txt, the text will be output into several files in ./output, each consisting of up to a user specified number of characters, including whitespace.

**Choose the self-contained package if you do not have dotnet 7 installed.**

# Usage:
    ./LLM-Char [-c | --characters]
    ./LLM-Char [-h | --help]
    ./LLM-Char [-d | --delete]
    
# Options:
    -h --help:          Show this screen
    -d --delete:        Delete all output files that match 'output*.txt'
    -c --characters:    Set the number of characters per output file (default: 2000)

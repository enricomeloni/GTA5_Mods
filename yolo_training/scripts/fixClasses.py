import sys
from os.path import join
from os import walk
from progressbar import progressbar
import fileinput


dataDir = sys.argv[1]

txtFiles = [join(root, file) for root, dirs, files in walk(dataDir) for file in files if file.endswith(".txt")]
#print(dirs)
for txtFile in progressbar(txtFiles):
    with fileinput.input(txtFile, inplace=True) as file:
        for line in file:
            if line.startswith("8"):
                print("6" + line[1:], end='')
            else:
                print(line, end='')



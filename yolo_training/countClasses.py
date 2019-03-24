from os import listdir, walk
from os.path import isfile, isdir, join
import sys
import progressbar


dataDir = sys.argv[1]
txtFiles = [join(root,file) for root,dirs,files in walk(dataDir, False) for file in files if file.endswith(".txt") and file != "list.txt"]

classCount = [0] * 7

for txtFilePath in progressbar.progressbar(txtFiles):
    txtFile = open(txtFilePath)
    for line in txtFile.readlines():
        classNumber = int(line.split(" ")[0])
        classCount[classNumber] += 1

print(f"Total annotations: {sum(classCount)}")
print(classCount)


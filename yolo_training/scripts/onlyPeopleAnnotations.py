from os import listdir, walk, makedirs
from os.path import isfile, isdir, join
import sys
import progressbar


dataDir = sys.argv[1]
dstDir = sys.argv[2]

dirs = [join(root,dir) for root, dirs, files in walk(dataDir) for dir in dirs]
for dir in dirs:
    newDir = join(dstDir, dir.replace(dataDir, ""))
    makedirs(newDir, exist_ok=True)

txtFiles = [join(root,file) for root,dirs,files in walk(dataDir, False) for file in files if file.endswith(".txt") and file != "list.txt"]

classCount = [0] * 7

for txtFilePath in progressbar.progressbar(txtFiles):
    dstFilePath = join(dstDir, txtFilePath.replace(dataDir,""))
    with open(txtFilePath) as srcFile:
        with open(dstFilePath, "w+") as dstFile:    
            for line in srcFile.readlines():
                splitLine = line.split(" ")
                classNumber = int(splitLine[0])
                if classNumber == 6:
                    splitLine[0] = "0"
                    dstFile.writelines(" ".join(splitLine))

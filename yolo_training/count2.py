from os import listdir, walk
from os.path import isfile, isdir, join
import sys
import progressbar



with open("./train_win/valid.scaled.txt") as file:
     lines = file.readlines()

txtFiles = [line.replace("png","txt").replace("\n","") for line in lines]

classCount = [0] * 7

for txtFilePath in progressbar.progressbar(txtFiles):
    txtFile = open(txtFilePath)
    for line in txtFile.readlines():
        classNumber = int(line.split(" ")[0])
        classCount[classNumber] += 1

print(f"Total annotations: {sum(classCount)}")
print(classCount)


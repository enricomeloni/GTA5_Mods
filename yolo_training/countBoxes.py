from os import listdir, walk
from os.path import isfile, isdir, join
import sys
import progressbar


validListPath = sys.argv[1]

with open(validListPath) as validListFile:
    validImgs = validListFile.readlines()
    validTxts = [validImg.replace(".png", ".txt").replace(".jpg", ".txt").replace(".JPG", ".txt").replace(".jpeg", ".txt").replace("\n","") for validImg in validImgs]
    print(validTxts)


classCount = [0]*7

for txtFilePath in validTxts:
    txtFile = open(txtFilePath)
    for line in txtFile.readlines():
        classNumber = int(line.split(" ")[0])
        classCount[classNumber] += 1
    


print(classCount)
print(f"Total boxes: {sum(classCount)}")


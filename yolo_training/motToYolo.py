import sys
from os.path import join
from os import walk, makedirs
from progressbar import progressbar
from shutil import copyfile
import re

srcFilePath = sys.argv[1]
dstDir = sys.argv[2]
classNumber = sys.argv[3]
makedirs(dstDir, exist_ok=True)


with open(srcFilePath, "r") as srcFile:
        line = srcFile.readline()
        imgSize = (1920,1080)
        currentFrame = 1
        fileName = f"{currentFrame:06}.txt"
        currentFile = open(join(dstDir, fileName), "w+")

        lines = srcFile.readlines()
        lines = sorted(lines)

        for line in lines:
            print("input => " + line, end='')
            values = line.split(',')
            if(int(values[0]) != currentFrame):
                currentFrame = int(values[0])
                fileName = f"{currentFrame:06}.txt"
                print("opening file " + fileName)
                currentFile = open(join(dstDir, fileName), "w+")
            motClass = int(values[7])
            if motClass != 1:
                continue
            xTopLeft, yTopLeft, width, height = (float(values[2]), float(values[3]), float(values[4]), float(values[5]))
            print(f"extracted <= {xTopLeft}, {yTopLeft}, {width}, {height}")

            xCenter = (xTopLeft + width/2) / imgSize[0]
            yCenter = (yTopLeft + height/2) / imgSize[1]

            xSize = width / imgSize[0]
            ySize = height / imgSize[1]
            outputLine = f"{classNumber} {xCenter} {yCenter} {xSize} {ySize}\n"
            print("output => " + outputLine)
            currentFile.writelines(outputLine)



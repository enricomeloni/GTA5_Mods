import sys
from os.path import join
from os import walk, makedirs
from progressbar import progressbar
from shutil import copyfile
import re

srcDir = sys.argv[1]
dstDir = sys.argv[2]
makedirs(dstDir, exist_ok=True)

dirs = [join(root,dir) for root, dirs, files in walk(srcDir) for dir in dirs]
for dir in dirs:
    newDir = join(dstDir, dir.replace(srcDir, ""))
    print(newDir)
    makedirs(newDir, exist_ok=True)


txtFiles = [join(root, file) for root, dirs, files in walk(srcDir) for file in files if file.endswith(".txt")]
#print(dirs)
for txtFilePath in progressbar(txtFiles):
    dstFilePath = join(dstDir, txtFilePath.replace(srcDir,""))
    with open(txtFilePath, "r") as srcFile:
        with open(dstFilePath, "w+") as dstFile:
            line = srcFile.readline()
            imgSize = (0,0,0)
            while line:
                if "Image size" in line:
                    match = re.search("(\d+) x (\d+) x (\d+)", line)
                    imgSize = (float(match.group(1)), float(match.group(2)), float(match.group(3)))
                    print("Size is " + str(imgSize))

                if "Details for pedestrian" in line:
                    print(line, end='')
                    srcFile.readline() #skip next line
                    line = srcFile.readline()
                    # (Xmin, Ymin) - (Xmax, Ymax)
                    match = re.search("\((\d+), (\d+)\) - \((\d+), (\d+)\)", line)
                    xMin, yMin, xMax, yMax = (float(match.group(1)), float(match.group(2)), float(match.group(3)), float(match.group(4)))
                    xSize = (xMax - xMin) / imgSize[0]
                    ySize = (yMax - yMin) / imgSize[1]
                    xCenter = ( (xMax + xMin) / 2 ) / imgSize[0]
                    yCenter = ( (yMax + yMin) / 2 ) / imgSize[1]
                    
                    print(f"6 {xCenter} {yCenter} {xSize} {ySize}")
                    dstFile.writelines(f"6 {xCenter} {yCenter} {xSize} {ySize}\n")
                    srcFile.readline() #skip other line
                    
                line = srcFile.readline()



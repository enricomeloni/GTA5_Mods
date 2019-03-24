import sys
from os.path import join, isdir
from os import listdir, system
from progressbar import progressbar
from shutil import copyfile
import re

#train dir
srcDirPath = sys.argv[1]
classNumber = sys.argv[2]

motDirs = [join(srcDirPath,motDir) for motDir in listdir(srcDirPath) if isdir(join(srcDirPath,motDir))]
for motDir in motDirs:
    gtFile = join(motDir, "gt", "gt.txt")
    imgDir = join(motDir, "img1")
    
    system(f"python ./motToYolo.py {gtFile} {imgDir} {classNumber}")
system(f"python ./createFileList.py {srcDirPath} jpg")   
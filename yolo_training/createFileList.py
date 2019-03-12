from os import listdir
from os.path import isfile, isdir, join
import sys
import progressbar


dataDir = sys.argv[1]
cameraDirs = [join(dataDir, cameraDir) for cameraDir in listdir(dataDir) if isdir(join(dataDir,cameraDir))]
cameraIdx = 0
with open(join(dataDir,"list.txt"), 'w+') as listFile:    
    for cameraDir in progressbar.progressbar(cameraDirs):
        bmpFiles = [join(cameraDir,file) for file in listdir(cameraDir) if isfile(join(cameraDir,file)) and file.endswith('.png')]
        
        for bmpFile in progressbar.progressbar(bmpFiles):
            listFile.write(bmpFile+"\n")

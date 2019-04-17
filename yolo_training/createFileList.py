from os import listdir, walk
from os.path import isfile, isdir, join
import sys
import progressbar


dataDir = sys.argv[1]
#format = sys.argv[2]
images = [join(root,file) for root,dirs,files in walk(dataDir, False) for file in files if not file.endswith(".txt")]

with open(join(dataDir,"list.txt"), 'w+') as listFile:
    for bmpFile in progressbar.progressbar(images):
        listFile.write(bmpFile+"\n")

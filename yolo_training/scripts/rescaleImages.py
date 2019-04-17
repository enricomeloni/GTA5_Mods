import cv2
import sys
from os.path import join
from os import walk, makedirs
from progressbar import progressbar

srcDir = sys.argv[1]
dstDir = sys.argv[2]
width = int(sys.argv[3])
height = int(sys.argv[4])

print("Rescaling from " + srcDir + " to " + dstDir + " (" + str(width) + ", " + str(height) +") ")

dirs = [join(root,dir) for root, dirs, files in progressbar(walk(srcDir)) for dir in dirs]
#print(dirs)
#newDirs = []
for dir in dirs:
    #print("replaced: " + dir.replace(srcDir, ""))
    newDir = join(dstDir, dir.replace(srcDir, ""))
    #print("newDir: " + newDir)
    makedirs(newDir, exist_ok=True)
    #newDirs.append(newDir)

#print(newDirs)

imgFiles = [join(root, file) for root, dirs, files in walk(srcDir) for file in files if file.endswith(".png")]
#print(dirs)
for imgFile in progressbar(imgFiles):
    dstImgFile = join(dstDir, imgFile.replace(srcDir,""))
    img = cv2.imread(imgFile)
    rescaledImg = cv2.resize(img, dsize=(width, height), interpolation=cv2.INTER_CUBIC)
    cv2.imwrite(dstImgFile, rescaledImg)



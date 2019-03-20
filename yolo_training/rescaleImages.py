import cv2
import sys
from os.path import join
from os import walk, makedirs
from progressbar import progressbar

srcDir = sys.argv[1]
dstDir = sys.argv[2]

dirs = [join(root,dir) for root, dirs, files in walk(srcDir) for dir in dirs]
for dir in dirs:
    newDir = join(dstDir, dir.replace(srcDir, ""))
    makedirs(newDir, exist_ok=True)



imgFiles = [join(root, file) for root, dirs, files in walk(srcDir) for file in files if file.endswith(".png")]
#print(dirs)
for imgFile in progressbar(imgFiles):
    dstImgFile = join(dstDir, imgFile.replace(srcDir,""))
    img = cv2.imread(imgFile)
    rescaledImg = cv2.resize(img, dsize=(1280, 720), interpolation=cv2.INTER_CUBIC)
    cv2.imwrite(dstImgFile, rescaledImg)



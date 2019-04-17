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
for imgFile in imgFiles:
    dstImgFile = join(dstDir, imgFile.replace(srcDir,""))
    img = cv2.imread(imgFile)
    height, width, channels = img.shape
    print(f"{imgFile} is {width}x{height}x{channels}")
    w = int(1920/2)
    h = int(1080/2)
    x = int(1920/4)
    y = int(1080/4)
    crop_img = img[y:y+h, x:x+w]
    cv2.imwrite(dstImgFile, crop_img)

    srcAnnPath = imgFile.replace("png","txt")
    dstAnnPath = dstImgFile.replace("png","txt")
    with open(srcAnnPath) as srcAnn:
        with open(dstAnnPath, "w+") as dstAnn:
            for line in srcAnn.readlines():
                vals = line.split(" ")
                classId = vals[0]
                newVals = [vals[0]]
                newVals[1:3] = [2*float(x) - 0.5 for x in vals[1:3]]
                newVals[3:5] = [2*float(x) for x in vals[3:5]]
                print(newVals)
                dstAnn.writelines(" ".join(str(x) for x in newVals) + "\n")






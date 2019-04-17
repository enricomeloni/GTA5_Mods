import sys
from os.path import join
from os import walk, makedirs
from progressbar import progressbar
from shutil import copyfile

srcDir = sys.argv[1]
dstDir = sys.argv[2]

dirs = [join(root,dir) for root, dirs, files in walk(srcDir) for dir in dirs]
for dir in dirs:
    newDir = join(dstDir, dir.replace(srcDir, ""))
    makedirs(newDir, exist_ok=True)



txtFiles = [join(root, file) for root, dirs, files in walk(srcDir) for file in files if file.endswith(".txt")]
#print(dirs)
for txtFile in progressbar(txtFiles):
    dstTxtFile = join(dstDir, txtFile.replace(srcDir,""))
    copyfile(txtFile, dstTxtFile)



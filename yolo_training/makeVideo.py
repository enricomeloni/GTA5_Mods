import sys
import matplotlib
import numpy as np
import matplotlib.pyplot as plt
import matplotlib.animation as manimation
import cv2
import progressbar

from os import listdir
from os.path import isfile, isdir, join
import codecs

colors = [
    (255, 0, 0),
    (255, 128, 0),
    (255, 255, 0),
    (128, 255, 0),
    (0, 255, 0),
    (0, 255, 128),
    (0, 255, 255),
    (0, 128, 255),
    (0, 0, 255),
    (128, 0, 255),
    (255, 0, 255),
    (128, 128, 128)
]

dataDir = sys.argv[1]

FFMpegWriter = manimation.writers['ffmpeg']
metadata = dict(title='Movie Test')
writer = FFMpegWriter(fps=30, metadata=metadata)
cameraDirs = [join(dataDir, cameraDir) for cameraDir in listdir(dataDir) if isdir(join(dataDir,cameraDir))]
cameraIdx = 0
for cameraDir in progressbar.progressbar(cameraDirs):
    bmpFiles = [join(cameraDir,file) for file in listdir(cameraDir) if isfile(join(cameraDir,file)) and file.endswith('.bmp')]
    bmpFiles.sort()

    fig = plt.figure(figsize=(14,14))
    with writer.saving(fig, str(cameraIdx) + '.mp4', 100):
        for bmpFile in progressbar.progressbar(bmpFiles):
            plt.clf()
            #print('Processing file ' + bmpFile)
            txtFile = bmpFile.replace('.bmp', '.txt')

            with codecs.open(txtFile, 'r', 'utf-8') as txtFileStream:
                content = txtFileStream.readlines()

            content = [x.strip().replace('\ufeff', '') for x in content]
            if content[0]: #check that at least a row exists
                img = cv2.imread(bmpFile)
                h,w = img.shape[:2]

                bbs = []
                for element in content:
                        bb_info = element.split()
                        bb_width = float(bb_info[3])*float(w)
                        bb_height = float(bb_info[4])*float(h)
                        center_bb_x = float(bb_info[1])*float(w)
                        center_bb_y = float(bb_info[2])*float(h)
                        top_left_bb_x = center_bb_x - bb_width/2.0
                        top_left_bb_y = center_bb_y - bb_height/2.0
                        bb_class = int(bb_info[0])
                        bb = [int(top_left_bb_x), int(top_left_bb_y), int(bb_width), int(bb_height), bb_class]
                        #print(bb)
                        bbs.append(bb)

                for bb in bbs:
                    cv2.rectangle(img, (bb[0],bb[1]), (bb[0]+bb[2], bb[1]+bb[3]), colors[bb[4]], 2)

            plt.imshow(cv2.cvtColor(img, cv2.COLOR_BGR2RGB))
            writer.grab_frame()

            #plt.close(fig)

            #print('File ' + bmpFile + ' processed')
    cameraIdx += 1

#print(bmpFiles)
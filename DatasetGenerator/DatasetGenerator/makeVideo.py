import sys
import matplotlib
import numpy as np
import matplotlib.pyplot as plt
import matplotlib.animation as manimation
import cv2

from os import listdir
from os.path import isfile, join

dataDir = sys.argv[1]

FFMpegWriter = manimation.writers['ffmpeg']
metadata = dict(title='Movie Test')
writer = FFMpegWriter(fps=15, metadata=metadata)

bmpFiles = [join(dataDir,file) for file in listdir(dataDir) if isfile(join(dataDir,file)) and file.endswith(".bmp")]
bmpFiles.sort()

fig = plt.figure(figsize=(14,14))
with writer.saving(fig, "writer_test.mp4", 100):
    for bmpFile in bmpFiles:
        plt.clf()
        print("Processing file " + bmpFile)
        txtFile = bmpFile.replace(".bmp", ".txt")

        with open(txtFile) as txtFileStream:
            content = txtFileStream.readlines()

        content = [x.strip() for x in content] 
        
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
                bb = [int(top_left_bb_x), int(top_left_bb_y), int(bb_width), int(bb_height)]
                #print(bb)
                bbs.append(bb)

        for bb in bbs:
            cv2.rectangle(img, (bb[0],bb[1]), (bb[0]+bb[2], bb[1]+bb[3]), (255, 255, 0), 2)

        
        plt.imshow(cv2.cvtColor(img, cv2.COLOR_BGR2RGB))
        writer.grab_frame()

        #plt.close(fig)

        print("File " + bmpFile + " processed")


#print(bmpFiles)
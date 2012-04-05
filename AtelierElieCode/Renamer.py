#! /usr/bin/python

# To change this template, choose Tools | Templates
# and open the template in the editor.

__author__="Mark Liu"
__date__ ="$18/01/2010 1:17:17 PM$"

import os

if __name__ == "__main__":

    count = 0

    for fname in os.listdir("D:/AtelierElie/Items"):
        ext = os.path.splitext(fname)
        newname = "%.3d_%d%s" % ((count / 2), (count % 2), ext[1])

        print fname + " - " + newname
        count +=1
        os.rename("D:/AtelierElie/Items/" + fname, "D:/AtelierElie/Items/" + newname)



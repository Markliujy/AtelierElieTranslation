
import os

if __name__ == "__main__":

    count = 0

    for fname in os.listdir("D:/AtelierElie/Items"):
        ext = os.path.splitext(fname)
        newname = "%.3d_%d%s" % ((count / 2), (count % 2), ext[1])

        print fname + " - " + newname
        count +=1
        os.rename("D:/AtelierElie/Items/" + fname, "D:/AtelierElie/Items/" + newname)



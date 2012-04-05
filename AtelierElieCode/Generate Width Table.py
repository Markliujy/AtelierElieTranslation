from struct import unpack, pack
import binascii
import StringIO
from PIL import Image
import bitstring as bin

def genWidthTable(fontfile, width, height, space_width = 3):
	BaseImage = Image.open(fontfile)
	w, h = BaseImage.size
	no_w = w / width
	no_h = h / height


	Final = ""
	for imgy in xrange(no_h):
		for imgx in xrange(no_w):

			crop = (imgx * 12, imgy * 12, imgx * 12 + 12, imgy * 12 + 12)
			im = BaseImage.crop(crop)

			result = []

			for y in xrange(height):
				for x in reversed(xrange(width)):
					if im.getpixel((x, y)) == 0:
						result.append(x+1)
						break


			result.sort()
			if result == []:
				result.append(space_width)

			result = result.pop()
			#result += result % 2

                        print chr(imgy * no_w + imgx + 0x20) + " = " + str(result)

			Final += pack("B", result+1)

	return Final




if __name__ == "__main__":

        
        #f = open("../New/Font.bmp", "rb")
        f = open("../Fonttest.bmp", "rb")
	new = genWidthTable(f, 12, 12)
	f.close()

	f = open("../ARMips/Width_Table.bin", "wb")
	f.write(new)
	f.close()
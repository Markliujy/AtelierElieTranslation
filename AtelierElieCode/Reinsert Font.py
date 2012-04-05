from struct import unpack, pack
import binascii
import StringIO
from PIL import Image
import bitstring as bin

def _genBMP(tilebytes):

	tilebitarray = []
	for i in xrange(0, 12):
		tilebitarray.append([])
		for j in xrange(0, 12):
			tilebitarray[i].append(0)

	block_x = 0
	block_y = 0


	for byte in tilebytes:
		bits = _bin(unpack("<B", byte)[0])
		x = 0
		y = 0
		for i, bit in enumerate(bits):

			tilebitarray[block_y * 4 + 3-y][block_x * 2 + 1 - x] = bit
			y += 1
			if y > 3:
				y = 0
				x += 1

		block_x += 1
		if block_x > 5:
			block_x = 0
			block_y += 1

	for i, row in enumerate(tilebitarray):
		tilebitarray[i] = pack("B", int("".join(row[:8]), 2)) + pack("B", int("".join(row[8:]).ljust(8, '0'), 2)) + pack("H", 0)

	tilebitarray.reverse()

	bitmap = "".join(tilebitarray)



	bitmap = StringIO.StringIO(_header(len(bitmap), 12, 12) + bitmap)
	return Image.open(bitmap)

def remakeFont(file):
	BaseImage = Image.open(file)
	w, h = BaseImage.size
	no_w = w / 12
	no_h = h / 12

	
	Final = ""
	for imgy in xrange(no_h):
		for imgx in xrange(no_w):

			crop = (imgx * 12, imgy * 12, imgx * 12 + 12, imgy * 12 + 12)
			im = BaseImage.crop(crop)


			#for each row!
			for y in xrange(3):

				#for each column!
				for x in xrange(6):

					Final += str(im.getpixel((x * 2 + 1, y * 4 + 3)))
					Final += str(im.getpixel((x * 2 + 1, y * 4 + 2)))
					Final += str(im.getpixel((x * 2 + 1, y * 4 + 1)))
					Final += str(im.getpixel((x * 2 + 1, y * 4 + 0)))

					Final += str(im.getpixel((x * 2, y * 4 + 3)))
					Final += str(im.getpixel((x * 2, y * 4 + 2)))
					Final += str(im.getpixel((x * 2, y * 4 + 1)))
					Final += str(im.getpixel((x * 2, y * 4 + 0)))


	F = bin.BitString(bin = Final)
	F = ~F

	return F.tobytes()




if __name__ == "__main__":
	f = open("../Fonttest.bmp", "rb")

	new = remakeFont(f)
	f.close()

	f = open("../ARMips/New_Font.bin", "wb")
	f.write(new)
	f.close()

        print "Done."
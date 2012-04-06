from struct import unpack, pack
import binascii
import StringIO
from PIL import Image	# Requires PIL Library

def ConvertPointer(Pointer):
	'''
	
	Simple Pointer conversion
	
	'''
	return Pointer - 0x8000F800

def _bin(x, width = 8):
    return ''.join(str((x>>i)&1) for i in xrange(width-1,-1,-1))

def _header(length, width, height):
	header = "BM" + pack("L", length + 62) + pack("L", 0) + pack("L", 62)

	info_header = pack("L", 40) + pack("L", width) + pack("L", height) + \
				pack("H", 1) + pack("H", 1) + pack("L", 0) + pack("L", length) + \
                pack("L", 0) + pack("L", 0) + pack("L", 0) + pack("L", 0) + pack("L", 0)
	rgbquad = chr(0xff) + chr(0xff) + chr(0xff) + chr(0)

	return (header + info_header + rgbquad)

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


f = open("../Image Dump/SLPS_017.51", "rb")
f.seek(0xb2b58)
r = f.read(18)
f.close()
s = _genBMP(r)
s.show()


#f = open("../Dump/SLPS_017.51", "rb")
#
#FinalImgRows = []
##Pointers = []
#
#for i in range(0, 42):
#	pointer_offset = 0xBA31C + i * 4
#	f.seek(pointer_offset)
#	pointer = unpack("<L", f.read(4))[0]
#
#	if 1:
#		#Pointers.append(pointer)
#
#
#		#print str(hex(pointer_offset)) + ' ' + str(hex(pointer)) + ' ' + str(hex(ConvertPointer(pointer)))
#
#
#		#get character count...crappy thing.
#		f.seek(0xbaae4 + i * 4)
#		countp = ConvertPointer(unpack("<L", f.read(4))[0])
#		f.seek(countp)
#		count = 0
#		while 1:
#			byte = unpack("B", f.read(1))[0]
#			if byte == 0xff:
#				break
#			count += 1
#		print count
#
#		f.seek(ConvertPointer(pointer))
#
#		images = []
#
#		imagerow = Image.new("1", (12 * count, 12))
#
#		for y in xrange(0, count):
#			tilebytes = f.read(18)
#			images.append(_genBMP(tilebytes))
#
#		for x, image in enumerate(images):
#			imagerow.paste(image, (x * 12, 0))
#
#
#		FinalImgRows.append(imagerow)
#
#
#
#FinalImg = Image.new("1", (12*120, 12*42))
#
#for y, img in enumerate(FinalImgRows):
#	FinalImg.paste(img, (0, y*12))
#
#
#
#FinalImg.save("../Test2.bmp")
#
#f.close()
#
#print 'Done'






















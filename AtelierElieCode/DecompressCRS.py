from struct import unpack, pack
import binascii
import os
import sys

def Decompress(f):
	f.seek(0)
	format = unpack(">H", f.read(2))[0]
	if format != 0x4330:
		sys.exit("Not a CRS file!")


	OFFSET = unpack("<H", f.read(2))[0]
	LENGTH = unpack("<L", f.read(4))[0] + 8



	DECOMPRESSED = ''

	while f.tell() < LENGTH:
		
		byte = unpack("B", f.read(1))[0]

		#Direct Next Byte
		if byte == 0xd0:
			byte = unpack("B", f.read(1))[0]
			DECOMPRESSED  = DECOMPRESSED + pack("B", byte)

		#Repeat Next Byte
		elif byte == 0xd2:
			byte = unpack("B", f.read(1))[0]
			count = unpack("B", f.read(1))[0] + 4

			for i in xrange(count):
				DECOMPRESSED  = DECOMPRESSED + pack("B", byte)

		#Repeat Next Double Byte
		elif byte == 0xd3:
			byte = unpack(">H", f.read(2))[0]
			count = unpack("B", f.read(1))[0] +	3

			for i in xrange(count):
				DECOMPRESSED  = DECOMPRESSED + pack(">H", byte)

		#Repeat Next Triple Byte
		elif byte == 0xd4:

			byte = unpack(">H", f.read(2))[0]
			byte2 = unpack("B", f.read(1))[0]
			count = unpack("B", f.read(1))[0] +	2

			for i in xrange(count):
				DECOMPRESSED  = DECOMPRESSED + pack(">H", byte) + pack("B", byte2)


		#Repeat Next Four Bytes
		elif byte == 0xd6:
			byte = unpack(">L", f.read(4))[0]
			count = unpack("B", f.read(1))[0] +	2

			for i in xrange(count):
				DECOMPRESSED  = DECOMPRESSED + pack(">L", byte)

		#Set Offset Sliding
		elif byte == 0xd7:
			count = unpack("B", f.read(1))[0] +	3
			offset = -OFFSET
			for i in xrange(count):

				DECOMPRESSED  = DECOMPRESSED + DECOMPRESSED[offset:offset+1]


		elif byte == 0xd8:

			count = unpack("B", f.read(1))[0] +	3
			offset = -(OFFSET + 1)
			for i in xrange(count):
				DECOMPRESSED  = DECOMPRESSED + DECOMPRESSED[offset:offset+1]

		elif byte == 0xe2:

			count = unpack("B", f.read(1))[0] +	3
			offset = -(OFFSET - 1)
			for i in xrange(count):

				DECOMPRESSED  = DECOMPRESSED + DECOMPRESSED[offset:offset+1]

		elif byte == 0xe3:
			offset = -(unpack("B", f.read(1))[0] + 4)
			count = unpack("B", f.read(1))[0] +	4

			for i in xrange(count):
				DECOMPRESSED  = DECOMPRESSED + DECOMPRESSED[offset:offset+1]

		elif byte == 0xe6:

			offset = -(unpack("<H", f.read(2))[0] + 5)
			count = unpack("B", f.read(1))[0] +	5
			#print hex(offset), str(hex(len(DECOMPRESSED))), count

			for i in xrange(count):
				DECOMPRESSED  = DECOMPRESSED + DECOMPRESSED[offset:offset+1]

		else:
			DECOMPRESSED  = DECOMPRESSED + pack("B", byte)


	return DECOMPRESSED
{}

if __name__ == "__main__":
	
	f = open("../test2", "rb")
	decomp = Decompress(f)
	f.close()
	f = open("../decomp",  "wb")
	f.write(decomp)
	f.close()

	print 'Done'

#	Path="..//Unpacked2//"  # insert the path to the directory of interest
#
#	NewDir = "..//Unpacked2//"
#	if not os.path.exists(NewDir):
#		os.makedirs(NewDir)
#
#	dirList=os.listdir(Path)
#	for fname in dirList:
#		newname, ext = os.path.splitext(fname)
#		path = os.path.join(Path, fname)
#
#
#		if ext == '.C0':
#			f = open(path, "rb")
#			decomp = Decompress(f)
#
#
#			path = os.path.join(NewDir, newname + '.Bin')
#
#			f = open(path,  "wb")
#			f.write(decomp)
#			f.close()
#
#
#			print 'Written' + fname
#









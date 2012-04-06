
from DecompressCRS import Decompress
from struct import unpack, pack
from CreateTIM import CreateTIM
import binascii
import os
import sys
import StringIO


#------------------------------
#
#	FINAL_FILES:
#
#		(Prefix, Number, Suffix, Ext, Data)
#
#		Prefix	= List of prefixes - ie. directories to be joined with '/'
#		Number	= The number of the file block in the ARC file
#		Suffix	= For Palette/Graphic setting
#		Ext		= Extension of the file:
#
#				ARC = Auto unpacked recursively
#				C0	= Compressed Data
#				BIN = Uncompressed Data
#				HEAD= Header file
#
#		Data	= Data to be written to the file
#
#------------------------------



def UnpackPair(f, prefix, number, auto_decompress=False, create_TIMs=False):

	f.seek(0)
	format = unpack("<L", f.read(4))[0]
	accepted_formats = [0x30090, 0x20090]
	if format not in accepted_formats:
		sys.exit("Not a Palette/Graphic Pair File!")

	#Initialize
	RESULT = []
	NO = str(number).rjust(3, '0')
	PREFIX = prefix[:]
	HEADER_PREFIX = prefix[:]
	HEADER_PREFIX.append('Header/')
	

	#Unpack offsets of two file blocks
	File_Blocks = [unpack("<L", f.read(4))[0], unpack("<L", f.read(4))[0]]

	#First file block
	f.seek(3 * 4)

	extra_data = []
	if File_Blocks[0] % 2:
		extra_data.append('pal')
	if File_Blocks[1] % 2:
		extra_data.append('graphic')

	if extra_data != []:
		extra_data = ','.join(extra_data)


		RESULT.append((HEADER_PREFIX, NO, '', '.extra', extra_data))


	Palette_Header = f.read(4 * 2)

	RESULT.append((HEADER_PREFIX, NO, '_Palette', '.head', Palette_Header))

	length = File_Blocks[1] - File_Blocks[0]

	Palette = f.read(length)
	ext = '.Bin'
	if Palette[:2] == 'C0':
		if auto_decompress == True or create_TIMs == True:
			prefix = PREFIX[:]
			prefix.append('Compressed/')
			RESULT.append((prefix, NO, '_Palette', '.C0', Palette))

			Palette = Decompress(StringIO.StringIO(Palette))
		else:
			ext = '.C0'
	RESULT.append((PREFIX, NO, '_Palette', ext, Palette))


	#Second file block
	f.seek(12 + File_Blocks[1])
	Graphic_Header = f.read(4 * 2)
	RESULT.append((HEADER_PREFIX, NO, '_Graphic', '.head', Graphic_Header))

	Graphic = f.read()
	ext = '.Bin'
	if Graphic[:2] == 'C0':
		if auto_decompress == True or create_TIMs == True:
			prefix = PREFIX[:]
			prefix.append('Compressed/')
			RESULT.append((prefix, NO, '_Graphic', '.C0', Graphic))

			Graphic = Decompress(StringIO.StringIO(Graphic))
		else:
			ext = '.C0'
	RESULT.append((PREFIX, NO, '_Graphic', ext, Graphic))

	if create_TIMs == True:
		TIM = CreateTIM(Palette, Palette_Header, Graphic, Graphic_Header)

		TIM_prefix = []
		TIM_dir = ['TIMs/']
		prefix = PREFIX[:]
		for pre in prefix:
			TIM_prefix.append(pre.replace('/', '_'))
			
		TIM_prefix = ''.join(TIM_prefix)

		RESULT.append((TIM_dir, TIM_prefix, NO, '.TIM', TIM))


	return RESULT

def GetARCLength(f):

	f.seek(0)
	format = f.read(4)
	if format != 'ar01':
		sys.exit("Not an ARC file!")

	count = unpack("<L", f.read(4))[0]

	f.seek(0x8 + (count) * 4)
	length = unpack("<L", f.read(4))[0]

	return length

def UnpackARC(f, prefix=[], auto_decompress=False, create_TIMs=False):

	PREFIX = prefix

	f.seek(0)
	format = f.read(4)
	if format != 'ar01':
		sys.exit("Not an ARC file!")

	FILE_COUNT = unpack("<L", f.read(4))[0]
	FILE_OFFSETS = []
	FINAL_FILES = []

	for i in xrange(FILE_COUNT + 1):
		f.seek(0x8 + i * 4)
		Offset = unpack("<L", f.read(4))[0]
		FILE_OFFSETS.append(Offset)

		#print i, hex(Offset)

	for i in xrange(FILE_COUNT):
		FILE_START = FILE_OFFSETS[i]
		FILE_END = FILE_OFFSETS[i + 1]
		NO = str(i).rjust(3, '0')

		f.seek(FILE_START)

		Header = f.read(4)

		if Header == 'ar01':
			f.seek(FILE_START)
			length = FILE_END - FILE_START
			prefix = PREFIX[:]
			prefix.append("%03d" % i + '/')
			Data = UnpackARC(StringIO.StringIO(f.read(length)), prefix, auto_decompress, create_TIMs)

			FINAL_FILES.extend(Data)

		elif Header == pack("<L", 0x30090) or Header == pack("<L", 0x20090):

			f.seek(FILE_START)
			length = FILE_END - FILE_START
			Data = UnpackPair(StringIO.StringIO(f.read(length)), PREFIX, i, auto_decompress, create_TIMs)
			FINAL_FILES.extend(Data)

		else:
			#print 'Error: Out of consideration:' + str(hex(unpack(">L", Header)[0]))
			try:
				print 'Unknown Archived File Format: ' + str(hex(unpack(">L", Header)[0])), "File:", '/'.join(PREFIX) + NO + '.Bin' + ' Pos:' + str(hex(FILE_START))
			except:
				print 'Unknown Archived File Format', "File:", '/'.join(PREFIX) + '/' + NO + '.Bin'
			f.seek(FILE_START)
			length = FILE_END - FILE_START
			Data = f.read(length)

			FINAL_FILES.append((prefix, NO, '', '.Bin', Data))


	return FINAL_FILES

def GetPairLength(f):
	f.seek(8)
	offset = unpack("<L", f.read(4))[0]
	f.seek(12 + offset + 12)
	return unpack("<L", f.read(4))[0] + f.tell()

def UnpackFile(f, auto_decompress=False, create_TIMs=False):
	"""
	Unpacks file according to its format:

		ARC
		C0
		090030 (Pair)
		Unknowns
	"""

	#Initialize
	RESULT = []
	f.seek(0)

	#Format switch
	format = f.read(4)
	if format == 'ar01':

		#Is ARC file - Unpack ARC file
		print 'Unpacking ARC file...'
		f.seek(0)
		Binary = f.read()
		Position = 0

		#If there is more data in the file - Seek and unpack
		if GetARCLength(f) < len(Binary):

			#Endless loop while still in file
			while Position < len(Binary):
				f.seek(Position)

				#Loop to check for repeating 00s
				while Position < len(Binary):
					if unpack("B", f.read(1))[0] != 0:
						break
					Position += 1

				#Double-check not at EOF
				if Position >= len(Binary): break

				f.seek(Position)

				#If block after is ARC - just dump it
				if f.read(4) == 'ar01':
					pos = Position

				#If block isn't ARC - seek next ARC, then seek/dump block inbetween
				else:
					pos = Binary[Position:].find('ar01')

					#No more ARC files - Dump remaining
					if pos == -1:
						prefix = ["0x%08X/" % Position]
						RESULT.append((prefix, 'Data', '', '.bin', Binary[Position:]))
						print "Error Check - Not EOF:", hex(Position), hex(len(Binary))
						break
					#Still ARC files - Dump block inbetween
					else:
						prefix = ["0x%08X/" % Position]
						RESULT.append((prefix, 'Data', '', '.bin', Binary[Position:pos + Position]))

					#Found more ARC files
					f.seek(Position)
					print "Error Check - Gaps not filled with 00:", hex(Position), hex(pos + Position), binascii.hexlify(f.read(4))
					pos += Position



				f.seek(pos)
				data = StringIO.StringIO(f.read())
				length = GetARCLength(data)

				prefix = ["0x%08X/" % pos]
				
				data = UnpackARC(data, prefix, auto_decompress=auto_decompress, create_TIMs=create_TIMs)
				RESULT.extend(data)

				Position = pos + length
				#print hex(Position), hex(length), hex(pos)

		#Only the ARC file in the file - Simple Unpack
		else:
			RESULT.extend(UnpackARC(f, auto_decompress=auto_decompress, create_TIMs=create_TIMs))



	elif format == pack("<L", 0x30090) or format == pack("<L", 0x20090):
		print 'Unpacking Unheadered ARC file...'
		f.seek(0)
		Position = 0
		Binary = f.read()
		
		while Position < len(Binary):
			f.seek(Position)

			while Position < len(Binary):
				if unpack("B", f.read(1))[0] != 0:
					break
				Position += 1

			if Position >= len(Binary): break

			f.seek(Position)

			format = unpack("<L", f.read(4))[0]
			if format in (0x30090, 0x20090):
				pos = Position
			else:
				pos1 = Binary[Position:].find(pack("<L", 0x30090))
				pos2 = Binary[Position:].find(pack("<L", 0x20090))

				if pos1 == -1 and pos2 == -1:
					break
				elif pos1 == -1:
					pos = pos2
				elif pos2 == -1:
					pos = pos1
				elif pos1 < pos2:
					pos = pos1
				else:
					pos = pos2

				f.seek(Position)
				print "Error Check - Gaps not filled with 00:", hex(Position), hex(pos + Position), binascii.hexlify(f.read(4))
				pos += Position

			f.seek(pos)
			data = StringIO.StringIO(f.read())
			length = GetPairLength(data)

			prefix = ["0x%08X/" % pos]

			data = UnpackPair(data, prefix, 'Data', auto_decompress=auto_decompress, create_TIMs=create_TIMs)
			RESULT.extend(data)

			Position = pos + length

		if Position < len(Binary):
				f.seek(Position)
				while Position < len(Binary):
					if unpack("B", f.read(1))[0] != 0:
						break
					Position += 1
				if Position < len(Binary):
					f.seek(Position)
					print "Error Check - Not EOF:", hex(Position), hex(len(Binary)), binascii.hexlify(f.read(4))


	elif format[0:2] == 'C0':
		print 'Unpacking Compressed file...'
		prefix, number, suffix, ext = ('', '1', '', '.bin')
		Data = Decompress(f)
		RESULT.append((prefix, number, suffix, ext, Data))

	else:
		print 'Error: Unknown file.'

		


		
	print 'Done.'
	
	return RESULT

if __name__ == "__main__":
	
	#		files = UnpackARC(f)
	#		length = files.pop()
	#
	#		f.seek(0)
	#
	#
	#		print len(Binary), length
	#		if len(Binary) > length:
	#			print 'test'
	
	
	#BasePath = "D:/Atelier Marie/Dump/"
	#BaseExtractPath = "D:/Atelier Marie/Unpacked"
	#BaseExtractPath = "../Unpacked"
	#BasePath = "../Dump/"
	BaseExtractPath = "../Original Files/Unpacked/"
	BasePath = "../Image Dump/"
	
	
	

	FolderList = os.listdir(BasePath)
	FolderList = ["OP"]
	
	for Folder in FolderList:
	
		##FileList = ["E_21.AR2"]
		FileList = os.listdir(BasePath + Folder)
		
		for FileName in FileList:
			NewPath = os.path.join(BaseExtractPath, Folder, os.path.splitext(FileName)[0])
	
			print "Extracting " + FileName + "..."
	
			f = open(BasePath + Folder + "/" + FileName, "rb")
			files = UnpackFile(f, create_TIMs=True)
		
		
			Dir = NewPath + "/"
		
			if not os.path.exists(Dir):
				os.makedirs(Dir)
		
		
			for File in files:
	
				(prefix, number, suffix, ext, Data) = File
		
				path = os.path.join(Dir, *prefix)
		
		
				if not os.path.exists(path):
					os.makedirs(path)
		
				fname = number + suffix + ext
		
		
				f = open(path + fname, "wb")
				f.write(Data)
				f.close()
	
			print ""










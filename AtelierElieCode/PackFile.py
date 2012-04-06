from RecompressCRS import Compress
from struct import unpack, pack
import binascii
import os
import sys
import StringIO


def ripTIM(f):
	'''
	
	Rip Graphic out of TIM
	
	'''
	f.seek(8)
	clut_len = unpack("<L", f.read(4))[0]
	

	length_offset = clut_len + 8
	f.seek(length_offset)
	length = unpack("<L", f.read(4))[0]

	data_offset = length_offset + 12

	f.seek(data_offset)
        print "Ripping...Length=" + str(length)
	return f.read(length)


def packPair(pal, graphic, pal_head, graphic_head, extra):
	'''
	
	Repack pair of Graphic and Palette
	
	'''
	data = pal_head + pal

	extrapal = 0
	extragraphic = 0

	if extra:
		extra_data = extra.split(',')
		
		if 'pal' in extra_data:
			extrapal = 1
		
		if 'graphic' in extra_data:
			extragraphic = 1




	if (len(data) + 12) % 4:
		for i in xrange(4 - (len(data) % 4)):
			data += pack("B", 0)

	if pal[0:2] == 'C0':
		head = pack("<L", 0x30090) + pack("<L", 0x8 + extrapal) + pack("<L", len(data) + extragraphic)
	else:
		head = pack("<L", 0x20090) + pack("<L", 0x8 + extrapal) + pack("<L", len(data) + extragraphic)

	

	return head + data + graphic_head + graphic

def packARC(Base_Dir, Prefix = []):
	'''
	
	Pack Archives in Directory
	
	'''
	Dir = os.path.join(Base_Dir, *Prefix)
	
	Files = os.listdir(Dir)
	Final = []
	print Dir
	File_Count = 0

	#Loop through all files
	while 1:
		Number = "%03d" % File_Count

		#ARC file directory
		if Number in Files:
			path = os.path.join(Dir, Number) + '/'
			prefix = Prefix[:]
			prefix.append(Number)

			Final.append(packARC(Base_Dir, prefix))
			File_Count += 1

		#Pair
		elif Number+"_Graphic.Bin" in Files:

			# Get Palette
			# NO CHANGE POSSIBLE TO PALETTE
			pal = os.path.join(Dir, 'Compressed', Number+"_Palette.C0")
			if os.path.exists(pal):
				pal = open(pal, "rb")
			else:
				pal = os.path.join(Dir, Number+"_Palette.Bin")
				pal = open(pal, "rb")

			graphic = os.path.join(Dir, 'Compressed', Number+"_Graphic.C0")
			
			
			New_Tim = Prefix[:]
			New_Tim.append(Number+'.TIM')
			New_Tim = '_'.join(New_Tim)
			New_Tim = os.path.join(Base_Dir, 'New_Tims', New_Tim)

			# If New TIM file exists, use that instead for the Graphic and compress
			if os.path.exists(New_Tim):
				print New_Tim
				tim = open(New_Tim, 'rb')
				graphic = ripTIM(tim)
				tim.close()
				compress = Compress(graphic)
				if len(compress) < len(graphic):
					graphic = compress
				
				graphic = StringIO.StringIO(graphic)
			
			# Else if Compressed Graphic exists, use that		
			elif os.path.exists(graphic):
				graphic = open(graphic, "rb")
			else:
				graphic = os.path.join(Dir, Number+"_Graphic.Bin")
				graphic = open(graphic, "rb")

			pal_header = os.path.join(Dir, 'Header', Number + "_Palette.head")
			pal_header = open(pal_header, "rb")
			graphic_header = os.path.join(Dir, 'Header', Number + "_Graphic.head")
			graphic_header = open(graphic_header, "rb")

			pal = pal.read()
			graphic = graphic.read()
			pal_header = pal_header.read()
			graphic_header = graphic_header.read()

			extra_test = os.path.join(Dir, 'Header', Number + ".extra")
			if os.path.exists(extra_test):
				f = open(extra_test, 'rb')
				extra = f.read()
				f.close()
			else:
				extra = None

			#Add pair block
			Final.append(packPair(pal, graphic, pal_header, graphic_header, extra))

			File_Count += 1

		#Unknown file
		elif Number+".Bin" in Files:
			path = os.path.join(Dir, Number + ".Bin")
			Final.append(open(path, "rb").read())
			File_Count += 1
			
		else:
			break

	Offsets = [4 * (2 + File_Count + 1)]
	pos = 4 * (2 + File_Count + 1)
	print "ARC Blocks:"
	for i, block in enumerate(Final):
		pos += len(block)
		
		Offsets.append(pos)
		
		
	Final = ''.join(Final)

	Final_length = (len(Final) + 4 * (2 + File_Count + 1))
	if Final_length % 0x4:
		for i in xrange(0x10 - (Final_length % 0x10)):
			Final += pack("B", 0)
		Offsets[-1] += 0x10 - (Final_length % 0x10)

	Header = 'ar01' + pack("<L", File_Count)
	for offset in Offsets:
		Header += pack("<L", offset)
		print "	Block - %08X" % (offset)


	return Header + Final





def packFiles(Dir):
	'''
	
	Pack File depending on Type
	
	'''
	Final = ''

	Files = os.listdir(Dir)

	#If simple unpacked ARC
	if 'Header' in Files:
		return packARC(Dir)
	
	#If simple uncompressed file
	elif 'Data.bin' in Files:
		if len(Files) > 1:
			print 'More files then expected:'
			for file in Files:
				if file != 'Data.Bin': print file

		return Compress(open(os.path.join(Dir, 'Data.Bin')))

	#If simple Pair - haven't seen it?
	elif '000_Graphic.Bin' in Files:
		pass

	#If multi file packed file
	else:
		pass


#	for file in Files:
#		print file

	print 'Done'







if __name__ == "__main__":
	
	DIR = "../Translated Files/Unpacked/BOOT/BOOT_T/"
	NewFile = "../Translated Files/Final/BOOT/BOOT_T.ARC"
	f = open(NewFile, 'wb')
	result = packFiles(DIR)
	f.write(result)
	f.close()

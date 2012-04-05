from struct import unpack, pack
import binascii
import os
import sys
import math
import StringIO

def CreateTIM(Palette, Palette_Header, Graphic, Graphic_Header):
	TIM = ""
	
	#TIM Header
	TIM += pack(">L", 0x10000000)

	Colours = unpack("<H", Palette_Header[4:6])[0]
	BPP = int(math.log(Colours, 2))
	if BPP == 4:
		TIM += pack(">L", 0x08000000)
	elif BPP == 8:
		TIM += pack(">L", 0x09000000)

	TIM += pack("<L", len(Palette) + 12)
	TIM += Palette_Header[0:4]
	TIM += pack("<H", Colours)
	TIM += pack("<H", int(len(Palette) / Colours))

	TIM += Palette

	TIM += pack("<L", len(Graphic) + 12)
	TIM += Graphic_Header[0:8]

	TIM += Graphic


	return TIM

if __name__ == "__main__":

	DIR = "../Unpacked2/"
	HEADER = "../Unpacked2/Headers/"
	NEWDIR = "../Unpacked2/TIM"


	FILES = []

	if not os.path.exists(NEWDIR):
		os.makedirs(NEWDIR)

	dirList=os.listdir(DIR)
	for fname in dirList:

		name, ext = os.path.splitext(fname)

		if os.path.isfile(DIR + fname):
			number = name.split("_")[0]

			#needs more error checking

			if number not in FILES:
				FILES.append(number)

	for number in FILES:

		try:
			Palette = open(os.path.join(DIR, number + '_A.Bin'), "rb").read()
			Graphic = open(os.path.join(DIR, number + '_B.Bin'), "rb").read()
			PaletteHead = open(os.path.join(HEADER, 'Header_' + number + '_A.Bin'), "rb").read()
			GraphicHead = open(os.path.join(HEADER, 'Header_' + number + '_B.Bin'), "rb").read()


			TIM = CreateTIM(Palette, PaletteHead, Graphic, GraphicHead)

			f = open(os.path.join(NEWDIR, number + '.TIM'), "wb")
			f.write(TIM)
			f.close()
		except:
			pass



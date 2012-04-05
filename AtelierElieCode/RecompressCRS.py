from struct import unpack, pack
import binascii
import os
import time
import re
import sys
import types

def print_timing(func):
    def wrapper(*arg):
        t1 = time.time()
        res = func(*arg)
        t2 = time.time()
        print '%s took %0.3f ms' % (func.func_name, (t2-t1)*1000.0)
        return res
    return wrapper

@print_timing
def Compress(BINARY):
	FINAL = []
	LENGTH = len(BINARY)
	SET_OFFSET = []
	COMMAND_BYTES = [0xD0, 0xD2, 0xD3, 0xD4, 0xD6, 0xD7, 0xD8, 0xE2, 0xE3, 0xE6]
	REGEX_BYTES = ['^', '$', '+', '*', '?', '.', '|', '(', ')', '{', '}', '[', ']', '\\']

	i = 0

	print "Compressing...Length = " + str(LENGTH)

	#	-----------------
	#	Reading Loop
	#	-----------------

	while i < LENGTH:
		#Read BackBuffer to limit of 0xFF => Furthest back Sliding Door can go
		BackBufferLimit = i - 0xFFFF
		if BackBufferLimit < 0:
			BackBufferLimit = 0

		BackBuffer = BINARY[BackBufferLimit:i]

		Choices = []
		print i
		#	-----------------
		#	Sliding Window Test
		#	-----------------
		Byte = BINARY[i:i+1]

		if Byte in REGEX_BYTES:
			Byte = "\\" + Byte


		SearchResults = re.finditer(r"" + Byte, BackBuffer)
		
		result_pos = 0



		for result in SearchResults:
			j = result.start()
			if j > result_pos:

				Count = 0
				while 1:
					if BINARY[i + Count:i + Count + 1] != BINARY[j + Count:j + Count + 1]:
						break
					Count += 1

				Offset = i - j

				result_pos = j + Count

				if Count > 2:
					#High Move
					if Offset > 0xFF + 5:
						if Count > 4:
							if Count >= 0xFF:
								Count = 0xFF
							Gain = Count - 4
							Choices.append((0, Offset, Count, Gain))
					#Norm Move, Norm Count
					if Offset < 0xFF and Offset > 4:
						if Count > 3:
							if Count >= 0xFF:
								Count = 0xFF
							Gain = Count - 3
							Choices.append((4, Offset, Count, Gain))
		#	-----------------
		#	End Sliding Window Test
		#	-----------------

		#	-----------------
		#	RLE Test
		#	-----------------

		for j in xrange(1, 5):
			Bytes = BINARY[i:i + j]

			Count = 0
			while 1:
				if Bytes != BINARY[i + Count:i + (Count + 1)] or Count >= 255:
					break
				Count += j

			if Count > (2 + j):
				Gain = Count - (2 + j)
				Choices.append((4 + j, Bytes, Count / j, Gain))
		#	-----------------
		#	End RLE Test
		#	-----------------

		#	-----------------
		#	Write Data
		#	-----------------

		#If Compression is possible and effective
		if Choices != []:
			#Sort Choices
			Choices.sort(lambda x, y: cmp(x[3],y[3]))

			Data = Choices.pop()
			Choice, Offset, Count, Gain = Data

			FINAL.append(Data)
			if Choice == 5:
				Count *=  1
			elif Choice == 6:
				Count *=  2
			elif Choice == 7:
				Count *=  3
			elif Choice == 8:
				Count *=  4
			elif Choice == 0 or Choice == 4:
				SET_OFFSET.append(Offset)

			i += Count

		#No Compression Possible
		else:
			Byte = BINARY[i:i + 1]
			Hex = unpack("B", Byte)[0]
			if Hex in COMMAND_BYTES:
				FINAL.append(pack("B", 0xD0) + Byte)
			else:
				FINAL.append(Byte)
			i += 1

		#	-----------------
		#	Write Data
		#	-----------------

		if i % 0x1000 == 0:
			print hex(i/0x1000)

	#	-----------------
	#	End Reading Loop
	#	-----------------

	#	-----------------
	#	SET_OFFSET
	#	-----------------

	counts = set(item for item in SET_OFFSET)
	counts = [(item, SET_OFFSET.count(item)) for item in counts]
	counts.append((0, 0))

	offsets, counts = zip(*counts)
	offsets = list(offsets)

	SET_OFFSET = []
	for i, offset in enumerate(offsets):
		count = counts[i]
		if offset + 1 in offsets:
			count += counts[offsets.index(offset + 1)]
		if offset - 1 in offsets:
			count += counts[offsets.index(offset - 1)]

		SET_OFFSET.append((offset, count))


	SET_OFFSET.sort(lambda x, y: cmp(x[1],y[1]))
	SET_OFFSET, _ = SET_OFFSET.pop()

	

	#	-----------------
	#	End SET_OFFSET
	#	-----------------

	#	-----------------
	#	2nd Pass - To optimize SET_OFFSET
	#	-----------------
	RESULT = ''
	for data in FINAL:
		if type(data) == types.TupleType:
			#	-----------------
			#	Switch on Compression Type:
			#
			#	Format: (Type, Offset, Count, Gain)
			#
			#	0: E6 3 Byte Sliding Window
			#	1: D7 Set Sliding Window
			#	2: D8 Set+1 Sliding Window
			#	3: E2 Set-1 Sliding Window
			#	4: E3 2 Byte Sliding Window
			#	5: D2 1 Byte RLE
			#	6: D3 2 Byte RLE
			#	7: D4 3 Byte RLE
			#	8: D6 4 Byte RLE
			#
			#	-----------------
			Choice, Offset, Count, Gain = data
			Gain -= 1

			if Choice in [0, 4] and Offset == SET_OFFSET:
				Data = pack("B", 0xD7) + pack("B", Count - 3)
			if Choice in [0, 4] and Offset == SET_OFFSET + 1:
				Data = pack("B", 0xD8) + pack("B", Count - 3)
			if Choice in [0, 4] and Offset == SET_OFFSET - 1:
				Data = pack("B", 0xE2) + pack("B", Count - 3)
			elif Choice == 0:
				Data = pack("B", 0xE6) + pack("<H", Offset - 5) + pack("B", Gain)
#			elif Choice == 1:
#				Data = pack("B", 0xD7) + pack("B", Gain)
#			elif Choice == 2:
#				Data = pack("B", 0xD8) + pack("B", Gain)
#			elif Choice == 3:
#				Data = pack("B", 0xE2) + pack("B", Gain)
			elif Choice == 4:
				Data = pack("B", 0xE3) + pack("B", Offset - 4) + pack("B", Gain)
			elif Choice == 5:
				Data = pack("B", 0xD2) + Offset + pack("B", Count - 4)
			elif Choice == 6:
				Data = pack("B", 0xD3) + Offset + pack("B", Count - 3)
			elif Choice == 7:
				Data = pack("B", 0xD4) + Offset + pack("B", Count - 2)
			elif Choice == 8:
				Data = pack("B", 0xD6) + Offset + pack("B", Count - 2)


			RESULT = RESULT + Data

		else:
			RESULT = RESULT + data

	#	-----------------
	#	End 2nd Pass
	#	-----------------

	return CompressionHeader(RESULT, SET_OFFSET) + RESULT

def CompressionHeader(Data, Offset):
	return pack("B", 0x43) + pack("B", 0x30) + pack("<H", Offset) + pack("<L", len(Data))



if __name__ == "__main__":
	f = open("D:/AtelierElie/New/Unpacked/OV/EV_001.CRS", "rb")

	BINARY = f.read()
	f.close()

	Data = Compress(BINARY)

	f = open("D:/AtelierElie/New/Packed/OV/EV_001.CRS", "wb")
	f.write(Data)
	f.close()

	print 'Done'


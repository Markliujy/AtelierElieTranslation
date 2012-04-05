from struct import unpack, pack
import binascii
import StringIO
from PIL import Image
import bitstring as bin

def HexToByte( hexStr ):
    """
    Convert a string hex byte values into a byte string. The Hex Byte values may
    or may not be space separated.
    """
    # The list comprehension implementation is fractionally slower in this case
    #
    #    hexStr = ''.join( hexStr.split(" ") )
    #    return ''.join( ["%c" % chr( int ( hexStr[i:i+2],16 ) ) \
    #                                   for i in range(0, len( hexStr ), 2) ] )

    bytes = []

    hexStr = ''.join( hexStr.split(" ") )

    for i in range(0, len(hexStr), 2):
        bytes.append( chr( int (hexStr[i:i+2], 16 ) ) )

    return ''.join( bytes )


def remakeFont(file):
	BaseImage = Image.open(file)
	w, h = BaseImage.size
	no_w = w / 8
	no_h = h / 12


	Final = ""
	for imgy in xrange(no_h):
		for imgx in xrange(no_w):
                    for y in xrange(12):
				for x in xrange(8):
                                    if (BaseImage.getpixel((x + imgx * 8, y + imgy * 12)) == 1):
                                        Final += "0"
                                    else:
                                        Final += "1"


        print Final

	return HexToByte(Final)




if __name__ == "__main__":
	f = open("../New/Font.bmp", "rb")

	new = remakeFont(f)
	f.close()

	f = open("../ARMips/New_Font.bin", "wb")
	f.write(new)
	f.close()

        print "Done."
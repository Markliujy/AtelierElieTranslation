
    FEIDIAN: The Freaking Easy, Indispensable Dot-Image formAt coNverter
    Copyright (C) 2003,2004 Derrick Sobodash
    Version: 0.90a
    Web    : http://feidian.sourceforge.net/
    E-mail : d-xiansheng at users dot sourceforge dot net

-------------------------------------------------------------------------------

Please read changes.txt for information about newly supported commands.

For reference, this README is really more of a case study to give the user a
feel for how this program can actually be applied to real scenarios. I think
something like this helps people to see the actual value of a program a lot
more than just a list of commands would.

-------------------------------------------------------------------------------
 . : Contents : .
-------------------------------------------------------------------------------
 i.... What is FEIDIAN?
 ii... Setting up
 iii.. Standard bitplane font format
 iv... Creating a custom tile definition
 v.... Extracting a font
 vi... Inserting a font
 vii.. Color tiles/Multiple planes
 viii. Scaling and padding
 ix... Building a dual-letter font
 x.... BCR (Binary Character Recognition)
 xi... Other Notes
 xii.. License

-------------------------------------------------------------------------------
 i. What is FEIDIAN?
-------------------------------------------------------------------------------
So you've downloaded FEIDIAN. I'm already impressed. Nobody EVER uses anything
I write, so I'm just, wow, I'm impressed.

So what is FEIDIAN? FEIDIAN is a tool for converting between bitmap and
bitplane graphic formats at any size whatsoever. If you're new to the concept
of bitplane graphics, basically it works like this. You have a tile. It can be
any width and any height. And tiles of this size are repeated again and again
for as long as whatever is getting stored is being stored.

-------------------------------------------------------------------------------
 iii. Setting up
-------------------------------------------------------------------------------
FEIDIAN is fairly easy to set up, it just requires PHP. For help with
downloading PHP and how to add it to your system path, please refer to
the install.txt included in this archive.

Once PHP is read, there's still a few more options for you to configure. These
are all stored in settings.php. Open the file up in your favorite text editor
(WARNING: Notepad will eat the Unix line breaks making the file one long line,
you should use Wordpad if nothing else is available).

The first option is a warning and is enabled by default. It will alert you if
your dumping pattern could produce an invalid graphic, and halt dumping. It is
strongly recommended you leave this enabled, but to disable it, just comment
out the line (put "//" at the start of the line to comment it out).

Next is your preferred graphic format. FEIDIAN can work with either BMP
(Windows Bitmap) or XPM (X Pixmap) graphic formats. BMP is enabled by default.
If you use an OS where a bitmap editor is not available, you can comment this
line out and uncomment the one below it to use XPM instead.

The next two options are the default name and copyright string of system fonts
(BDF or FD/FON) produced by FEIDIAN. You can change these to anything you like.
Just type the new text between the quotation marks, and if you want to use any
quotation marks within your text, put a "\" before them.

There's a load of options for Binary Character Recognition (BCR) as well. The
first is what character to write when a tile isn't matched. It's recommended
if matching dual byte tiles, you use a dual-byte character (or two spaces) to
preserve the columns in the matched file.

$char_bytes is the number of bytes used for the text encoding in your table
file. If you're feeding in SJIS, you'd write "2" since SJIS is two bytes. If
using Unicode, you'd enter "5", since Unicode is five bytes. All characters in
the file must follow this encoding. Do not try to double up encodings in a
file (example: SJIS, Unicode and EUC-KR), this will cause corrupt output.

The next up is hashing options for tile comparison. The shorter your hash, the
faster matching will be. The longer your hash, the less chance of mismatched
tiles. With no hash, there's no chance of errors, but PHP arrays are known to
be glitchy when containing too long an entry. It is recommended you use a hash,
since the chance of error is around 1:16,980,000 -- fairly good odds when
you're giving it less than 3000 characters. You can only declare ONE hashing
method. This means all expect the one you are using should be commented out.

If you must use no hash, there's an option for Gzipping the tiles. This is
your best option for hash-free matching. It can also be used with the hashes
themselves, though it probably would not improve performance (any time saved
by the smaller tiles would be wasted since it takes time to Gzip them). Just
uncomment the line to enable Gzipping.

The last option is for custom tiles. By uncommenting it, you can use the
high-ascii European characters in your tile definitions. This is commented out
by default, so if you do use them in a definition, alert people to uncomment
this line before using your tile definition!

-------------------------------------------------------------------------------
 iii. Standard bitplane font format
-------------------------------------------------------------------------------
So let's take a VERY common tile size, 8x16. This means the tile is 8 bits
wide and 16 bits tall. Since a byte is 8 bits, this tile would be 16 bytes in
the file ((8*16)/8). Simple, yes? I thought so.

So far, we're still in the realm of things every other tile tool on the planet
can do -- that being multiples of 8. People like working with bytes because
they're fast and fairly regular. When you get into tiles that do not store
their rows in complete bytes, it can get a little hairy.

Say you have a file that is 12x14. That means there are 12 bits to a line. Good
luck finding a tile editor that will handle that. If you find one, send the
author a hearty thank you because virtually NOBODY fells like adding support
for such widths, even though it's INCREDIBLY common.

So how are these 12 bits being stored? Well, most commonly, multiple bytes are
just hooked together in a long, wrapping bit string. Let's pretend our first
byte is "A," our second is "B," and so on. A will be repeated 8 times for each
bit in A.

                               AAAAAAAABBBB
                               BBBBCCCCCCCC
                               DDDDDDDDEEEE
                               EEEEFFFFFFFF
                               ...

And so on. This means we have one full byte on the first line, the upper nibble
(4 bits of a byte) on that line, the lower nibble on the next line, then the
next byte. Then the pattern repeats.

-------------------------------------------------------------------------------
 iv. Creating a custom tile definition
-------------------------------------------------------------------------------
So FEIDIAN will handle the above example, since it's all one after another. But
what if you ran across a REALLY queer game that used something 12x12, but did
not even bother keeping the bits in that logical an order (*cough* NCS/Masaya's
Der Langrisser *cough*). In Der Langrisser, your bits are like this:

                               AAAAAAAABBBB
                               CCCCCCCCBBBB
                               DDDDDDDDEEEE
                               FFFFFFFFEEEE
                               ...

Well uh-oh. FEIDIAN is not a crystal ball, so it can't see every possible tile
pattern imaginable. But it DOES allow the user to create their own custom tile
patterns! This is quite possibly the most useful feature in FEIDIAN.

How does one do this? First off, download any sample template off the FEIDIAN
webpage (http://feidian.sourceforge.net/) and pop it open in Notepad, vi,
or if you're scared to use a computer, pico. This file is heavily commented
(a comment is a line beginning with //) and should give you all the info you
need. But in case it's not enough, I'll walk you through making one from
scratch.

First off, a tile definition needs to say how tall and how wide a tile is.
There are two variables in the file to do this: $tile_width and $tile_height
(in PHP, a variable always has $ before the name). So for our 12x12 tile
example, we would write:
  $tile_width  = 12;
  $tile_height = 12;
Easy! Next, we need to define out above pattern. Well let's think carefully, so
we can keep our pattern as lean as possible. In the above example, the pattern
repeats itself after byte C. So that means, we only need to define up to that.
  $plane1  = "AAAAAAAABBBB
              CCCCCCCCBBBB";
Voila. You have just defined your byte pattern. That was the hardest part! The
only thing left is to say how many bytes are needed for the pattern. In this
case, it's three bytes (A, B and C). So we'll put:
  $pat_size    = 3;

You're all done! Save your file with a meaningful name with the extension .php.
(For this example, I chose dl.php since it's Der Langrisser). Also make sure
the first line of your file is "<?" and the last is "?>" Otherwise, FEIDIAN
will crash if you try to use it.

So, easy enough so far? I hope so.

-------------------------------------------------------------------------------
 v. Extracting a font
-------------------------------------------------------------------------------
Now it's time to actually dump the font. You need to know where it begins at
in the ROM. This I cannot help you with, you'll need to either debug and find
where it's loading from, or use your eyeballs and a lot of patience! Neill
Corlett's "Nana" tile viewer can be very helpful for this, but it can't show
anything that's not a multiple of 8 in 1bpp. There's a trick though, to help
you with 12x12 tiles. Since the width is 12, the nearest multiple of 8 would
be 24. If you set the width to 24, it should help you find your tiles (though
it will look broken).

Find the offset in the file where your tiles begin and write it down. It should
be something like 0xdeadbeef (poor example, but dead beef is my favorite kind).
Armed with the offset, your tile size, or your custom definition, you are
ready to use FEIDIAN.

FEIDIAN is capable of three modes. Insertion, ripping, and creating custom
tile sets using a list of letters (more on this later!) The modes are:

  r - rips bitplane to bitmap
  i - injects bitplane to bitmap
  d - creates dual-letter set.

There's also a prefix, "c," for when you're using a custom set.

If we were going with our very first example, which was not a custom tile, we
would run FEIDIAN like this:

  feidian -r 12,12,16,16,0xdeadbeef ourrom.rom output

This is a VERY simple command line. Let's go through its components.

  width,height,rows,columns,offset

The width is your tile width, 12. The height is also 12 for this example.
The 16 is how many tiles should be in a column. The next 16 is how many rows
of tiles you want to dump. The above example would dump the first 256 12x12
tiles located at 0xdeadbeef in ourrom.rom. It would output a bitmap of them
to output_12x12.bmp.

Easy enough? Kick ass.

Now for our Der Langrisser example, it's a bit different.

  feidian -cr dl,16,16,0x11814c derlang.smc output

Notice the -cr? That means we're using a custom tile definition. Out command
line is the same as before, except now, we replace the tile width and height
with out custom definition (which we saved as dl.php). Your definition should
be in the /tiles/ folder of the FEIDIAN directory.

The offset shown is actually the real offset for Der Langrisser's 12x12 tiles.
So have you followed the walkthrough so far, this is actually a functional
command line you could use to dump that font.

-------------------------------------------------------------------------------
 vi. Inserting a font
-------------------------------------------------------------------------------
Ok. So now you're a master of making your own tile definitions and dumping
fonts with FEIDIAN. What else can it do? Well, let's take that nifty
output_12x12.bmp we just made an crack it open in Paintbrush. Flood the thing
with black and bust open the text tool (white text, black background). Make
a big textbox and type something fun, like

  I ABSOLUTELY DESPISE HOW ANNOYING
  THE STUPID SSH SYSTEM IS FOR
  UPLOADING NEW FILES.

Now, let's try this.

  feidian -ci dl,16,16,0x11814c output_12x12.bmp derlang.smc

Congratulations, you have just replaced all those first tiles in Der Langrisser
with the above text. This is totally useless, since you can't see it in the
game right now (unless you inserted a string of all those first tiles in a row),
but this shows how FEIDIAN can insert a graphic back into the game.

-------------------------------------------------------------------------------
 vii. Color tiles/Multiple planes
-------------------------------------------------------------------------------
Color is a wonderful thing. It's what allows us to have games more complicated
than TI-Calculator games and Pong. But color takes more than what default
monochrome dumping can cover. To do color, we'll need to return to custom
tiles.

Remember how we previously defined a pattern for $plane1? Well, to make colors,
we need to layer planes. We can define multiple planes up to 4 for use with
FEIDIAN.

An explanation of console graphic storage is a bit beyond the scope of this
document. If you're interested in it, I suggest you head to romhacking.com and
read Klarth's document on Console Graphics. This document will focus more on
dumping color from an end-user perspective.

FEIDIAN does not ship with color support. To do color tiles, you will need to
download the appropriate tile definitions from the FEIDIAN website and install
them in your /tiles/ folder. All formats explained in Karth's document have
already been supported, so you won't need to design them yourself.

Once you have it installed, you can find graphics using Nana or your favorite
tile editor (since Nana can't do some layerings that tile editors might), then
just dump how you normally would using a custom tile.

You may notice the colors will be VERY funky! This is not an accident. The tile
definition contains a palette in it, which you may wish to edit to get a more
accurate reflection of the colors used in your game. Because graphics are
handled by their binary data, the palette is irrelevant to reinsertion, it's
just something you can make pretty for your editing pleasure.

Each color in the palette is stored as an RGB array. The first # is red, the
second is green and the third is blue. You can change them to any value between
0-255 (of 0x00-0xff) you like.

After you have your palette the way you want, just redump the graphic.

-------------------------------------------------------------------------------
 viii. Scaling and padding
-------------------------------------------------------------------------------
Both monochrome and color bitmaps can be scaled by FEIDIAN with no problems.
To scale a graphic, use this command line:

  feidian -s *width,*height input.bmp output

The width and height are how much you wish to multiply by. You can only do
nearest neighbor scaling in increments of 100%, so a scale setting of 2,1 would
scale the graphic to 200%x100%.

Padding, likewise, can work on both monochrome and color graphics. You can pad
by x,y pixels (width,height). These are pixels IN ADDITION to the current tile
size, not a new tile size. The command line for padding is:

    feidian -p tile_width,tile_height,pad_width,pad_height input.bmp output

-------------------------------------------------------------------------------
 ix. Building a dual-letter font
-------------------------------------------------------------------------------
How would this be useful? We're getting there, hold your horses.

First off, we will need a font. A FONT YOU SAY?! Yes, but not just any font.
It needs to be half the width of our desired tile size, 16 columns wide, and 16
rows high. Yes, 256 tiles. And they need to correspond to the ASCII ordering.

So we need to make a graphic that has 16 rows and 16 columns of 6x12 tiles on
it. Rather, YOU need to. Get to work, Photoshop whore!

When you've done this, copy and paste it into Paintbrush and save it as a
monochrome bitmap (it's in the dropdown list). Adobe doesn't like complying
with the M$ BITMAP standards, so while it may rock for drawing, it will eat
your graphic if you save. And if that happens, FEIDIAN won't like you anymore.
It might even give you a severe, acute respiratory syndrome, just for looking
at it funny (hah, get it? FEIDIAN. HAH).

Next up, you need a list. A LIST?! A list! And it should have two letters on
each line. Here's an example:

  I 
  do
   n
  ot
   l
  ik
  e 
  te
  nt
  ac
  le
   m
  on
  st
  er
  s.

Save that as something like "japan.txt"

So here goes our command line fun!
  feidian -d 6,12,japan.txt my_6x12.bmp output

This could take a while, so be patient. What this will do is make a new 12x12
font where each 12x12 tile contains one of your two-letter combos written using
the 6x12 font you gave it.

So, if you were doing a 16x16 font, you could follow the above example and
include and 8x16 bitmap instead. I'm sure you're getting the idea.

-------------------------------------------------------------------------------
 x. BCR (Binary Character Recognition)
-------------------------------------------------------------------------------
One of the most annoying parts of translation is having to make a table for
the font to get a script dump. Rather than starting from scratch every time,
character recognition can save a lot of time.

OCR (Optical Character Recognition) is great, but often has problems with
misreporting characters. BCR is another way to go.

Using BCR, you can take a bitmap of one font and its table and use it to try
to fill in the table for another font. It's handy for companies who use the
default dev kit fonts, or who recycle fonts between games.

In the case of identical font faces, you can get around 70~80% matched tiles
regardless of ordering changes.

The command line for binary character recognition is:
  feidian -b tile_width,tile_height,source_bitmap,text_version input.bmp output

-------------------------------------------------------------------------------
 xi. Other Notes
-------------------------------------------------------------------------------
So what else can FEIDIAN do? At the moment, a whole lot of  N O T H I N G ! ! !

And the reason for that is, everything else is already covered by another tool.
Just look for something to do it at http://www.romhacking.net/. FEIDIAN is
really just my way of filling in a GAPING need in the world of translation
tools, and it was mostly written to do things I've found myself needing again
and again that no other tool could do.

I hope you find it useful, and if not, do me the courtesy of not insulting me/
flaming my message board/filing bogus complaints with my host causing me to
lose JumpStation.org and $150 in pre-paid service.

 T H A N K   Y O U   F O R   R E A D I N G   A N D   S E E   Y O U   N E X T !

-------------------------------------------------------------------------------
 xii. License
-------------------------------------------------------------------------------
This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 2 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program (license.txt); if not, write to the Free Software
Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

This archive is subject to license.txt. Read it, or die.
--


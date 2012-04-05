

.psx					; Set the architecture to PSX


.open "OLD_SLPS_017.51","D:\AtelierElie\New\Packed\SLPS_017.51",0x8000F800		; Open SLPS_010.57 for output.

FreeSpace equ 0x800C2358				;Start of Freespace block (From Font Tables)
TestValue equ 0x32						;Test Value for NonASCII bytes
ASCIIOffset equ -0x20					;ASCII Offset
ASCIIBound equ 0x5A						;Number of Characs in Font Table

;----------------------------------------------------------
;FreeSpace Area - Tables, RAM
;----------------------------------------------------------
.org FreeSpace
	Font_Table:
		.incbin New_Font.bin
	Width_Table:
		.incbin Width_Table.bin
	PrevWidth:
		.fill 0x1
	.align

;----------------------------------------------------------
;FreeSpace Area - DMA End of Line Check
;----------------------------------------------------------

	DMA_Line_End_Check:
		;Load text byte from ROM
		lbu r2,0x0000(r7)					
		nop

		;If Text = 0, Add 12 to PrevWidth (To stop loop)
		bne r2, r0, @@Non_Zero
		nop
		lb r2, PrevWidth
		nop
		addiu r2,r2,0x6
		sb r2, PrevWidth

		;Compare Max Bytes per row with Current Bytes
		@@Non_Zero:
		sll r2, r3, 0x2
		sll r3, r3, 0x1
		addu r2, r2, r3						;r2 = max number of bytes
		lb r3, PrevWidth
		nop
		addiu r3, r3, 0x6					;leave 12 pixels from side at least
		
		;If Out of Bound - Reset PrevWidth; Stop DMA Loop
		sltu r2, r3, r2
		bne r2, r0, @@Jump_Routine
		nop
		sb r0, PrevWidth
		li r2, Hijack_DMA_Line_End_Check
		jr r2
		nop

		;Else if within Bounds - Loop DMA Routine
		@@Jump_Routine:
		li r2, DMA_Routine
		jr r2
		nop

;----------------------------------------------------------
;FreeSpace Area - DMA Width Routine
;----------------------------------------------------------
	DMA_Width:
		;r2 = chara byte

		;Push r4
		addiu sp, sp, -4
		sw r4, 0(sp)						

		;If linebreak
		beq r2,r25,@@Jump
		addiu r7,r7,0x0001

		;Check ASCII Bounds
		addiu r4, r0, ASCIIBound
		addiu r2, r2, ASCIIOffset
		sltu r4, r4, r2						;check if greater than font table! possible DTE here
		beq r4, r0, @@ValueOK
		nop

		;ASCII out of Bounds - DTE Routine
		addiu r2, r0, TestValue				;Resets to TestValue if not within table - DTE etc later here

		;ASCII within Bounds
		@@ValueOK:

		;Load PrevWidth and add current tile's width to it
		li r3, Width_Table
		nop
		addu r3, r3, r2
		lbu r3, 0x0000(r3)					;r3 = width
		lb r4, PrevWidth
		srl r2, r3, 0x1
		addu r4,r2,r4
		sb r4, PrevWidth					;load prev width and use as position of now+width/2

		;Pop r4
		lw r4, 0($sp)
		addiu sp, sp, 4

		;Load DMA Width (?)
		lhu r2,0x0010(r4)
		addiu r11,r11,0x0001
		addu r2,r2,r3						;r3 = current tile's width
		sh r2,0x0010(r4)
		sh r2,0x0574(r4)
		addiu r2,r0,0x0001

		;End Routine
		li r3, End_DMA_Width
		jr r3
		nop


		;Linebreak
		@@Jump:
		li r3, 0x8002aeb0
		jr r3
		nop

;----------------------------------------------------------
; Text/Graphic Routine
;----------------------------------------------------------
.org 0x8002a838

	Text_Draw:

		@@EndRoutine equ 0x8002aac8

		;If text byte = 0 - End Routine
		lbu r2,0x0000(r16)					;r16 = text block position
		nop
		beq r2,r0,@@EndRoutine
		sll r3,r10,0x10

		;Load textbyte
		lbu r4,0x0000(r16)					;load single byte only

		;LineBreak
		ori r2,r0,0x818f
		andi r3,r4,0x00ff
		beq r3,r2,@@EndRoutine				
		addiu r16,r16,0x0001				;increment text block position

		;Check ASCII Bounds
		addiu r2, r0, ASCIIBound
		addiu r4, r4, ASCIIOffset			;Dec by 20 for ASCII
		
		;ASCII Out of Bounds - DTE
		sltu r2, r2, r4						;check if greater than font table! possible DTE here
		beq r2, r0, @@ValueOK
		nop
		addiu r4, r0, TestValue				;Resets to 1 if not within table - DTE etc later here

		;ASCII In Bounds - Normal
		@@ValueOK:

		;Load Width of tile
		li r3, Width_Table
		nop
		addu r3, r3, r4
		lbu r2, 0x0000(r3)
		
		;Add to RAM position to store
		addiu r6,r29,0x0010					;r29 + 0x10 = base RAM position to draw to
		lb r3, PrevWidth
		srl r2, r2, 0x1
		addu r6,r6,r3

		;Add width to PrevWidth
		addu r3,r3,r2
		sb r3, PrevWidth					;load prev width and use as position of now

		;Obtain position in Font_Table (x18 Bytes)
		sll r3, r4, 0x4
		sll r4, r4, 0x1
		addu r4, r3, r4	
	
		;Load Tile position in ROM
		li r3, Font_Table
		addu r5, r3, r4
		addiu r4, r5, 0x4					;place character pos. in ROM into r5, +0x4 into r4
		addu r7,r0,r0
		j 0x8002a930
		nop

	;New Line Highjack to reset PrevWidth
	New_Line:
		sll r2,r10,0x10
		sb r0, PrevWidth
		j Hijack_NewLine

	;Line End Check to compare PrevWidth
	Line_End_Check:
		;r2 = current char no.; r3 = number of characters in whole (Both usable)

		;Obtain Max No. of Bytes (r2)
		sll r2, r3, 0x2
		sll r3, r3, 0x1
		addu r2, r2, r3			

		;Load what the current no. of bytes is
		lb r3, PrevWidth
		nop
		addiu r3, r3, 0x6					;r3 = what next would be
		
		;Within Bounds - Loop; else End Routine
		sltu r2, r3, r2
		bne r2, r0, Text_Draw
		addiu r18,r18,0x0001
		j Hijack_Line_End_Check
		nop



;----------------------------------------------------------
;Hijack Line End Check
;----------------------------------------------------------
.org 0x8002aabc
	j Line_End_Check
	nop
	nop
	Hijack_Line_End_Check:


;----------------------------------------------------------
;Hijack Newline/end
;----------------------------------------------------------
.org 0x8002aac8
	j New_Line
	Hijack_NewLine:

;----------------------------------------------------------
;DMA Creation routine
;----------------------------------------------------------
.org 0x8002ae50

	;DMA Writing Routine (?)
	DMA_Routine:
		;Checks if text byte = 0 - skips writing
		lbu r2,0x0000(r7)
		nop
		beq r2,r0,0x8002aeb8
		addiu r2,r5,0x0001

		;Unknown check? (Possibly Character Count check)
		sll r2,r11,0x10
		sra r2,r2,0x10
		slt r2,r2,r15
		beq r2,r0,0x8002aeb8
		addiu r2,r5,0x0001

		;Jump to DMA Width Routine
		lbu r2,0x0000(r7)
		li r3, DMA_Width
		jr r3
		nop
		nop
		nop
		nop
		nop
		nop
		nop
		nop
		nop
		End_DMA_Width:
	

;----------------------------------------------------------
;Hijack DMA End of Line Check
;----------------------------------------------------------
.org 0x8002aec8
	li r2, DMA_Line_End_Check
	jr r2
	nop
	Hijack_DMA_Line_End_Check:



.org 0x801594F8
	.dh 107, 0, 124, 12			; Gust Presents
	.dh 161, 16, 180, 10		;-|
	.dh 156, 27, 180, 20		; |
	.dh 156, 48, 180, 22		; |-Intro Text
	.dh 151, 71, 180, 32		; |
	.dh 156, 104, 180, 20		; |
	.dh 151, 125, 180, 30		;-|
	.dh 181, 160, 200, 12		; Press Start Button
	.dh 45, 176, 112, 72		; Logo


.close

 ; make sure to leave an empty line at the end

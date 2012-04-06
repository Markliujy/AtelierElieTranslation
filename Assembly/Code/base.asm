

.psx					//  Set the architecture to PSX


.open "OLD_SLPS_017.51","..\Translated Files\Final\SLPS_017.51",0x8000F800		//  Open SLPS_010.57 for output.

FreeSpace equ 0x800C2358			// Start of Freespace block (From Font Tables)
TestValue equ 0x32						// Test Value for NonASCII bytes
ASCIIOffset equ -0x20					// ASCII Offset
ASCIIBound equ 0x5A						// Number of Characs in Font Table

// ----------------------------------------------------------
// FreeSpace Area - Tables, RAM
// ----------------------------------------------------------

.org FreeSpace
	DATA_Font_Table:
		.incbin ./Bin/New_Font.bin

	DATA_Width_Table:
		.incbin ./Bin/Width_Table.bin

	RAM_New_Width:
		.fill 0x1

	RAM_Prev_Width:
		.fill 0x1

	RAM_Width:
		.fill 0x1

	RAM_Row_Buffer:
		.fill 0x8

	RAM_DTE_Buffer:
		.fill 0x1
		
	DATA_DTE_Table:
		.incbin ./Bin/DTE_Table.bin

	.align

// ----------------------------------------------------------
// Include new functions into free space
// ----------------------------------------------------------

.include ./Code/TextDrawToVRAM.asm
;.include ./Code/DialogueDMA.asm
.include ./Code/GetTextWidth.asm

// ----------------------------------------------------------
// Hijack routines
// ----------------------------------------------------------

.include ./Code/Hijacks/TextDrawToVRAM.asm
.include ./Code/Hijacks/DialogueDMA.asm

		
// ----------------------------------------------------------
//  Include Intro Hack
// ----------------------------------------------------------

.include ./Code/Intro.asm	


// ----------------------------------------------------------

.close	

// ----------------------------------------------------------
// Request DMA Hack (r31 = link register)
// ----------------------------------------------------------



.open "..\Original Files\Decompressed\OV\REQUEST.CRS","..\Translated Files\Unpacked\OV\REQUEST.CRS",0x8014B700		//  Open REQUEST.CRS for output.


.org 0x80158768

	jal GetTextWidth
	nop
	END_Get_Width:
	addiu r4,r16,0x0020
  nop
  jal 0x8002a74c
  sh r2, 0x0006(r16)
  
.org 0x80157930
 
 	sll r2, r3, 0x4
 	nop
 	nop
 	
.org 0x801579a0
 	
 	addu r7, r0, r8
 	nop
 	sll r7, r7, 0x10

.org 0x801579f4
	
	sll r2, r3, 0x4
 	nop
 	nop

.org 0x80157a64
	addu r7, r0, r8
 	nop
 	sll r7, r7, 0x10

// 19, 19 - Longtext
// 19, 33 - fixedtext


.close
//  EMPTY

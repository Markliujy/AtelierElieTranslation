// ----------------------------------------------------------
//  	TextDrawToVRAM Sub-Routine 0x8002a838 - 0x8002aac8
// ----------------------------------------------------------

TextDrawToVRAM_Return equ 0x8002ab48
DrawSync equ 0x800B3860
LoadImage equ 0x800B3B0C


	
	// r10 - Originally Character Counter
	// r16 - Script block position
	// r17 - RAM Position
	//		0x0004 - Max Chars	
	// r18 - No idea? some sort of character counter...
TextDrawToVRAM:

	// If no text
	lbu     r2, 0(r16)
	nop
	beqz    r2, TextDrawToVRAM_End
	nop

	
	// Check if Max Chars is less than 0
	lh      r2, 4(r17)
	nop
	blez    r2, EndOfLine
	move    r10, r0


	// NewTextChar
	TextDrawToVRAM_NewTextChar:
		
		
		// Normal load text byte from ROM
		lbu r4,0x0000(r16)					// r16 = text block position
		move r10, r0						// Initialize DTE register
		
		// Zero EoL in ROM
		beq r4,r0,EndOfLine
		nop

		// LineBreak check
		ori r2,r0,0x818f
		andi r3,r4,0x00ff
		beq r3,r2,EndOfLine
		nop

		// Check ASCII Bounds
		addiu r2, r0, ASCIIBound			// Use r2 to check for bound (7A combined with offset)
		addiu r4, r4, ASCIIOffset			// Dec by 20 for ASCII
		andi r4, r4, 0x00ff

		// ASCII Out of Bounds - DTE
		sltu r2, r2, r4						// check if greater than font table! possible DTE here
		beq r2, r0, @@Value_OK
		nop

// --- DTE ---------------------------------------------------------------------------------------

		// Find DTE Table position
		addiu r4, r4, -(ASCIIBound+1)
		sll r4, r4, 0x1
		li r2, DATA_DTE_Table
		addu r4, r4, r2
		
		// Load DTE Buffer
		lbu r2, RAM_DTE_Buffer
		nop
		
		// Use DTE Buffer to find DTE text
		addu r4, r2, r4
		lbu r4, 0x0000(r4)
		nop
		addiu r4, r4, ASCIIOffset
		
		addiu r2, 1
		move r10, r2						// Set DTE register
		// Stop DTE if more than 2 depth
		bge r2, 0x2, @@StopDTE
		nop
		
		// Continue DTE
		sb r2, RAM_DTE_Buffer				// Set DTE Buffer
		j @@DTE_Value_OK
		nop
		
		@@StopDTE:
		sb r0, RAM_DTE_Buffer				// Reset DTE Buffer

		j @@DTE_Value_OK
		nop
	
// ------------------------------------------------------------------------------------------

		// ASCII In Bounds - Normal routine
		@@Value_OK:
			
			sb r0, RAM_DTE_Buffer				// Reset DTE Buffer
			

		@@DTE_Value_OK:

			// Load Width of tile
			li r3, DATA_Width_Table
			nop
			addu r3, r3, r4
			lbu r2, 0x0000(r3)
			sb r2, RAM_Width
			
			// Determines Dest. RAM Position
			
			lbu r3, RAM_New_Width
			nop
			sb r3, RAM_Prev_Width
			
			// Add width to RAM_New_Width
			addu r2,r3,r2
			sb r2, RAM_New_Width

			// End of Line Check
			lh r7,0x0004(r17)			// Max Chars
			nop
			sll r5, r7, 0x2				// x4
			sll r7, r7, 0x1				// x2
			addu r5, r5, r7				// x6
			sll r5, r5, 0x1				// x12
			;addiu r5, r5, 0xFFFa						// Bug - Character tiles are 12x12!

			blt r2, r5, @@Not_EOL
			addiu r18,r18,0x0001		// Character counter?

			bne r0, r10, @@DTEEoL
			nop
			sb r10, RAM_DTE_Buffer
			j EndOfLine
			nop

			@@DTEEoL:
			addiu r10, -1
			sb r10, RAM_DTE_Buffer
			j EndOfLine
			nop

		@@Not_EOL:
			lbu r10, RAM_DTE_Buffer
			nop
			bne r0, r10, @@Not_EOL_DTE
			nop
			addiu r16,r16,0x0001				// Increment character
			
			@@Not_EOL_DTE:

			// Used regs:
			//	r7, r5,

			li r7, 0x1
			and r7, r7, r3
			add r3, r3, r7
			srl r3, r3, 0x1

			addiu r6,r29,0x0010					// r29 + 0x10 = base RAM position to draw to
			addu r6,r6,r3
			srl r5, r5, 0x1

			subu r10, r5, r3
			//srl r10, 0x1						// Remaining no of bytes

			// Obtain position in DATA_Font_Table (x18 Bytes)
			sll r3, r4, 0x4
			sll r4, r4, 0x1
			addu r4, r3, r4

			// Load Tile position in ROM
			li r3, DATA_Font_Table
			addu r5, r3, r4
			addiu r4, r5, 0x4					// place character pos. in ROM into r5, +0x4 into r4

		// Register Inputs:
		// 	r4 - Source RAM 2 (r5 + 0x4)
		// 	r5 - Source RAM
		// 	r6 - Destination RAM
		// 	r7 - Row block counter (starts at 0)
		// 	r8 - empty?
		//	r10- Byte Width remaining in line

		// Used registers:
		//	r2, r3
		addiu r4, r0, 0x0			// Blank r4 - Row counter
		addu r7,r0,r0					// Blank r7 - Row Block Counter


// --- Drawing ---------------------------------------------------------------------------------------

		// Loop Each 'Block' of 4 pixel height
		LOOP_Row_Blocks:
			
			// 4 Rows of Pixels Loop
			LOOP_Rows:
				
				// Skip row 12
				blt r7, 0x2, @@Not_Last_Row		
				nop
				bge r4, 0x3, END_LOOP_Rows
				nop
				
				
					
				// --- Draw to Row Buffer ---------------------------------------------------------------------------------------
				@@Not_Last_Row:
				
				// 1st 4 pixels
				
				lhu r2, 0x0000(r5)
				addiu r5, r5, 0x2
				srlv r2, r2, r4				// Shift right by row counter
				andi r2, r2, 0x1111
				sh r2, RAM_Row_Buffer
				
				
				// 2nd 4 pixels
				
				lhu r2, 0x0000(r5)
				addiu r5, r5, 0x2
				srlv r2, r2, r4				// Shift right by row counter
				andi r2, r2, 0x1111
				sh r2, RAM_Row_Buffer+2
				

				// 3rd 4 pixels
				
				lhu r2, 0x0000(r5)
				addiu r5, r5, 0x2
				srlv r2, r2, r4				// Shift right by row counter
				andi r2, r2, 0x1111
				sh r2, RAM_Row_Buffer+4
				

				addiu r4, r4, 0x1			// Increment Row Counter

				// --- Draw to RAM ---------------------------------------------------------------------------------------
				
				// 3 Halfwords in BufferRAM - Little endian so reversed!

				// Can use:
				// 	r2, r3, r10

				// Load width and determine whether it is odd
				lbu r2, RAM_Prev_Width
				nop
				andi r2, r2, 0x1
				beq r2, r0, @@Even_Width
				nop

				// Odd Width - lots of shifting - Note little endianness!
					
					// First pixel - needs to be ored with last halfword
					lhu r3, RAM_Row_Buffer
					nop
					lhu r2, -0x0002(r6)
			
					sll r3, r3, 3*4			// Shift 3 pixels right
					sll r2, r2, 1*4			// Remove last pixel of last halfword
					srl r2, r2, 1*4
					
					or r2, r2, r3
					sh r2, -0x0002(r6)
					
					beq r10, 0x0, @@Return
					nop							// End if only 1 pixel

					// Next 4 pixels
					lhu r3, RAM_Row_Buffer
					nop
					lhu r2, RAM_Row_Buffer+2
					srl r3, r3, 0x4
					sll r2, r2, 0xc
					or r2, r2, r3
					bge r10, 0x2, @@LargerThan2	// Continue if larger than 5 pixels
					nop
					
					sb r2, 0x0000(r6)			// Save only 1 byte and end
					j @@Return
					nop
					
					@@LargerThan2:
					sh r2, 0x0000(r6)
					beq r10, 0x2, @@Return
					nop						
					
					// Next 4 pixels
					lhu r3, RAM_Row_Buffer+2
					nop
					lhu r2, RAM_Row_Buffer+4
					srl r3, r3, 0x4
					sll r2, r2, 0xc
					or r2, r2, r3
					
					bge r10, 0x4, @@LargerThan4	// Continue if larger than 2 bytes
					nop
					
					sb r2, 0x0002(r6)			// Save only 1 byte and end
					j @@Return
					nop
					
					@@LargerThan4:
					sh r2, 0x0002(r6)
					beq r10, 0x4, @@Return
					nop						
					
					// Last 3 pixels (and extra)
					lhu r3, RAM_Row_Buffer+4
					nop
					srl r3, r3, 0x4
					bge r10, 0x6, @@LargerThan6	// Continue if larger than 5 pixels
					nop
					
					sb r2, 0x0004(r6)			// Save only 1 byte and end
					j @@Return
					nop

					
					@@LargerThan6:
						
					sh r3, 0x0004(r6)
					j @@Return
					nop
					



				// Even width - simple copy and paste
				@@Even_Width:
					
					
					lhu r2, RAM_Row_Buffer
					nop
					bge r10, 0x2, @@ELargerThan2	// Continue if larger than 2 bytes
					nop
					
					sb r2, 0x0000(r6)			// Save only 1 byte and end
					j @@Return
					nop
					
					@@ELargerThan2:

					
					sh r2, 0x0000(r6)
					beq r10, 0x2, @@Return
					nop
					lhu r2, RAM_Row_Buffer+2
					nop
					bge r10, 0x4, @@ELargerThan4	// Continue if larger than 2 bytes
					nop
					
					sb r2, 0x0002(r6)			// Save only 1 byte and end
					j @@Return
					nop
					
					@@ELargerThan4:

					
					sh r2, 0x0002(r6)
					beq r10, 0x4, @@Return
					nop					
					lhu r2, RAM_Row_Buffer+4
					nop
					bge r10, 0x6, @@ELargerThan6	// Continue if larger than 2 bytes
					nop
					
					sb r2, 0x0004(r6)			// Save only 1 byte and end
					j @@Return
					nop
					
					@@ELargerThan6:
					sh r2, 0x0004(r6)
					


				// Return
				@@Return:

				// Next Line
				lh r3,0x0004(r17)				// Max Chars
				nop
				sll r2,r3,0x01
				addu r2,r2,r3
				sll r2,r2,0x01
				addu r6,r6,r2
				
				// Continue loop if 4 rows in this block not complete
				addiu r5, r5, -0x6
				blt r4, 0x4, LOOP_Rows
				nop

			END_LOOP_Rows:

				// End loops if last block complete
				bge r7, 0x2, END_LOOP_Row_Blocks
				nop
				
				addiu r5, r5, 0x6				// Inc. Source RAM Position
				addu r4, r0, r0					// Reset Row Counter

				j LOOP_Row_Blocks
				addiu r7, r7, 0x1				// Inc. Row Block Counter

		END_LOOP_Row_Blocks:
			
			
			// Next char
			j TextDrawToVRAM_NewTextChar
			nop
			

// --- EoL ---------------------------------------------------------------------------------------
			
	EndOfLine:
		
		
		// Clear remaining graphics area
		// Usable Rs = 2, 3, 4, 5
		//		
		
					
			// Max Byte Width of line
			lh r3,0x0004(r17)				
			nop
			sll r2,r3,0x01
			addu r2,r2,r3
			sll r5,r2,0x01
			
			// Current Byte width of line
			lbu r3, RAM_New_Width
			nop
			
			andi r2, r3, 0x1
			addu r3, r2, r3						// Make sure width is even
			srl r3, r3, 0x1						// Find number of bytes
			
			addiu r6,r29,0x0010
			addu r6, r3, r6						// Find current RAM position
			
			addiu sp, sp, -4
			sw r6,0(sp)
			
			
			
			subu r3, r5, r3						// Find Remaining Width
			
			
			li r2, 0x11
			blez r3, @@NoLoop
			move r4, r0
			
			// Loop:
			//		r2 = New
			//		r3 = Remaining width (Bytes)
			//		r4 = Byte Counter
			//		r5 = Line width
			//		r6 = Dest. RAM
			@@Loop:
				addiu r4, 0x1					// Increment Bytes counter
				
				// Draw 11 rows
				sb r2, 0x0(r6)
				addu r6, r5, r6
				sb r2, 0x0(r6)
				addu r6, r5, r6
				sb r2, 0x0(r6)
				addu r6, r5, r6
				sb r2, 0x0(r6)
				addu r6, r5, r6
				sb r2, 0x0(r6)
				addu r6, r5, r6
				sb r2, 0x0(r6)
				addu r6, r5, r6
				sb r2, 0x0(r6)
				addu r6, r5, r6
				sb r2, 0x0(r6)
				addu r6, r5, r6
				sb r2, 0x0(r6)
				addu r6, r5, r6
				sb r2, 0x0(r6)
				addu r6, r5, r6
				sb r2, 0x0(r6)
				addu r6, r5, r6
				
				// Load original RAM destination and increment
				lw r6, 0($sp)
				nop
				addiu r6, 0x1
				sw r6,0(sp)
				blt r4, r3, @@Loop
				nop
				
		@@NoLoop:
		addiu sp, 0x4
		sb r0, RAM_New_Width					// Reset VWF Width
		j CopyToVRAM
		nop

	// Return to CopyToVRAM sub-function
	CopyToVRAM:
		jal     DrawSync
		move    r4, r0
		addiu   r4, r29, 0x580
		jal     LoadImage
		addiu   r5, r29, 0x10
		addiu   r2, r19, 1
		move    r19, r2
		lhu     r3, 0x582(r29)
		sll     r2, 16
		addiu   r3, 0xB
		sh      r3, 0x582(r29)
		lh      r3, 6(r17)
		sra     r2, 16
		slt     r2, r3
		bnez    r2, TextDrawToVRAM
		nop


		
	TextDrawToVRAM_End:
		sb r0, RAM_DTE_Buffer				// Reset DTE Buffer for each new sub-routine
		li r2, TextDrawToVRAM_Return
		jr r2
		nop		

		//  EMPTY
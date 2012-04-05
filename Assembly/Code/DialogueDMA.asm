// ----------------------------------------------------------
// FreeSpace Area - DMA End of Line Check
// ----------------------------------------------------------

	NEWFUNC_Dma_Line_End_Check:

		// Load text byte from ROM
		lbu r2,0x0000(r7)
		nop

		// If Text = 00, End
		bne r2, r0, @@Non_Zero
		nop
		sb r0, RAM_New_Width
		li r2, RETURN_Dma_Line_End_Check
		jr r2
		nop


		// Compare Max Bytes per row with Current Bytes
		@@Non_Zero:

			sll r2, r3, 0x2
			sll r3, r3, 0x1
			addu r2, r2, r3
			sll r2, r2, 0x1						// r2 = max width


			lbu r3, RAM_New_Width
			nop
			addiu r3, r3, 12

			// If Out of Bound - Reset RAM_New_Width//  Stop DMA Loop
			sltu r2, r3, r2
			bne r2, r0, @@Jump_Routine
			nop
			sb r0, RAM_New_Width
			li r2, RETURN_Dma_Line_End_Check
			jr r2
			nop


		// Else if within Bounds - Loop DMA Routine
		@@Jump_Routine:

			li r2, FUNC_Dma_Routine
			jr r2
			nop

// ----------------------------------------------------------
// FreeSpace Area - DMA Width Routine
// ----------------------------------------------------------
	NEWFUNC_Dma_Width:
		
		// Push r4
		addiu sp, sp, -4
		sw r4, 0(sp)
		nop

		
		// DTE Buffer? Load DTE and skip normal load
		lbu r2, RAM_DTE_Buffer
		nop
		beq r2, r0, @@Non_DTE
		nop
		sb r0, RAM_DTE_Buffer
		nop
		j @@DTE_Start
		nop
		

		
		// Normal load
		@@Non_DTE:
			
		lbu r2,0x0000(r7)
		nop
		addiu r7,r7,0x0001
			
		// If linebreak
		beq r2,r25,@@Jump
		nop
		
		@@DTE_Start:

		// Check ASCII Bounds
		addiu r4, r0, ASCIIBound
		addiu r2, r2, ASCIIOffset
		sltu r4, r4, r2						// check if greater than font table! possible DTE here
		beq r4, r0, @@Value_OK
		nop

		// Find DTE Table position
		addiu r4, r2, -(ASCIIBound+1)
		sll r4, r4, 0x1
		li r2, DATA_DTE_Table
		addu r4, r4, r2
		
		// Load next DTE value and store into RAM
		lbu r2, 0x0001(r4)
		nop
		sb r2, RAM_DTE_Buffer
		nop
		
		// Load current DTE value and use for rest of routine
		lbu r2, 0x0000(r4)
		nop
		addiu r2, r2, ASCIIOffset


		// ASCII within Bounds
		@@Value_OK:

			// Load RAM_New_Width and add current tile's width to it
			li r3, DATA_Width_Table
			nop
			addu r3, r3, r2
			lbu r3, 0x0000(r3)					// r3 = width
			lbu r4, RAM_New_Width
			nop
			addu r4,r3,r4
			sb r4, RAM_New_Width					// load prev width and use as position of now+width/2
			nop


			// Pop r4
			lw r4, 0($sp)
			addiu sp, sp, 4

			// Load DMA Width (?)
			lhu r2,0x0010(r4)
			addiu r11,r11,0x0001
			addu r2,r2,r3						// r3 = current tile's width
			sh r2,0x0010(r4)
			sh r2,0x0574(r4)
			addiu r2,r0,0x0001

			// End Routine
			li r3, RETURN_Dma_Width
			jr r3
			nop


		// Linebreak
		@@Jump:
			li r3, 0x8002aeb0
			jr r3
			nop
			
//Empty
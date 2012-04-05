// ----------------------------------------------------------
// 	GetWidth Function
//
//	Stack:
//		8 Registers
//		Return Values:
//			Width
//		Arguments/Return:
//			RAM begin
//			DTE input
//			Max Width
//
// ----------------------------------------------------------				

GetTextWidth:
	
// --- Initialize ---------------------------------------------------------------------------------------

	// Push registers
	addi	sp, sp, -28
	sw 		r2, 0(sp)
	sw 		r3, 4(sp)
	sw 		r4, 8(sp)
	sw 		r5, 12(sp)
	sw 		r6, 16(sp)
	sw 		r7, 20(sp)
	sw		r8, 24(sp)
	
	lw 		r8, 32(sp)			// Load RAM input
	lw 		r7, 36(sp)			// Load DTE input
	lw		r6, 40(sp)			// Load Max Width input
	move 	r5, r0				// Initialize Width counter
	
// --- Next Char ---------------------------------------------------------------------------------------	
	
	@@NextChar:
		
		// Load next char
		lbu 	r3, 0x0(r8)
		nop
		
		// End if end of text
		beqz 	r3, @@EndFunction
		nop
		
		// DTE Check
		addiu 	r3, ASCIIOffset
		andi 	r3, r3, 0x00ff
		blt 	r3, ASCIIBound+1, @@NonDTE
		nop
		
// --- DTE ---------------------------------------------------------------------------------------
		
		// Find DTE Table position
		addiu 	r4, r3, -(ASCIIBound+1)
		sll 	r4, r4, 0x1
		li 		r2, DATA_DTE_Table
		addu 	r4, r4, r2
				
		// Use DTE Counter to find DTE text
		addu 	r4, r7
		lbu 	r4, 0x0000(r4)
		nop
		addiu 	r3, r4, ASCIIOffset
		
		// Increment DTE counter
		addiu 	r7, 1
		
		
// --- Non-DTE ---------------------------------------------------------------------------------------
	@@NonDTE:
		
		// Load width and add to width counter
		li 		r2, DATA_Width_Table
		nop
		addu 	r2, r3, r2
		lbu 	r2, 0x0000(r2)
		nop
		addu 	r4, r2, r5
		
		// If larger then max width - End function
		bge 	r4, r6, @@EoL
		nop
		addu 	r5, r2
	
		// Increment if no DTE
		beqz 	r7, @@Increment
		nop
		
		// Skip increment if DTE
		blt 	r7, 2, @@NextChar
		nop
		
		
		// Reset DTE and increment character
		@@Increment:
		move r7, r0
		addiu r8, 1
		
		j @@NextChar
		nop
			
	@@EoL:
		// End if no DTE
		beqz r7, @@EndFunction
		nop
		
		// Decrement DTE as not used yet
		addiu r7, -1
		j @@EndFunction
		nop
			
// --- End Function ---------------------------------------------------------------------------------------
			
	@@EndFunction:
		sw 		r8, 32(sp)			// Save RAM input
		sw 		r7, 36(sp)			// Save DTE input
		
		sw		r5, 28(sp)			// Save width output
			
		// Pop Stored Registers
		lw 		r2, 0(sp)
		lw 		r3, 4(sp)
		lw 		r4, 8(sp)
		lw 		r5, 12(sp)
		lw 		r6, 16(sp)
		lw 		r7, 20(sp)
		lw		r8, 24(sp)
		addi	sp, sp, 28
		jr ra
		nop
				
			//  EMPTY
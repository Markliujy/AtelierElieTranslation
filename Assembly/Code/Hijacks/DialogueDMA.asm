// ----------------------------------------------------------
// DialogueDMA Hijack
// ----------------------------------------------------------
DialogueDMA_EndOfLine equ 0x8002AED8

.org 0x8002ad30
	sw r0, -0x8(sp)			// Setup DTE input

.org 0x8002ae50

	
	// Load Max Width
	lh      r3, 4(r8)
	nop
	addu 	r3, r3, r6
	sll r2, r3, 0x2
	sll r3, r3, 0x1
	addu r2, r2, r3
	sll r2, r2, 0x1
	
	// Store arguments
	addiu sp, -0x10
	sw r2, 0xC(sp)			// Max width
	sw r7, 0x4(sp)			// RAM position
	
	// Run GetTextWidth
	jal GetTextWidth
	nop
	
	lw r7, 0x4(sp)
	lw r2, 0x0(sp)
	addiu sp, 0x10
	                
	sh      r2, 0x10(r4)
	sh      r2, 0x574(r4)
	li      r2, 1
	j       DialogueDMA_EndOfLine
	sb      r2, 0xAD4(r9)

	

//Empty
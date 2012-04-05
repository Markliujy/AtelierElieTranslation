// ----------------------------------------------------------
//  TextDrawToVRAM Sub-Routine Hijack 0x8002a838 - 0x8002aac8
// ----------------------------------------------------------
.org 0x8002a818
	
	li r2, TextDrawToVRAM
	jr r2
	nop

//  EMPTY
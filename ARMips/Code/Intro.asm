//----------------------------------------------------------
// Intro Hack
//
//	1 - Vert On-Screen position
//	2 - Vert VRAM position 
//	3 - Width of block (multiple of 4)
//	4 - Height of block
//
//----------------------------------------------------------

.org 0x801594F8
	.dh 107, 0, 124, 12			// Gust Presents
	.dh 160, 12, 180, 12		//-|
	.dh 154, 24, 180, 24		// |
	.dh 154, 48, 180, 24		// |-Intro Text (V-Center is approx 166)
	.dh 148, 72, 180, 36		// |
	.dh 154, 108, 180, 24		// |
	.dh 148, 132, 180, 36		//-|
	.dh 181, 173, 200, 1		// Press Start Button (Not relevant?)
	.dh 56, 187, 112, 61		// Logo
	
	
.org 0x801566F4
	.dh 173									// Press Start Button Y VRAM Pos

//.org 0x801594F8
//	.dh 107, 0, 124, 12			// Gust Presents
//	.dh 161, 16, 180, 10		//-|
//	.dh 156, 27, 180, 20		// |
//	.dh 156, 48, 180, 22		// |-Intro Text (V-Center is approx 166)
//	.dh 151, 71, 180, 32		// |
//	.dh 156, 104, 180, 20		// |
//	.dh 151, 125, 180, 30		//-|
//	.dh 181, 160, 200, 12		// Press Start Button
//	.dh 45, 176, 112, 72		// Logo
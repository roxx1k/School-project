#ifndef __Game_Header__
#define __Game_Header__

#include "Graphics.h"
#include "UString.h"
#include "Debug.h"
#include "Image.h"
#include "ImageAtlas.h"
#include "FileResolver.h"
#include "Event.h"
#include "UString.h"
#include "Animation.h"
#include "GameData.h"
#include "Zone.h"
#include <WindowsX.h>

class Game{

public:

	Game();
	~Game();
	
	void init();
	void keyPressed(const Event& e);
    void keyReleassed(const Event& e);	
	void mouseMoved(const Event& e);
	void mouseClicked(int x,int y);

	bool update();
	void draw();
	void display();
	void distanceChecker(GameObject *first,GameObject *second);
	

	
};

#endif //__Game_Header__
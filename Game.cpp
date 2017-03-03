#include "Game.h"

Game::Game(){}

/*
 *	inicializace hry
 *
 */
void Game::init(){
	Graphics &g = Graphics::getInstance();
	g.init(); // inicializace vykreslování
	g.orthoProjectionf(0, WIDTH, 0, HEIGHT, -1, 1); // nastavení projekce
	
	//Nefunguje
	//SoundBank::getInstance();
	//SoundBank::getInstance().addEffect(FileResolver::resolvePath("bomb2.ogg"));	// naètení zvuku
	//SoundBank::getInstance().playMusic(FileResolver::resolvePath("test.ogg")); // streamování hudby na pozadí
	//SoundBank::getInstance().setMusicVolumef(0.1); //nastavení hlasitosti hudby
	
	GameData& data = GameData::getInstance();
	/*GameData::dispose();
	data = GameData::getInstance();*/
	//data.enemy->animation = new Animation(FileResolver::resolvePath("animation.xml"), FileResolver::resolvePath("radiace.xml"), Zone::e_Game); // naètení animace
	//data.enemy->animation->play(); // spuštìní animace
	//data.atlas = new ImageAtlas(Zone::e_Game); // vytvoøení atlasu obrázkù
	//data.atlas->load(FileResolver::resolvePath("heli.xml"));// naètení vzducholodì a pozadí
	//data.player->image = data.atlas->get("heli"); // pøiøazení textury objektu player
	
	
	/*glDisable(GL_DEPTH_TEST);
	glClearColor(0.0f,0.0f,0.5f,0.1f);	
	glClear(GL_COLOR_BUFFER_BIT);*/
	/*glEnable(GL_BLEND);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);*/

	
}

/*
 *	zachycení stisknutých kláves
 *
 */
void Game::keyPressed(const Event& e){
	
	//DEBUG_LOG(UString().concat(e.getKeyFlags())+"\n");
	if(e.isPressed(Event::KEY_LEFT)){
		GameData& data = GameData::getInstance();
		data.player->speed.x += -1;
	}
	if(e.isPressed(Event::KEY_RIGHT)){
		GameData& data = GameData::getInstance();
		data.player->speed.x += 1;
	}
	if(e.isPressed(Event::KEY_UP)){
		GameData& data = GameData::getInstance();
		data.player->speed.y += 1;
	}
	if(e.isPressed(Event::KEY_DOWN)){
		GameData& data = GameData::getInstance();
		data.player->speed.y += -1;
	}
	//
	if (e.isPressed(Event::MOUSE_LEFT_BTN))
	{
		GameData& data = GameData::getInstance();

		POINT p;		
		p.x = e.mouseX();
		p.y= HEIGHT - e.mouseY();
		Vector newDirection(0.0,0.0);
		newDirection.x = -(p.x - data.player->position.x);
		newDirection.y = -(p.y - data.player->position.y);
		if (abs(newDirection.x) > abs(newDirection.y))
		{
			newDirection.y /= abs(newDirection.x);
			newDirection.x /= abs(newDirection.x);			
		} 
		else
		{			
			newDirection.x /= abs(newDirection.y);
			newDirection.y /= abs(newDirection.y);
		}
		float playerArea,newPlayerArea;
		playerArea = M_PI*pow(data.player->radius,2);
		float newEnemyArea = playerArea /20;
		data.enemyList.push_back(
			new Enemy(
			data.player->position.x - (data.player->radius * newDirection.x),
			data.player->position.y - (data.player->radius * newDirection.y),
			sqrt(newEnemyArea/M_PI),//data.player->radius/10,
			-newDirection.x*20,
			-newDirection.y*20
			));
		//float newEnemyArea =  M_PI*pow(data.enemyList.at((data.enemyList.size()-1))->radius,2);
		
		
		newPlayerArea = playerArea - newEnemyArea;
		
		data.player->radius = sqrt(newPlayerArea/M_PI);
		data.player->speed.x += newDirection.x;
		data.player->speed.y += newDirection.y;
		//data.player->radius -= data.player->radius/ data.enemyList.at((data.enemyList.size()-1))->radius;

		
		//if (((float)p.x < data.player->position.x) )
		//{	
		//	data.player->speed.x += newDirection.x;

		//	if ((float)p.y > data.player->position.y )
		//	{	
		//		data.player->speed.y += newDirection.y;
		//		data.player->radius -= data.player->radius/90;
		//		/*data.enemyList.push_back(
		//			new Enemy(
		//			data.player->position.x - data.player->radius,
		//			data.player->position.y + data.player->radius,
		//			data.player->radius/5,
		//			-data.player->speed.x*4,
		//			-data.player->speed.y*4
		//			));*/
		//	}
		//	if ((float)p.y < data.player->position.y) 
		//	{		
		//		data.player->speed.y += newDirection.y;
		//		data.player->radius -= data.player->radius/90;

		//		/*data.enemyList.push_back(
		//			new Enemy(
		//			data.player->position.x - data.player->radius,
		//			data.player->position.y - data.player->radius,
		//			data.player->radius/5,
		//			-data.player->speed.x*4,
		//			-data.player->speed.y*4
		//			));*/
		//	}						
		//}
		//if (((float)p.x > data.player->position.x) )
		//{	
		//	data.player->speed.x += newDirection.x;

		//	if ((float)p.y < data.player->position.y )
		//	{			
		//		data.player->speed.y += newDirection.y;
		//		data.player->radius -= data.player->radius/90;

		//		/*data.enemyList.push_back(
		//			new Enemy(
		//			data.player->position.x + data.player->radius,
		//			data.player->position.y + data.player->radius,
		//			data.player->radius/5,
		//			-data.player->speed.x*4,
		//			-data.player->speed.y*4
		//			));*/
		//	}
		//	if ((float)p.y > data.player->position.y) 
		//	{		
		//		data.player->speed.y += newDirection.y;
		//		data.player->radius -= data.player->radius/90;

		//		/*data.enemyList.push_back(
		//			new Enemy(
		//			data.player->position.x + data.player->radius,
		//			data.player->position.y - data.player->radius,
		//			data.player->radius/5,
		//			-data.player->speed.x*4,
		//			-data.player->speed.y*4
		//			));*/
		//	}
		//							
		//}

	}
	if (e.isPressed(Event::MOUSE_RIGHT_BTN))
	{
		GameData& data = GameData::getInstance();
		//data.player->speed.x = 0;		
	}
}
void Game::display() {
	glDisable(GL_DEPTH_TEST);
	glClearColor(0.0f,0.0f,0.0f,0.5f);
	glClear(GL_COLOR_BUFFER_BIT);
	glLoadIdentity();
}

void Game::keyReleassed(const Event& e){

}

/*
 *	update objektù hry
 *
 */
bool Game::update(){
	
	GameData& data = GameData::getInstance();
	data.player->update();	// posun hrace
	// posun nepratel
	for (int i=0;i<data.enemyList.size();i++)
	{
		data.enemyList.at(i)->update();
	}

	//check if player is too small and restart is needed
	//second condition determines if radius isn't NaN due to changes and updates and calculations
	if(data.player->radius <= 1 || data.player->radius != data.player->radius){
		Sleep(1000);
		data.player->init();
		int count = data.enemyList.size();

		for (int i=0;i<count;i++)
		{
			if (i<ENEMYCOUNT)
			{
				data.enemyList.at(i)->init();
			} 
			else
			{
				data.enemyList.pop_back();
			}
			
		}
	}
	return false;
}

/*
 *	vykreslení hry
 *
 */
void Game::draw(){
	//glClearColor(0.1f,0.0f,0.0f,0.5f); //pozadi
	//glClear(GL_COLOR_BUFFER_BIT);
	Graphics &g = Graphics::getInstance();
	g.reset();
		
	GameData& data = GameData::getInstance();
	
	//if(data.player->radius<1)glClearColor(0.0f,0.0f,0.0f,0.1f);
	//else 
	glClearColor(1.0f,1.0f,0.5f,0.1f);	
	//pomocna kolekce pro porovnavani vzdalenosti
	vector<GameObject*> objects;
	
	for (int i=0;i < data.enemyList.size()+1;i++)
	{		
		if (i==0)
		{		
			objects.push_back(new Player());
			objects.at(i)->position.x = data.player->position.x;
			objects.at(i)->position.y = data.player->position.y;			
			objects.at(i)->radius = data.player->radius;	
			objects.at(i)->speed.x = data.player->speed.x;
			objects.at(i)->speed.y = data.player->speed.y;
		} 
		else
		{
			objects.push_back(new Enemy());
			objects.at(i)->radius = data.enemyList.at(i-1)->radius;			
			objects.at(i)->position.x = data.enemyList.at(i-1)->position.x;
			objects.at(i)->position.y = data.enemyList.at(i-1)->position.y;
			objects.at(i)->speed.x = data.enemyList.at(i-1)->speed.x;
			objects.at(i)->speed.y = data.enemyList.at(i-1)->speed.y;
		}		
	}

	
	//kontorla vzdalenosti objektu
	for (int i=0;i < objects.size()-1;i++)
	{
		for (int j = i+1; j < objects.size();j++)
		{
			distanceChecker(objects.at(i),objects.at(j));
		}
	}

	//aktualizace objektu v kolekci data
	for (int i=0;i < objects.size();i++)
	{
		if (i==0)
		{	
			data.player->position.x = objects.at(i)->position.x;
			data.player->position.y = objects.at(i)->position.y;			
			data.player->radius = objects.at(i)->radius;	
			data.player->speed.x = objects.at(i)->speed.x;
			data.player->speed.y = objects.at(i)->speed.y;
		} 
		else
		{

			data.enemyList.at(i-1)->radius = objects.at(i)->radius;			
			data.enemyList.at(i-1)->position.x = objects.at(i)->position.x;
			data.enemyList.at(i-1)->position.y = objects.at(i)->position.y;
			data.enemyList.at(i-1)->speed.x = objects.at(i)->speed.x;
			data.enemyList.at(i-1)->speed.y = objects.at(i)->speed.y;
		}		
	}

	//vykresleni nepratel
	for (int i=0;i<data.enemyList.size();i++)
	{
		if (data.enemyList.at(i)->radius <= data.player->radius)
			glColor3f(0.0f, 1.0f, 0.0f);		
		else glColor3f(1.0f, 0.0f, 0.0f);		

		data.enemyList.at(i)->draw();
	}

	data.player->draw();// vykreslení hráèe
	
}

void Game::mouseMoved(const Event& e){
	
}
void Game::mouseClicked(int x,int y){
	

}

Game::~Game(){

}

void Game::distanceChecker(GameObject *first,GameObject *second){
	float circleDistance = sqrt(pow(first->position.x - second->position.x,2)+pow(first->position.y - second->position.y,2));
	float firstArea,secondArea;
	firstArea = M_PI*pow(first->radius,2);
	secondArea = M_PI*pow(second->radius,2);

	if (circleDistance < first->radius + second->radius)
	{
		float vyskaUsece,obsahUsece,obsahPruniku;
//		float koeficient = 2.0;
		if (firstArea > secondArea)
		{
			vyskaUsece = first->radius - (circleDistance - second->radius);
			obsahUsece = pow(first->radius,2) * acos((first->radius - vyskaUsece)/first->radius)-(first->radius - vyskaUsece)*sqrt( (2*vyskaUsece*first->radius) - pow(vyskaUsece,2) );
 			obsahPruniku = 2*obsahUsece;
			firstArea += obsahPruniku; 
 			secondArea -= obsahPruniku;
		} 

 		else
 		{
			vyskaUsece = second->radius - (circleDistance - first->radius);
			obsahUsece = pow(second->radius,2) * acos((second->radius - vyskaUsece)/second->radius)-(second->radius - vyskaUsece)*sqrt( (2*vyskaUsece*second->radius) - pow(vyskaUsece,2) );
			obsahPruniku = 2*obsahUsece;
			secondArea += obsahPruniku;
 			firstArea -= obsahPruniku; // (circleDistance / 20);
 		}

		first->radius = sqrt((firstArea/M_PI));		
		second->radius = sqrt((secondArea/M_PI));

		if(first->radius <= 0.0) first->radius = 0.01;
		if(second->radius <= 0.0) second->radius = 0.01;
	}
	

	
}
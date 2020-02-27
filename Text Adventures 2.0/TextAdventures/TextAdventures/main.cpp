//Michael Hunsaker
//05/16/2017 Start date
/*The idea of this program is to be a text based video game
  set in an early time period highest tech crossbow an adventure 
  game with main and side quests to be just like any other modern
  video game except graphically null
  
  Things needed to be done:

  *********************************************
  Remove options from char stats put them into chugachuga 
  chugachuga should never lose control
  DO NOT SEND CONTROL AWAY FROM CHUGACHUGA UNLESS TO A DIFFERENT FUNCTION IN ENGINE
  *********************************************

  Make a working map and a way to move around it             Almost done 05/17/17 complete 05/18 
	********05/18/2017	:new objective create minimap for standard gameplay making full map an option to enter rather than a full time thing complete 05/19
  Make an inventory system and items to go into it           
  Make character sheets and assign attributes to them        Started     05/17/17 complete 05/19
  Make a few basic monsters to fight
  in the near future:
  skill tree, classes, spells,
  expand monster database to have a large array of enemies with various skills and abilities
  implement a story line and side quests
  in the far future:
  file saving character data, map data and inventory data
  add text based animations
  And finally get greenlit*/
//msvisualstudio
#include <iostream>
#include <string>
#include <iomanip>
#include <fstream>
#include <ctime>


using namespace std;

class monster
{private:
	struct monsters
	{
		string monname;
		double
			monhp,
			monmana,
			monmedmg,
			monradmg,
			monmadmg,
			mondodgechance,
			monphysresis,
			monmagresis,
			moncritchance,
			monexpval,
			monlevel,
			monlevelmodhp,
			monlevelmodmana,
			monlevelmodmedmg,
			monlevelmodradmg,
			monlevelmodmadmg,
			monlevelmoddodgechance,
			monlevelmodphysresis,
			monlevelmodmagresis,
			monlevelmodcritchance,
			monlevelmodexpval,
			monmaxlevel;
	}monsternum[5];
public:
	void initiatemons()
	{
		monsternum[1].monname = "Bandit";
		monsternum[1].monhp = 80;
		monsternum[1].monmana = 20;
		monsternum[1].monmedmg = 30;
		monsternum[1].monradmg = 30;
		monsternum[1].monmadmg = 0;
		monsternum[1].mondodgechance = .001;
		monsternum[1].monphysresis = .05;
		monsternum[1].monmagresis = .05;
		monsternum[1].moncritchance = .05;
		monsternum[1].monlevel = 1;
		monsternum[1].monmaxlevel = 25;
		monsternum[1].monexpval = 25;
		///////////////////////Base stats^^^^^//levelmodifyer\/\/\/\/\/\/
		monsternum[1].monlevelmodhp = 1.1;
		monsternum[1].monlevelmodmana = 1.1;
		monsternum[1].monlevelmodmedmg = 1.08;
		monsternum[1].monlevelmodradmg = 1.08;
		monsternum[1].monlevelmodmadmg = 1.08;
		monsternum[1].monlevelmoddodgechance = .001;
		monsternum[1].monlevelmodphysresis = .05;
		monsternum[1].monlevelmodmagresis = .05;
		monsternum[1].monlevelmodcritchance = .003;
		monsternum[1].monlevelmodexpval = 9;

	}
	void  mongenerate(string &mobname, double &mobhp, double &mobmana, double &mobmedmg, double& mobradmg, double& mobmadmg, double& 
		mobdodgechance, double& mobphysresis, double& mobmagresis, double& mobcritchance, double& moblevel, double& mobexpval )
	{
		srand(static_cast<unsigned int>(time(NULL)));
		int num = NULL;
		int level;
		num = 1; //+ rand() % 1; random monster gen for future
		level = 1 + rand() % 25;
		monsternum[num].monhp *= monsternum[num].monlevelmodhp * level;
		monsternum[num].monmana *= monsternum[num].monlevelmodmana * level;
		monsternum[num].monmedmg *= monsternum[num].monlevelmodmedmg * level;
		monsternum[num].monradmg *= monsternum[num].monlevelmodradmg * level;
		monsternum[num].monmadmg *= monsternum[num].monlevelmodmadmg * level;
		monsternum[num].mondodgechance += monsternum[num].monlevelmoddodgechance * level;
		monsternum[num].monphysresis += monsternum[num].monlevelmodphysresis * level;
		monsternum[num].monmagresis += monsternum[num].monlevelmodmagresis * level;
		monsternum[num].moncritchance += monsternum[num].monlevelmodcritchance * level;
		monsternum[num].monlevel = level;
		monsternum[num].monexpval = monsternum[num].monlevelmodexpval * level;

		mobhp = monsternum[num].monhp;
		mobmana = monsternum[num].monmana;
		mobmedmg = monsternum[num].monmedmg;
		mobradmg = monsternum[num].monradmg;
		mobmadmg = monsternum[num].monmadmg;
		mobdodgechance = monsternum[num].mondodgechance;
		mobphysresis = monsternum[num].monphysresis;
		mobmagresis = monsternum[num].monmagresis;
		mobcritchance =  monsternum[num].moncritchance;
		moblevel  = monsternum[num].monlevel;
		mobexpval = monsternum[num].monexpval;
		mobname = monsternum[num].monname;
	}
};

class character
{
private:
	//attributes
	double attributes[6];
	string attributename[6];
	string attributedesc[6];
	string charactername;
	//Base stats
	double bhealth, bmana, bmedmg, bradmg, bmadmg,
		bdodgchnce, bphysresis, bmagresis, bhpregen, bmanaregen, bcritchance,

		skillpnts, attrpnts, currexp, needexp, maxlevel, currlevel, armor,

		ahealth, amana, amedmg, aradmg, amadmg, adodgchnce,
		aphysresis, amagresis, ahpregen, amanaregen, acritchance,

		hpmod, manamod, medmgmod, radmgmod, madmgmod, dodgchncemod,
		physresismod, magresismod, hpregenmod, manaregenmod, critchacemod,

		chpmod, cmanamod, cmedmgmod, cradmgmod, cmadmgmod, cdodgchncemod,
		cphysresismod, cmagresismod, chpregenmod, cmanaregenmod, ccritchance,

		currhealth, currmana,
		
		damage;

	int maxinven, skillflag, skillswitch;
	int currskills[5];
public:
	void initiateattributes()
	{
		string locattname[6] = { "Strength", "Perception", "Intelligence", "Agillity", "Endurance", "Wisdom" };
		string locattdesc[6] = { "Effects mellee damage, and strength based item requirements",
			"Effects ranged damage, and luck",
			"Effects magic damage, and magic resistance",
			"Effects ability to dodge, and critical hit chance",
			"Effects health, base physical resistance, and health regen",
			"Effects mana pool, and mana regen" };
		for (int r = 0; r < 6; r++)
		{
			attributename[r] = locattname[r];
			attributedesc[r] = locattdesc[r];
		}
	}
	void charcreate()
	{
		initiateattributes();
		maxinven = 30;
		for (int c = 0; c < 6; c++)
		{
			attributes[c] = 0;
		}
		cout << "Enter character name(single word): ";
		cin >> charactername;
		cin.clear(); cin.ignore(INT_MAX, '\n');
		system("cls");
		bhealth = 100;
		bmana = 50;
		skillpnts = 1;
		attrpnts = 5;
		bmedmg = 3;
		bradmg = 3;
		bmadmg = 3;
		bdodgchnce = .001;
		bphysresis = .05;
		bmagresis = .05;
		bhpregen = 20;
		bmanaregen = 20;
		bcritchance = .01;
		needexp = 300;
		currexp = 0;
		maxlevel = 25;
		currlevel = 1;
		armor = 0;
		currhealth = bhealth;
		currmana = bmana;
		activestatsupdate();
		starthpandmana();
	}
	void charlevelup()
	{
		if (currexp >= needexp)
		{
			bhealth *= 1.1;
			bmana *= 1.05;
			skillpnts += 1;
			attrpnts += 5;
			bmedmg *= 1.08;
			bradmg *= 1.08;
			bmadmg *= 1.08;
			bdodgchnce += .001;
			bphysresis += .05;
			bmagresis += .05;
			bhpregen += 7.5;
			bmanaregen += 7.5;
			bcritchance + .003;
			currhealth = ahealth;
			currmana = amana;
			currexp -= needexp;
			needexp *= 1.1;
			currlevel++;
		}
	}
	void attributepage()
	{
		cout << setw(13) << attributename[0] << ": " << attributes[0] << endl;
		cout << setw(13) << attributename[1] << ": " << attributes[1] << endl;
		cout << setw(13) << attributename[2] << ": " << attributes[2] << endl;
		cout << setw(13) << attributename[3] << ": " << attributes[3] << endl;
		cout << setw(13) << attributename[4] << ": " << attributes[4] << endl;
		cout << setw(13) << attributename[5] << ": " << attributes[5] << endl;
	}
	
	double getattpnts()
	{
		return attrpnts;
	}
	void addstrength()
	{
		attributes[0]++;
		attrpnts--;
		activestatsupdate();
	}
	void addperception()
	{
		attributes[1] += 1;
		attrpnts--;
		activestatsupdate();
	}
	void addintelligence()
	{
		attributes[2] += 1;
		attrpnts--;
		activestatsupdate();
	}
	void addAgility()
	{
		attributes[3] += 1;
		attrpnts--;
		activestatsupdate();
	}
	void addendurance()
	{
		attributes[4] += 1;
		attrpnts--;
		activestatsupdate();
	}
	void addWisdom()
	{
		attributes[5] += 1;
		attrpnts--;
		activestatsupdate();
	}
	void hpandmanaregen()
	{
		currhealth += ahpregen;
		currmana += amanaregen;
		if (currhealth > ahealth) { currhealth = ahealth; }
		if (currmana > amana) { currmana = amana; }
	}
	void addattributes()
	{
		system("cls");
			cout << setw(13) << attributename[0] << ": " << attributes[0] << "  :1" << endl;
			cout << setw(13) << attributename[1] << ": " << attributes[1] << "  :2" << endl;
			cout << setw(13) << attributename[2] << ": " << attributes[2] << "  :3" << endl;
			cout << setw(13) << attributename[3] << ": " << attributes[3] << "  :4" << endl;
			cout << setw(13) << attributename[4] << ": " << attributes[4] << "  :5" << endl;
			cout << setw(13) << attributename[5] << ": " << attributes[5] << "  :6" << endl;
			cout << "Unused attribute points: " << attrpnts << endl;
			cout << "Too add one point to an attribute enter the corrosponding number. To exit enter 7\n:";
	}
	void learnattributes()
	{
		system("cls");
		cout << setw(13) << attributename[0] << ": " << setw(4) << attributedesc[0] << endl;
		cout << setw(13) << attributename[1] << ": " << setw(4) << attributedesc[1] << endl;
		cout << setw(13) << attributename[2] << ": " << setw(4) << attributedesc[2] << endl;
		cout << setw(13) << attributename[3] << ": " << setw(4) << attributedesc[3] << endl;
		cout << setw(13) << attributename[4] << ": " << setw(4) << attributedesc[4] << endl;
		cout << setw(13) << attributename[5] << ": " << setw(4) << attributedesc[5] << endl;
		system("pause");
	}
	void charstats()
	{
		activestatsupdate();
		cout << setw(22) << charactername << setw(19) << " | |" << setw(15) << "   Armor: " << setw(4)<< armor << endl;
		cout << "Health             : " << setw(7) << currhealth << "/" << setw(7) << ahealth << setw(4) << "  | |" << setw(13) << attributename[0] << ": " << setw(4) << attributes[0] << endl;
		cout << "Mana               : " << setw(7) << currmana << "/" << setw(7) << amana << setw(4) << "  | |" << setw(13) << attributename[1] << ": " << setw(4) << attributes[1] << endl;
		cout << "Mellee damage      : " << setw(15) << amedmg << setw(4) << "  | |" << setw(13) << attributename[2] << ": " << setw(4) << attributes[2] << endl;
		cout << "Ranged damage      : " << setw(15) << aradmg << setw(4) << "  | |" << setw(13) << attributename[3] << ": " << setw(4) << attributes[3] << endl;
		cout << "Magic damage       : " << setw(15) << amadmg << setw(4) << "  | |" << setw(13) << attributename[4] << ": " << setw(4) << attributes[4] << endl;
		cout << "Dodge chance       : " << setw(15) << adodgchnce * 10 << "%" << setw(4) << "| |" << setw(13) << attributename[5] << ": " << setw(4) << attributes[5] << endl;
		cout << "Physical resistance: " << setw(15) << aphysresis * 10 << "%" << setw(4) << "| |" << setw(13) << "   Experience: " << currexp << "/" << needexp << endl;
		cout << "Magical resistance : " << setw(15) << amagresis * 10 << "%" << setw(4) << "| |" << "Current level: " << currlevel << endl;
		cout << "Health regeneration: " << setw(15) << ahpregen << setw(4) << "  | |" << setw(20) << "Available attribute points: " << attrpnts << endl;
		cout << "Mana regeneration  : " << setw(15) << amanaregen << setw(4) << "  | |" << setw(20) << "Available skill points    : " << skillpnts << endl;
		cout << "Critical hit chance: " << setw(15) << acritchance * 10 << "%" << setw(4) << " | |" << endl;

	}

	void activestatsupdate()
	{
		hpmod = 1.04 * attributes[4];
		manamod = 1.04 * attributes[5];
		medmgmod = 1.02 * attributes[0];
		radmgmod = 1.02 * attributes[1];
		madmgmod = 1.02 * attributes[2];
		dodgchncemod = .003 * attributes[3];
		physresismod = .02 * attributes[4];
		magresismod = .02 * attributes[2];
		hpregenmod = 5 * attributes[4];
		manaregenmod = 4 * attributes[5];
		critchacemod = .005 * attributes[3];
		if (hpmod == 0) { hpmod = 1; }
		if (manamod == 0) { manamod = 1; }
		if (medmgmod == 0) { medmgmod = 1; }
		if (radmgmod == 0) { radmgmod = 1; }
		if (madmgmod == 0) { madmgmod = 1; }
		if (dodgchncemod == 0) { dodgchncemod = 1; }
		if (physresismod == 0) { physresismod = 1; }
		if (magresismod == 0) { magresismod = 1; }
		if (hpregenmod == 0) { hpregenmod = 1; }
		if (manaregenmod == 0) { manaregenmod = 1; }
		if (critchacemod == 0) { critchacemod = 1; }
		ahealth = bhealth * hpmod;
		amana = bmana * manamod;
		amedmg = bmedmg * medmgmod;
		aradmg = bradmg * radmgmod;
		amadmg = bmadmg * madmgmod;
		adodgchnce = bdodgchnce + dodgchncemod;
		aphysresis = bphysresis + physresismod;
		amagresis = bmagresis + magresismod;
		ahpregen = bhpregen * hpregenmod;
		amanaregen = bmanaregen * manaregenmod;
		acritchance = bcritchance + critchacemod;

	}
	void gainexp(double value)
	{
		currexp += value;
		charlevelup();
	}
	void charinventory()
	{
		struct inventory
		{
			string itemname;

		};
	}
	void  starthpandmana()
	{
		currhealth = ahealth;
		currmana = amana;
	}
	/////////////////////////////////////////////////////////////////////////////////////////////
	///////////////////Character skills//////////////////////////////////////////////////////////
	/////////////////////////////////////////////////////////////////////////////////////////////

	struct skills
	{
		string skillname, lockedskillname, skilldesc;
		double skillnumber, skilllevel, maxskilllevel;
		bool unlocked;
	}skillnum[9];
	
	double getskillpnts()
	{
		return skillpnts;
	}
	void initskills()
	{
		skillnum[0].skillname = "Mellee Attack";
		skillnum[0].lockedskillname = "Mellee Attack(locked)";
		skillnum[0].skilldesc = "A basic mellee attack that does exactly the amout of mellee damage you have.";
		skillnum[0].skillnumber = 1;
		skillnum[0].skilllevel = 1;
		skillnum[0].maxskilllevel = 1;
		skillnum[0].unlocked = true;

		skillnum[1].skillname = "Ranged Attack";
		skillnum[1].lockedskillname = "Ranged Attack(locked)";
		skillnum[1].skilldesc = "A basic ranged attack that does exactly the amout of ranged damage you have.";
		skillnum[1].skillnumber = 2;
		skillnum[1].skilllevel = 1;
		skillnum[1].maxskilllevel = 1;
		skillnum[1].unlocked = true;

		skillnum[2].skillname = "Fire ball";
		skillnum[2].lockedskillname = "Fire ball(locked)";
		skillnum[2].skilldesc = "A basic magic attack that does exactly the amout of magic damage you have.";
		skillnum[2].skillnumber = 3;
		skillnum[2].skilllevel = 1;
		skillnum[2].maxskilllevel = 1;
		skillnum[2].unlocked = true;

		skillnum[3].skillname = "Heavy Strike";
		skillnum[3].lockedskillname = "Heavy Strike(locked)";
		skillnum[3].skilldesc = "An extra strong mellee attack that does 50% extra damage, every additional point adds 20%.";
		skillnum[3].skillnumber = 4;
		skillnum[3].skilllevel = 0;
		skillnum[3].maxskilllevel = 5;
		skillnum[3].unlocked = false;

		skillnum[4].skillname = "Charged shot";
		skillnum[4].lockedskillname = "Charged shot(locked)";
		skillnum[4].skilldesc = "An extra strong ranged attack that does 50% extra damage, every additional point adds 20%.";
		skillnum[4].skillnumber = 5;
		skillnum[4].skilllevel = 0;
		skillnum[4].maxskilllevel = 5;
		skillnum[4].unlocked = false;

		skillnum[5].skillname = "Frost bolt";
		skillnum[5].lockedskillname = "Frost bolt(locked)";
		skillnum[5].skilldesc = "An extra strong magic attack that does 50% extra damage, every additional point adds 20%.";
		skillnum[5].skillnumber = 6;
		skillnum[5].skilllevel = 0;
		skillnum[5].maxskilllevel = 5;
		skillnum[5].unlocked = false;

		skillnum[6].skillname = "Flee";
		skillnum[6].lockedskillname = "Flee(locked)";
		skillnum[6].skilldesc = "Attemp to run for your life because it depends upon it.";
		skillnum[6].skillnumber = 7;
		skillnum[6].skilllevel = 1;
		skillnum[6].maxskilllevel = 1;
		skillnum[6].unlocked = true;

		currskills[0] = 0;
		currskills[1] = 1;
		currskills[2] = 2;
		currskills[3] = 7;
		currskills[4] = 7;
	}
	void addskills()
	{
		system("cls");
		cout << "Skills" << endl;
		cout << "Too learn a skill enter the skill number too exit enter 0" << endl;
		for (int n = 0; n <= 6; n++)
		{
				cout << skillnum[n].skillname << "     " << skillnum[n].skilllevel << "/" << skillnum[n].maxskilllevel << endl;
				cout << "Skill number: " << skillnum[n].skillnumber << endl;
				cout << skillnum[n].skilldesc << endl;
				if (skillnum[n].unlocked == true) { cout << "Unlocked" << endl; }
				if (skillnum[n].unlocked == false) { cout << "Locked" << endl; }
				cout << endl;
		}
	}
	void skillslot1()
	{
		currskills[0] = skillswitch;
	}
	void skillslot2()
	{
		currskills[1] = skillswitch;
	}
	void skillslot3()
	{
		currskills[2] = skillswitch;
	}
	void skillslot4()
	{
		currskills[3] = skillswitch;
	}
	void skillswitchfunc(int skill)
	{
		skillswitch = skill;
	}
	void skillselect()
	{
		system("cls");
		cout << "Skills" << endl;
		cout << "Too selct a skill, enter the skill slot you would like to change, and then enter the skill number. 5 too exit" << endl;
		for (int n = 0; n <= 6; n++)
		{
			if (skillnum[n].unlocked == true)
			{
				cout << skillnum[n].skillname << "     " << skillnum[n].skilllevel << "/" << skillnum[n].maxskilllevel << endl;
				cout << "Skill number: " << skillnum[n].skillnumber << endl;
				cout << skillnum[n].skilldesc << endl;
				cout << endl;
			}
		}
		cout << "Currently selected skills: " << endl;
		cout << "Slot one  :" << skillnum[currskills[0]].skillname << endl;
		cout << "Slot two  :" << skillnum[currskills[1]].skillname << endl;
		cout << "Slot three:" << skillnum[currskills[2]].skillname << endl;
		cout << "Slot four :" << skillnum[currskills[3]].skillname << endl;
		cout << "Slot five :" << skillnum[currskills[4]].skillname << endl;

	}
	void learnmelleeattack()
	{
		if (skillnum[0].unlocked == false) { skillnum[0].unlocked = true; }
		if (skillnum[0].maxskilllevel > skillnum[0].skilllevel)
		{
			skillnum[0].skilllevel += 1;
			skillpnts -= 1;
		}
		else if (skillnum[0].maxskilllevel <= skillnum[0].skilllevel)
		{
			skillflag = 1;
		}
	}
	void learnrangedattack()
	{
		if (skillnum[1].unlocked == false) { skillnum[1].unlocked = true; }
		if (skillnum[1].maxskilllevel > skillnum[1].skilllevel)
		{
			skillnum[1].skilllevel += 1;
			skillpnts -= 1;
		}
		else if (skillnum[1].maxskilllevel <= skillnum[1].skilllevel)
		{
			skillflag = 1;
		}
	}
	void learnfireball()
	{
		if (skillnum[2].unlocked == false) { skillnum[2].unlocked = true; }
		if (skillnum[2].maxskilllevel > skillnum[2].skilllevel)
		{
			skillnum[2].skilllevel += 1;
			skillpnts -= 1;
		}
		else if (skillnum[2].maxskilllevel <= skillnum[2].skilllevel)
		{
			skillflag = 1;
		}
	}
	void learnheavyStrike()
	{
		if (skillnum[3].unlocked == false) { skillnum[3].unlocked = true; }
		if (skillnum[3].maxskilllevel > skillnum[3].skilllevel)
		{
			skillnum[3].skilllevel += 1;
			skillpnts -= 1;
		}
		else if (skillnum[3].maxskilllevel <= skillnum[3].skilllevel)
		{
			skillflag = 1;
		}
	}
	void learnchargedshot()
	{
		if (skillnum[4].unlocked == false) { skillnum[4].unlocked = true; }
		if (skillnum[4].maxskilllevel > skillnum[4].skilllevel)
		{
			skillnum[4].skilllevel += 1;
			skillpnts -= 1;
		}
		else if (skillnum[4].maxskilllevel <= skillnum[4].skilllevel)
		{
			skillflag = 1;
		}
	}
	void learnfrostbolt()
	{
		if (skillnum[5].unlocked == false) { skillnum[5].unlocked = true; }
		if (skillnum[5].maxskilllevel > skillnum[5].skilllevel)
		{
			skillnum[5].skilllevel += 1;
			skillpnts -= 1;
		}
		else if (skillnum[5].maxskilllevel <= skillnum[5].skilllevel)
		{
			skillflag = 1;
		}
	}
	int getskillflag()
	{
		return skillflag;
	}
	void skillflagreset()
	{
		skillflag = 0;
	}
	void charskills()
	{
		system("cls");
		cout << "Skills" << endl;
		for (int n = 0; n <= 6; n++)
		{
			if (skillnum[n].unlocked == true)
			{
				cout << skillnum[n].skillname << "     " << skillnum[n].skilllevel << "/" << skillnum[n].maxskilllevel << endl;
				cout << "Skill number: " << skillnum[n].skillnumber << endl;
				cout << skillnum[n].skilldesc << endl;
				cout << endl;
			}
		}
		cout << "Currently selected skills: " << endl;
		cout << "Slot one  : " << skillnum[currskills[0]].skillname << endl;
		cout << "Slot two  : " << skillnum[currskills[1]].skillname << endl;
		cout << "Slot three: " << skillnum[currskills[2]].skillname << endl;
		cout << "Slot four : " << skillnum[currskills[3]].skillname << endl;
		cout << "Slot five : " << skillnum[currskills[4]].skillname << endl;
	}

	void melleeattack()
	{
		damage = amedmg;
	}
	void rangedattack()
	{
		damage = aradmg;
	}
	void fireball()
	{
		damage = amadmg;
	}
	void heavystrike()
	{
		damage = amedmg *(1.5+(.2*skillnum[3].skilllevel));
	}
	void chargedshot()
	{
		damage = aradmg *(1.5 + (.2*skillnum[4].skilllevel));
	}
	void frostbolt()
	{
		damage = amadmg *(1.5 + (.2*skillnum[3].skilllevel));
	}
	/////////////////////////////////////////////////////////////////////////////////////////////
	///////////////////Character skills//////////////////////////////////////////////////////////
	/////////////////////////////////////////////////////////////////////////////////////////////
	void perktree()
	{
	}
	void getcharacterstats(double &charhp, double &charmana, double &charmedmg, double &charradmg, double &charmadmg,
		double &chardodgechance, double &charphysresis, double &charmagresis, double &charcritchance, double &maxhp, double &maxmana, string &charname)
	{
		charhp = currhealth;
		charmana = currmana;
		charmedmg = amedmg;
		charradmg = aradmg;
		charmadmg = amadmg;
		chardodgechance = adodgchnce;
		charphysresis = aphysresis;
		charmagresis = amagresis;
		charcritchance = acritchance;
		maxhp = ahealth;
		maxmana = amana;
		charname = charactername;
	}
	void updatehpmanapostcombat(double &charhp, double &charmana )
	{
		currhealth = charhp;
		currmana = charmana;
	}
	/*character attributes and what they are supposed to do:
	Strength: effects mellee damage, and str based item requirements
	Perception: effects ranged damage, and something that equates to luck
	Intelligence: effects magic damage and magic resistance
	Agility: effects ability to dodge, and crits
	Endurance: effects health, and base resistance health regen
	Wisdom: effects mana pool mana regen
	***********************************************************
	level ups effects stats in this way:
	base health + 3%
	base mana + 2%
	mellee attack + 5
	ranged attack + 5
	magic attack + 5
	Base physical resistance + .05%
	base magic resistance + .05%
	health regen + 7.5
	mana regen + 7.5
	stat points + 3
	skill points + 1
	dodge chance + .1%
	critchance +.3%
	exp needed for next level + 10%
	***********************************************************
	attributes will effect base in this way for each point:
	Strength: base mellee attack by + 2%
	Perception: base Ranged damage + 2% luck + .05%
	Intelligence: base magic damage + 2% base magic resistance + 2%
	Agility: dodge + .03% crits + .05%
	Endurance: Health + 4% base physical resistance + 2% hp regen +20
	Wisdom: Mana pool + 4% and mana regen + 20
	***********************************************************
	Base stats will be:
	Health:       100
	mana:         50
	skill points: 1
	attribute points:  5
	base mellee attack 3
	base ranged attack 3
	base magic attack 3
	base dodge chance 0.01%
	Base physical damage resistance 5%
	base magic damage resistance 5%
	health regen 20
	mana regen 20
	critchance 1.0%
	*/
};
class map
{
private:
	int currposvert, currposhor;
	int biome[25][25];
	int  numofbiomes;
	string maptype[12];
	string currmaptype[12];
	string mystery;
	int discovered[25][25];
public:
	void mapgen()
	{
		string maptypelocal[12] = { "null0" ,"null1", "null2","null3","Forest", "Plains", "Marsh", "Desert", "Jungle", "Artic", "Dungeon", "Town" };
		string currmaptypelocal[12] = { "null0" ,"null1", "null2","null3","*Forest*", "*Plains*", "*Marsh*", "*Desert*", "*Jungle*", "*Artic*", "*Dungeon*", "*Town*" };
		currposvert = 1, currposhor = 1;
		numofbiomes = 8;
		mystery = "?????";
		srand(static_cast<unsigned int>(time(NULL)));
		for (int r = 1; r <= 11; r++)
		{
			maptype[r] = maptypelocal[r];
			currmaptype[r] = currmaptypelocal[r];
		}

		for (int c = 1; c <= 25; c++)
		{
			for (int r = 1; r <= 25; r++)
			{
				biome[r][c] = 4 + rand() % 7;
				discovered[r][c] = 0;
			}
		}
		biome[1][1] = 11;
		discovered[1][1] = 1;
	}
	void printdiscovered()
	{
		for (int r = 1; r <= 25; r++)
		{
			for (int c = 1; c <= 25; c++)
			{
				cout << setw(9) << discovered[c][r];
			}
			cout << endl;
		}
		system("pause");
	}
	void minimap()
	{
		/*$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
		THIS IS THE BEFORE MOVING PAST THREE SECTION!!
		$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$*/
		if (currposhor <= 3 && currposvert <= 3)
		{
			////////////////////////////////////////////
			/////Sets numbers acrossed the top//////////
			////////////////////////////////////////////
			for (int num = 1; num <= 5; num++)
			{
				cout << setw(9) << num;
			}
			cout << endl;
			//***************************************///
			//sets number along left side of map*****///
			//***************************************///
			for (int r = 1; r <= 5; r++)
			{
				cout << setw(3) << r;
				/////////////////////////////////////////////
				//Prints map/////////////////////////////////
				for (int c = 1; c <= 5; c++)
				{
					if (discovered[c][r] == 1)
					{

						if (currposhor == c && currposvert == r)
						{
							cout << setw(9) << currmaptype[biome[c][r]];
						}
						else
							cout << setw(9) << maptype[biome[c][r]];

					}
					else if (discovered[c][r] == 0)
					{
						cout << setw(9) << mystery;
					}
				}
				cout << endl;
				cout << endl;
			}
		}
		/*$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
		THIS IS THE AFTER MOVING PAST THREE In both directions SECTION!!
		$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$*/
		if (currposvert > 3 && currposhor > 3)
		{
			////////////////////////////////////////////
			/////Sets numbers acrossed the top of mini//////////
			////////////////////////////////////////////
			for (int num = currposhor - 2; num <= currposhor + 2; num++)
			{
				cout << setw(9) << num;
			}
			cout << endl;
			//***************************************///
			//sets number along left side of mini map*****///
			//***************************************///
			for (int r = currposvert - 2; r <= currposvert + 2; r++)
			{
				cout << setw(3) << r;
				/////////////////////////////////////////////
				//Prints mini map/////////////////////////////////
				for (int c = currposhor - 2; c <= currposhor + 2; c++)
				{
					if (discovered[c][r] == 1)
					{
						if (currposhor == c && currposvert == r)
						{
							cout << setw(9) << currmaptype[biome[c][r]];
						}
						else
							cout << setw(9) << maptype[biome[c][r]];

					}
					else if (discovered[c][r] == 0)
					{
						cout << setw(9) << mystery;
					}
				}
				cout << endl;
				cout << endl;
			}
		}
		/*$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
		THIS IS THE AFTER MOVING PAST THREE vertically SECTION!!
		$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$*/
		else if (currposvert > 3 && currposhor <= 3)
		{
			////////////////////////////////////////////
			/////Sets numbers acrossed the top//////////
			////////////////////////////////////////////
			for (int num = 1; num <= 5; num++)
			{
				cout << setw(9) << num;
			}
			cout << endl;
			//***************************************///
			//sets number along left side of map*****///
			//***************************************///
			for (int r = currposvert - 2; r <= currposvert + 2; r++)
			{
				cout << setw(3) << r;
				/////////////////////////////////////////////
				//Prints map/////////////////////////////////
				for (int c = 1; c <= 5; c++)
				{
					if (discovered[c][r] == 1)
					{
						if (currposhor == c && currposvert == r)
						{
							cout << setw(9) << currmaptype[biome[c][r]];
						}
						else
							cout << setw(9) << maptype[biome[c][r]];

					}
					else if (discovered[c][r] == 0)
					{
						cout << setw(9) << mystery;
					}
				}
				cout << endl;
				cout << endl;
			}
		}
		/*$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
		THIS IS THE AFTER MOVING PAST THREE horizontically SECTION!!
		$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$*/
		else if (currposhor > 3 && currposvert <= 3)
		{
			////////////////////////////////////////////
			/////Sets numbers acrossed the top//////////
			////////////////////////////////////////////
			for (int num = currposhor - 2; num <= currposhor + 2; num++)
			{
				cout << setw(9) << num;
			}
			cout << endl;
			//***************************************///
			//sets number along left side of map*****///
			//***************************************///
			for (int r = 1; r <= 5; r++)
			{
				cout << setw(3) << r;
				/////////////////////////////////////////////
				//Prints map/////////////////////////////////
				for (int c = currposhor - 2; c <= currposhor + 2; c++)
				{
					if (discovered[c][r] == 1)
					{
						if (currposhor == c && currposvert == r)
						{
							cout << setw(9) << currmaptype[biome[c][r]];
						}
						else
							cout << setw(9) << maptype[biome[c][r]];

					}
					else if (discovered[c][r] == 0)
					{
						cout << setw(9) << mystery;
					}
				}
				cout << endl;
				cout << endl;
			}
		}
		
	}
	void printmap()
	{
		int vertnum = 1;
		////////////////////////////////////////////
		/////Sets numbers acrossed the top//////////
		////////////////////////////////////////////
		for (int num = 1; num <= 25; num++)
		{
			cout << setw(9) << num;
		}
		cout << endl;
		//***************************************///
		//sets number along left side of map*****///
		//***************************************///
		for (int r = 1; r <= 25; r++)
		{
			cout << setw(3) << vertnum;
			vertnum++;

			/////////////////////////////////////////////
			//Prints map/////////////////////////////////
			for (int c = 1; c <= 25; c++)
			{
				if (discovered[c][r] == 1)
				{
					if (currposhor == c && currposvert == r)
					{
						cout << setw(9) << currmaptype[biome[c][r]];
					}
					else
						cout << setw(9) << maptype[biome[c][r]];
				}
				else if (discovered[c][r] == 0)
				{
					cout << setw(9) << mystery;
				}
			}
			cout << endl;
			cout << endl;
		}
		system("pause");
	}
	void moveup()
	{
		currposvert--;
		cout << "You have entered a " << maptype[biome[currposvert][currposhor]];
	}
	void movedown()
	{
		currposvert++;
		cout << "You have entered a " << maptype[biome[currposvert][currposhor]];
	}
	void moveleft()
	{
		currposhor--;
		cout << "You have entered a " << maptype[biome[currposvert][currposhor]];
	}
	void moveright()
	{
		currposhor++;
		cout << "You have entered a " << maptype[biome[currposvert][currposhor]];
	}
	void whereami()
	{
		cout << "You are in a " << maptype[biome[currposvert][currposhor]] << "(" << currposvert << "," << currposhor << ")" << endl;
	}
	void checkborder(char &option, int &valid2)
	{
		if (option == 'w' && currposvert == 1)
		{
			valid2 = 1;
			option = 'Z';
		}
		else if (option == 's' && currposvert == 25)
		{
			valid2 = 2;
			option = 'Z';
		}
		else if (option == 'a' && currposhor == 1)
		{
			valid2 = 3;
			option = 'Z';
		}
		else if (option == 'd' && currposhor == 25)
		{
			valid2 = 4;
			option = 'Z';
		}
	}
	void discovery(int &flag)
	{
		if (discovered[currposhor][currposvert] == 0) { flag++; }
		discovered[currposhor][currposvert] = 1;
	}

};
class engine:public character, map, monster
{
private:
	int somthingprivate;
	double 
		mobhp, mobmana, mobmedmg, mobradmg, mobmadmg,
		mobdodgechance, mobphysresis, mobmagresis, mobcritchance, 
		moblevel, mobexpval,

		charhp, charmana, charmedmg, charradmg, charmadmg,
		chardodgechance, charphysresis, charmagresis, charcritchance, maxhp, maxmana;

	string mobname, charname;

public:
	double combat()
	{
		double expgained = 0;
		getcharacterstats(charhp, charmana, charmedmg, charradmg, charmadmg,
			chardodgechance, charphysresis, charmagresis, charcritchance, maxhp, maxmana, charname);
		combatdisplayfighters();

		system("pause");


		updatehpmanapostcombat(charhp, charmana);
		return expgained;
	}
	void combatdisplayfighters()
	{
		cout << left << charname << right << setw(40) << right << mobname << setw(3) << moblevel << endl;
		cout << "Health: " << setw(6) << charhp << "/" << setw(6) << left <<  maxhp << setw(25) << right << "Health: " << setw(6) << mobhp << endl;//EXTEND TO ADD MONSTER INFO TO THE RIGHT ALIGN
		cout << "Mana  : " << setw(6) << charmana << "/" << setw(6) << left << maxmana << setw(25) << right << "Mana  : " << setw(6) << mobmana << endl;
		cout << "Mellee: " << setw(6) << charmedmg << setw(32) << right << "Mellee: " << setw(6) << mobmedmg << endl;
		cout << "Ranged: " << setw(6) << charradmg << setw(32) << right << "Ranged: " << setw(6) << mobradmg << endl;
		cout << "Magic : " << setw(6) << charmadmg << setw(32) << right << "Magic : " << setw(6) << mobmadmg << endl;
		cout << "Dodge : " << setw(6) << chardodgechance << "%" << setw(31) << right << "Dodge : " << setw(6) << mobdodgechance << "%" << endl;
		cout << "Crits : " << setw(6) << charcritchance << "%" << setw(31) << right << "Crits : " << setw(6) << mobcritchance << "%" << endl;
		cout << "PResis: " << setw(6) << charphysresis << "%" << setw(31) << right << "PResis: " << setw(6) << mobphysresis << "%" << endl;
		cout << "MResis: " << setw(6) << charmagresis << "%" << setw(31) << right << "MResis: " << setw(6) << mobmagresis << "%" << endl;


	}
	void Chuggachugga()
	{   int discflag = 0;
	srand(static_cast<unsigned int>(time(NULL)));
		int EXIT = 0;
		int foundmonster = rand() % 100;

		do
		{
			char option = 'Z', ssoption = 'Z';
			int valid = 0;
			int valid2 = 0;
			int attoption = NULL;
			int skilloption = NULL, skillnumop = NULL;
			minimap();
			whereami();

			do
			{
				if (valid2 > 0) { valid = 0; }
				if (valid > 0) { cout << "Invalid command!" << endl; }
				valid++;
				if (valid2 != 0)
				{
					system("cls");
					minimap();
					whereami();
				}
				switch (valid2)
				{
				case 1:
					cout << "This is as far North as we dare venture!" << endl;
					break;
				case 2:
					cout << "This is as far South as we dare venture!" << endl;
					break;
				case 3:
					cout << "This is as far West as we dare venture!" << endl;
					break;
				case 4:
					cout << "This is as far East as we dare venture!" << endl;
					break;
				}

				valid2 = 0;
				cout << "Move by Entering w,a,s,d (up,left,down,right respectivly). Inventory: i :";
				cin >> option;
				cin.clear(); cin.ignore(INT_MAX, '\n');
				checkborder(option, valid2);
			} while (option != 'w' && option != 'a' && option != 's' && option != 'd' && option != 'i');
			valid = 0;
			switch (option)
			{
			case 'w':
				moveup();
				hpandmanaregen();
				
				foundmonster = rand() % 100;
				if (foundmonster > 50)
				{
					mongenerate(mobname, mobhp, mobmana, mobmedmg, mobradmg, mobmadmg,
						mobdodgechance, mobphysresis, mobmagresis, mobcritchance, moblevel, mobexpval);
					cout << "\nYou have found a " << mobname << " Watch out!" << endl;;
					combat();

				}

				break;
			case 'a':
				moveleft();
				hpandmanaregen();

				foundmonster = rand() % 100;
				if (foundmonster > 50)
				{
					mongenerate(mobname, mobhp, mobmana, mobmedmg, mobradmg, mobmadmg,
						mobdodgechance, mobphysresis, mobmagresis, mobcritchance, moblevel, mobexpval);
					cout << "\nYou have found a " << mobname << " Watch out!" << endl;;
					combat();

				}

				break;
			case 's':
				movedown();
				hpandmanaregen();

				foundmonster = rand() % 100;
				if (foundmonster > 50)
				{
					mongenerate(mobname, mobhp, mobmana, mobmedmg, mobradmg, mobmadmg,
						mobdodgechance, mobphysresis, mobmagresis, mobcritchance, moblevel, mobexpval);
					cout << "\nYou have found a " << mobname << " Watch out!" << endl;;
					combat();

				}

				break;
			case 'd':
				moveright();
				hpandmanaregen();
				
				foundmonster = rand() % 100;
				if (foundmonster > 50)
				{
					mongenerate(mobname, mobhp, mobmana, mobmedmg, mobradmg, mobmadmg,
						mobdodgechance, mobphysresis, mobmagresis, mobcritchance, moblevel, mobexpval);
					cout << "\nYou have found a " << mobname << " Watch out!" << endl;;
					combat();

				}

				break;
			case 'i':   ///////////inventory////////////////////////////////////
				char invoption = 'Z';
				do
				{
				invoption = 'Z';
				system("cls");

				charstats();

				cout << "Enter a for the attribute page, m to go to the map, s for the skill page, e to return to game ";
				cin >> invoption;
				cin.clear(); cin.ignore(INT_MAX, '\n');
				switch (invoption)
				{
				case 'a'://///////////attribute page<<<<<<*******************************************************************
					//*******************************************************************************************************
					option = 'z';
					system("cls");
					do
					{
					option = 'Z';
					attributepage();
					cout << "To learn more about the attributes enter l. To return to stat page enter i\n";
					if (getattpnts() == 0) cout << ":";
					if (getattpnts() != 0) { cout << "To distribute attribute points enter d\n:"; }
					cin >> option;
					cin.clear(); cin.ignore(INT_MAX, '\n');
					if (getattpnts() != 0)
					{
						switch (option)
						{
						case 'l':
							learnattributes();
							break;
						case 'd':
							do
							{
								attoption = 0;
								system("cls");
								addattributes();
								cin >> attoption;
								cin.clear(); cin.ignore(INT_MAX, '\n');
								switch (attoption)
								{
								case 1:
									addstrength();
									break;
								case 2:
									addperception();
									break;
								case 3:
									addintelligence();
									break;
								case 4:
									addAgility();
									break;
								case 5:
									addendurance();
									break;
								case 6:
									addWisdom();
									break;
								}
							} while (attoption != 7 && getattpnts() > 0);
							break;
						case 'i':
							invoption = 'Z';
							break;
						}
					}
					else
					{
						switch (option)
						{
						case 'l':
							learnattributes();
							break;
						case 'i':
							invoption = 'Z';
							break;
						}
					}
					system("cls");
					} while (option != 'l' && option != 'd' && option != 'i');
					option = 'Z';
					break;//****************************end attribute page
				case 'm':
					system("cls");
					printmap();
					break;
				case 's'://///////////////////////////////////////////////////////////skilllsssss
					option = 'Z';
					do
					{
						charskills();
						cout << "To return to the inventory page enter i, to select skills enter s." << endl;
						if (getskillpnts() != 0) 
						{
							cout << "To learn new skills enter d\n:";
							cin >> option;
							cin.clear(); cin.ignore(INT_MAX, '\n');
							if (option == 'd')
							{
								do
								{
									system("cls");
									if (getskillflag() == 1) { cout << "That skill is already maxed out!" << endl; }
									skillflagreset();
									addskills();
									cin >> skilloption;
									cin.clear(); cin.ignore(INT_MAX, '\n');
									switch (skilloption)
									{
									case 1:
										learnmelleeattack();
										break;
									case 2:
										learnrangedattack();
										break;
									case 3:
										learnfireball();
										break;
									case 4:
										learnheavyStrike();
										break;
									case 5:
										learnchargedshot();
										break;
									case 6:
										learnfrostbolt();
										break;

									}
								} while (getskillpnts() > 0 && skilloption != 0);
							}
						}
						else
						{
							cin >> option;
							cin.clear(); cin.ignore(INT_MAX, '\n');
						}
						if (option == 's')
						{
							do
							{
								ssoption = 'z';
								system("cls");
								skillselect();
								cin >> ssoption;
								cin.clear(); cin.ignore(INT_MAX, '\n');
								cout << "And now the skill number: ";
								cin >> skillnumop;
								cin.clear(); cin.ignore(INT_MAX, '\n');
								skillswitchfunc(skillnumop);
								switch (ssoption)
								{
								case 1:
									skillslot1();
									break;
								case 2:
									skillslot2();
									break;
								case 3:
									skillslot3();
									break;
								case 4:
									skillslot4();
									break;
								case 5:
									break;
								}
							}while(ssoption != 5);

						}
					} while (option != 'd' && option != 'i' && option != 's');
					break;/////////////////////////////////////////////////////////////////////////end skillllllllllllllllllssssssssssssssss
				case 'e':
					break;
				}
				} while (invoption != 'a' && invoption != 'm' && invoption != 's' && invoption != 'e');
				invoption = 'Z';
				break;  ///////////endinventory
			}
		
			system("cls");

			discovery(discflag);

			if (discflag == 1) { double gain = 25; gainexp(gain); discflag = 0; }
		} while (EXIT != 2);
	}

	void newgame()
	{
		int attoption = 0;
		mapgen(); //generates the map
		charcreate(); //creates character
		initiatemons();//initiates monsters
		initskills(); //initiates skills
		do
		{
			attoption = 0;
			system("cls");
			addattributes();
			cin >> attoption;
			cin.clear(); cin.ignore(INT_MAX, '\n');
			switch (attoption)
			{
			case 1:
				addstrength();
				break;
			case 2:
				addperception();
				break;
			case 3:
				addintelligence();
				break;
			case 4:
				addAgility();
				break;
			case 5:
				addendurance();
				break;
			case 6:
				addWisdom();
				break;
			}
		} while (attoption != 7 && getattpnts() > 0);
		starthpandmana();
		system("cls");
		Chuggachugga(); //The primary hub for game mechanics.
	}
};

void main(void)
{
	engine choochoo;
	choochoo.newgame();

	cin.get();
	cin.ignore();
}
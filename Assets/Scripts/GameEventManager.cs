using UnityEngine;
using System.Collections;

public static class GameEventManager
{
		//Global Variables
		static public string gameVersion = "0.5_alfa";
	
		static public int berry, stick, stone, flint, log, weight;
		static public float hunger, health;
		static public string rightHandWeapon;
	
		public delegate void GameEvent ();
		
		public enum E_STATES
		{
				e_mainMenu,
				e_game,
				e_pause,
				e_inventoryWindow,
				e_actionButtonPressed				
		}
		;
	
		public enum E_CLICKSTATE
		{
				e_delayFinish,
				e_delayStart
	}
		;
	
		//--------------
	
		static E_STATES m_gameState = E_STATES.e_mainMenu;
	
		static E_CLICKSTATE m_click_state = E_CLICKSTATE.e_delayStart;
	
		//----------------
	
		public static void SetClickState (E_CLICKSTATE clickstate)
		{
				m_click_state = clickstate;
		}
		public static E_CLICKSTATE GetClickState ()
		{
				return m_click_state;
		}
	
		//---------------
	
		public static void SetState (E_STATES state)
		{
				m_gameState = state;
		}
		public static E_STATES GetState ()
		{
				return m_gameState;
		}
	
		//-------------------
	
	
}

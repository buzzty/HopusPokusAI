// our state interface
public interface IPotatoBossState
{
	// called by "PotatoBoss" during each update loop - effectively like UNityEngine.Update()
	// can be used to handle state transitions and update the game entity (PotatoBoss boss) in that method
	IPotatoBossState Handle(PotatoBoss boss);
	// entering the state with this - do setup for the state here
	void StateEnter(PotatoBoss boss);
	// exiting the state - do clean up for the state here
	void StateExit(PotatoBoss boss);
}
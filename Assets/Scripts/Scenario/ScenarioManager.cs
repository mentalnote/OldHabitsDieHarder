
public class ScenarioManager
{
	static private Scenario currentScenario;

	//Set the current scenario
	static public void SetCurrentScenario(Scenario scenario) {
		currentScenario = scenario;
	}

	//Retrieve the current scenario
	static public Scenario GetCurrentScenario() {
		return currentScenario;
	}
}

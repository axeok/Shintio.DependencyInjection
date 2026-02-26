namespace TestLib;

public class TestService
{
	private readonly TestConfigForFactory _configForFactory;
	private readonly TestConfigForInstance _configForInstance;

	public TestService(TestConfigForFactory configForFactory, TestConfigForInstance configForInstance)
	{
		_configForFactory = configForFactory;
		_configForInstance = configForInstance;
	}

	public string TestMethod()
	{
		return $"{_configForFactory.Message} {_configForInstance.Message}";
	}
}
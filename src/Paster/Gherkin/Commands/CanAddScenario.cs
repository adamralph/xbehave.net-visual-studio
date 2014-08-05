namespace xBehave.Paster.Gherkin
{
    internal interface CanAddScenario
    {
        TreeState AddScenario(string rawLine);
    }
}
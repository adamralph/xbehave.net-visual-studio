namespace xBehave.Paster.Gherkin
{
    internal interface CanAddInstruction
    {
        TreeState AddInstruction(string rawLine, Gherkin rawType);
    }
}
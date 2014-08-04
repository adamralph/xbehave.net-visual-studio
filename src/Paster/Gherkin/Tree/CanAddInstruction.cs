namespace xBehave.Paster.Gherkin
{
    internal interface CanAddInstruction
    {
        TreeState AddInstruction(string rawLine, Identifier rawType);
    }
}
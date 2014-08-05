namespace xBehave.Paster.Gherkin
{
    internal interface SyntaxGroupingNode : SyntaxNode
    {
        void AddNode(Instruction node);
    }
}
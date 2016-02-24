namespace CsLuaConverter.CodeElementAnalysis.Statements
{
    public class StatementInfo
    {
        public StatementType StatementType;
        public int[] EscentialElementsIndexes;

        public StatementInfo(StatementType statementType, int[] escentialElementsIndexes)
        {
            this.StatementType = statementType;
            this.EscentialElementsIndexes = escentialElementsIndexes;
        }

        public StatementInfo(StatementType statementType, int index)
        {
            this.StatementType = statementType;
            this.EscentialElementsIndexes = new []{index};
        }
    }
}
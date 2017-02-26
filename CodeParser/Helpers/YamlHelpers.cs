namespace CodeParser.Helpers
{
    public static class YamlHelpers
    {
        public static string GenerateIndent(int indentation)
        {
            string indent = "";
            for (int i = 0; i < indentation - 1; ++i)
                indent += " ";
            return indent;
        }

        public static string PropertyIndentSpace = "   ";
        public static int ListIndentSize = 3;
    }
}

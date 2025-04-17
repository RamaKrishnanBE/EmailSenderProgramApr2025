namespace EmailSenderProgram.Utility.NewFolder
{
    public static class EmailContentReplacer
    {
        public static string ReplacePlaceHolders(string inputText, Dictionary<string, string> placeholdersAndValues)
        {
            string result = inputText;
            if (!string.IsNullOrEmpty(inputText) && placeholdersAndValues != null && placeholdersAndValues.Any())
            {
                foreach (var placeholder in placeholdersAndValues)
                {
                    result = result.Replace(placeholder.Key, placeholder.Value);
                }

            }
            return result;
        }
    }
}

namespace ComputerShopDB.Helpers
{
    public static class Check
    {
        public static void ArgumentNotNull<T>(T argumentValue, string argumentName = "") where T : class
        {
            if (argumentValue != null)
            {
                return;
            }

            if (string.IsNullOrEmpty(argumentName))
            {
                throw Error.ArgumentNull();
            }

            throw Error.ArgumentNull(argumentName);
        }

        public static void ArgumentSatisfies<T>(T argumentValue, Func<T, bool> condition,
           string violationMessage = "", string argumentName = "")
        {
            if (condition == null)
            {
                return;
            }

            if (condition(argumentValue))
            {
                return;
            }

            if (string.IsNullOrEmpty(violationMessage))
            {
                throw Error.Argument();
            }

            if (string.IsNullOrEmpty(argumentName))
            {
                throw Error.Argument(violationMessage);
            }

            throw Error.Argument(argumentName, violationMessage);
        }
    }
}

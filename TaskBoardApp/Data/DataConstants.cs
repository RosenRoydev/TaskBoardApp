namespace TaskBoardApp.Data
{
    public static class DataConstants
    {
        
        
            public const int TitleMaxLength = 70;
            public const int TitleMinLength = 5;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 1000;

            public const int BoardNameMinLength = 3;
            public const int BoardNameMaxLength = 30;

        public const string MinimumStringLength = "The field {0} must be between {2} and {1} symbols";
        public const string RequiredField = "The field {0} is required";
    }
}

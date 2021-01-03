namespace SecondAssignmentApi.Extenions
{
    public class UserParams
    {
        private const int MaxItems = 50;
        public int PageNumber { get; set; } = 1;
        public int MinPrice { get; set; } = 1;
        public int MaxPrice { get; set; } = 1000000;
        public string ProvidedText { get; set; } = "";
        public int NbOfRooms { get; set; }

        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxItems) ? MaxItems : value; }
        }
    }
}

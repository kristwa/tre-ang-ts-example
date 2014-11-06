namespace ScoreTracker.API.Model
{
    public class TableItem
    {
        public int Place { get; set; }
        public Team Team { get; set; }

        public int Games { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int Won { get; set; }
        public int Drawn { get; set; }
        public int Lost { get; set; }
        public int Points { get; set; }
    }
}

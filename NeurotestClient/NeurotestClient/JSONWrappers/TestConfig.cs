namespace NeurotestServer.JSONWrappers
{
    /*
     * Wrapper class to transmitt test config via POST request
     */
    public class TestConfig
    {
        public string QuestionDuration { get; set; }
        public string AngerCount { get; set; }
        public string AstonishmentCount { get; set; }
        public string DisgustCount { get; set; }
        public string FearCount { get; set; }
        public string HappinessCount { get; set; }
        public string SadnessCount { get; set; }
    }
}

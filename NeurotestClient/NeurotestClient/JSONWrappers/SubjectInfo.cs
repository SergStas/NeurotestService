namespace NeurotestServer.JSONWrappers
{
    /*
     * Wrapper class to transmitt subject info via POST request
     */
    public class SubjectInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Sex { get; set; }
        public string BirthDate { get; set; }
        public string Address { get; set; }
        public string Job { get; set; }
        public string Diseases { get; set; }
        public string Phone { get; set; }
    }
}

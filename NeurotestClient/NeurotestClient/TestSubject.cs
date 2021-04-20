using System.Text;

namespace NeurotestServer
{
    public class TestSubject
    {
        public TestSubject(ulong id, SubjectInfo info)
        {
            ID = id;
            Info = info;
        }
        public string ToCSVString()
        {
            StringBuilder builder = new StringBuilder(c_CSVHeader);

            string fullName = 
                Info.LastName + " " +
                Info.FirstName + " " +
                ((Info.Patronymic == string.Empty) ? "" : " " + Info.Patronymic);
            builder.Append(fullName);
            builder.Append(";");
            builder.Append(Info.Sex.ToString("G"));
            builder.Append(";");
            builder.Append(Info.BirthDate);
            builder.Append(";");
            builder.Append(Info.Address);
            builder.Append(";");
            builder.Append(Info.Job);
            builder.Append(";");
            builder.Append(string.Join(", ", Info.Diseases));
            builder.Append(";");
            builder.Append(Info.Phone);

            return builder.ToString();
        }
        public ulong ID { get; }
        public SubjectInfo Info { get; }

        private const string c_CSVHeader = "Name;Sex;BirthDate;Address;Job;Diseases;Phone\n";
    }
}

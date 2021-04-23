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
            StringBuilder builder = new StringBuilder();

            string fullName =
                Info.LastName + " " +
                Info.FirstName + " " +
                ((Info.Patronymic == string.Empty) ? "" : " " + Info.Patronymic);

            builder.Append(ID);
            builder.Append(";");
            builder.Append(fullName);
            builder.Append(";");
            builder.Append(Info.Sex.ToString("G"));
            builder.Append(";");
            builder.Append(Info.BirthDate.ToString("dd.MM.yyyy"));
            builder.Append(";");
            builder.Append(Info.Address);
            builder.Append(";");
            builder.Append(Info.Job);
            builder.Append(";");
            builder.Append(string.Join(", ", Info.Diseases));
            builder.Append(";");
            builder.Append(Info.Phone);
            builder.Append("\n");

            return builder.ToString();
        }
        public ulong ID { get; }
        public SubjectInfo Info { get; }
    }
}

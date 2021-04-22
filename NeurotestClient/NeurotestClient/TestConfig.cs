using System;
using System.Diagnostics;

namespace NeurotestServer
{
    public class TestConfig
    {
        public static TestConfig FromJSON(JSONWrappers.TestConfig jsonConfig)
        {
            short questionDuration = Convert.ToInt16(jsonConfig.QuestionDuration);
            short angerCount = Convert.ToInt16(jsonConfig.AngerCount);
            short astonishmentCount = Convert.ToInt16(jsonConfig.AstonishmentCount);
            short disgustCount = Convert.ToInt16(jsonConfig.DisgustCount);
            short fearCount = Convert.ToInt16(jsonConfig.FearCount);
            short happinessCount = Convert.ToInt16(jsonConfig.HappinessCount);
            short sadnessCount = Convert.ToInt16(jsonConfig.SadnessCount);

            return new TestConfig(questionDuration, angerCount, astonishmentCount, disgustCount, fearCount,
                happinessCount, sadnessCount);
        }
        public short QuestionDuration { get; }  // The limit of one question in seconds
        public short AngerCount { get; }  // The number of pictures with anger in the sample
        public short AstonishmentCount { get; }  // The number of pictures with astonishment in the sample
        public short DisgustCount { get; }  // The number of pictures with disgust in the sample
        public short FearCount { get; }  // The number of pictures with fear in the sample
        public short HappinessCount { get; }  // The number of pictures with heppiness in the sample
        public short SadnessCount { get; }  // The number of pictures with sadness in the sample
        public short TotalCount { get; }  // Total quantity of pictures in the sample
        
        public const short c_SeveritiesCount = 3;  // The number of severities of each emotion type
        protected TestConfig(short questionDuration, short angerCount, short astonishmentCount,
            short disgustCount, short fearCount, short happinessCount, short sadnessCount) //FIXME
        {
            string assertionMessage = "{} must be in range {}-{}.";
            Debug.Assert((questionDuration >= 3) && (questionDuration <= 10),
                string.Format(assertionMessage, "Question duration", 3, 10));
            Debug.Assert((angerCount >= 2) && (angerCount <= 5),
                string.Format(assertionMessage, "Anger count", 2, 5));
            Debug.Assert((astonishmentCount >= 2) && (astonishmentCount <= 5),
                string.Format(assertionMessage, "Astonishment count", 2, 5));
            Debug.Assert((disgustCount >= 2) && (disgustCount <= 5),
                string.Format(assertionMessage, "Disgust count", 2, 5));
            Debug.Assert((fearCount >= 2) && (fearCount <= 5),
                string.Format(assertionMessage, "Fear count", 2, 5));
            Debug.Assert((happinessCount >= 2) && (happinessCount <= 5),
                string.Format(assertionMessage, "Happiness count", 2, 5));
            Debug.Assert((sadnessCount >= 2) && (sadnessCount <= 5),
                string.Format(assertionMessage, "Sadness count", 2, 5));

            QuestionDuration = questionDuration;
            AngerCount = angerCount;
            AstonishmentCount = astonishmentCount;
            DisgustCount = disgustCount;
            FearCount = fearCount;
            HappinessCount = happinessCount;
            SadnessCount = sadnessCount;
            TotalCount = Convert.ToInt16(
                c_SeveritiesCount * AngerCount +
                c_SeveritiesCount * AstonishmentCount +
                c_SeveritiesCount * DisgustCount +
                c_SeveritiesCount * FearCount +
                c_SeveritiesCount * HappinessCount +
                c_SeveritiesCount * SadnessCount
                );

            Debug.Assert((TotalCount >= 36) && (TotalCount <= 90),
                string.Format(assertionMessage, "Total count", 36, 90));
        }
    }
}
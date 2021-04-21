using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;

namespace NeurotestServer
{
    public class Answer
    {
        public static Answer FromJSON(JSONWrappers.Answer jsonAnswer)
        {
            Question question = Question.FromJSON(jsonAnswer.Question);
            EmotionType userInput = Utils.EmotionTypeStringToEnum(jsonAnswer.UserInput);
            float elapsedTime = Convert.ToSingle(jsonAnswer.ElapsedTime);

            return new Answer(question, userInput, elapsedTime);
        }
        public string ToCSVString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(Question.Path);
            builder.Append(";");
            builder.Append(Question.Type);
            builder.Append(";");
            builder.Append(UserInput);
            builder.Append(";");
            builder.Append(ElapsedTime);
            builder.Append("\n");

            return builder.ToString();
        }
        public EmotionType GetRight() => Question.Type;
        public EmotionSeverity GetSeverity() => Question.Severity;
        public bool IsRight() => Question.Type == UserInput;
        public bool IsSimilar() => Question.Type == s_SimilarEmotions[UserInput];
        public Question Question { get; }  // The original question
        public EmotionType UserInput { get; }  // The answer given by a user
        public float ElapsedTime { get; }  // Time taken by the user to answer in seconds. -1 means "No answer"
        protected Answer(Question question, EmotionType userInput, float elapsedTime)
        {
            Debug.Assert((elapsedTime >= -1.0f) && (elapsedTime <= 10.0f), "Time value must be in range -1-10");

            Question = question;
            UserInput = userInput;
            ElapsedTime = elapsedTime;
        }

        private static readonly Dictionary<EmotionType, EmotionType> s_SimilarEmotions = new Dictionary<EmotionType, EmotionType>()
        {
            { EmotionType.Anger, EmotionType.Disgust },
            { EmotionType.Astonishment, EmotionType.Happiness },
            { EmotionType.Disgust, EmotionType.Anger },
            { EmotionType.Fear, EmotionType.Sadness },
            { EmotionType.Happiness, EmotionType.Astonishment },
            { EmotionType.Sadness, EmotionType.Fear }
        };
    }
}

using System.Collections.Generic;
using System.Diagnostics;

namespace NeurotestServer
{
    public struct Answer
    {
        public Answer(Question question, EmotionType userInput, float elapsedTime)
        {
            Debug.Assert((elapsedTime >= -1.0f) && (elapsedTime <= 10.0f), "Time value must be in range -1-10");

            Question = question;
            UserInput = userInput;
            ElapsedTime = elapsedTime;
        }
        public EmotionType GetRight() => Question.Type;
        public EmotionSeverity GetSeverity() => Question.Severity;
        public bool IsRight() => Question.Type == UserInput;
        public bool IsSimilar() => Question.Type == s_SimilarEmotions[UserInput];
        public Question Question { get; }  // The original question
        public readonly EmotionType UserInput { get; }  // The answer given by a user
        public readonly float ElapsedTime { get; }  // Time taken by the user to answer in seconds. -1 means "No answer"

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

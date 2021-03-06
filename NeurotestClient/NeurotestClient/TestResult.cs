using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace NeurotestServer
{
    public class TestResult
    {
        /*
         * Calculates statistics by a given answers
         */
        public TestResult(ulong subjectID, List<Answer> answers)
        {
            SubjectID = subjectID;
            Answers = answers;
            AccuracyInPieces = CulcAccuraciesInPieces(answers);
            AccuracyInPercents = CulcAccuraciesInPercents(answers, AccuracyInPieces);
            UnanswerdQuestionCount = Convert.ToUInt16(answers.Where(answer => answer.ElapsedTime == -1.0f).Count());

            /* Answers count will never be 0, so we can just devide by it */
            UnansweredQuestionPercentage = UnanswerdQuestionCount / (float)answers.Count() * 100.0f;
            SimilarEmotionsCount = CulcSimilarAnswersCount(answers);
            Durations = CulcDurations(answers);
            MinReactionSpeeds = CulcMinSpeeds(answers);
            MeanReactionSpeeds = CulcMeanSpeeds(answers);
            MaxReactionSpeeds = CulcMaxSpeeds(answers);
            MeanSpeedsBySubcategory = CulcMeanSpeedsBySubcategory(answers);

            string assertionMessage = "Constructor detected wrong value for {0}. It must be in range {1}-{2}.";
            Debug.Assert(UnanswerdQuestionCount <= 90,
                string.Format(assertionMessage, "'UnansweredQuestionCount'", 0, 90));
            Debug.Assert((UnansweredQuestionPercentage >= 0.0f) && (UnansweredQuestionPercentage <= 100.0f),
                string.Format(assertionMessage, "'UnansweredQuestionPercentage'", 0, 100));
            Debug.Assert(SimilarEmotionsCount <= 90,
                string.Format(assertionMessage, "'SimilarEmotionsCount'", 0, 90));
        }
        public string ToCSVString()
        {
            const string floatFormat = "0.00";
            StringBuilder builder = new StringBuilder();

            builder.Append(SubjectID);
            builder.Append(";");

            foreach (EmotionType type in AccuracyInPieces.Keys)
            {
                builder.Append(AccuracyInPieces[type]);
                builder.Append(";");
            }

            foreach (EmotionType type in AccuracyInPercents.Keys)
            {
                builder.Append(AccuracyInPercents[type].ToString(floatFormat));
                builder.Append(";");
            }

            builder.Append(UnanswerdQuestionCount.ToString(floatFormat));
            builder.Append(";");
            builder.Append(UnansweredQuestionPercentage.ToString(floatFormat));
            builder.Append(";");
            builder.Append(SimilarEmotionsCount);
            builder.Append(";");

            foreach (EmotionType type in Durations.Keys)
            {
                builder.Append(Durations[type].ToString(floatFormat));
                builder.Append(";");
            }

            foreach (EmotionType type in MinReactionSpeeds.Keys)
            {
                builder.Append(MinReactionSpeeds[type].ToString(floatFormat));
                builder.Append(";");
            }

            foreach (EmotionType type in MeanReactionSpeeds.Keys)
            {
                builder.Append(MeanReactionSpeeds[type].ToString(floatFormat));
                builder.Append(";");
            }

            foreach (EmotionType type in MaxReactionSpeeds.Keys)
            {
                builder.Append(MaxReactionSpeeds[type].ToString(floatFormat));
                builder.Append(";");
            }

            foreach (EmotionType type in MeanSpeedsBySubcategory.Keys)
            {
                foreach (EmotionSeverity severity in MeanSpeedsBySubcategory[type].Keys)
                {
                    builder.Append(MeanSpeedsBySubcategory[type][severity].ToString(floatFormat));
                    builder.Append(";");
                }
            }

            builder.Append("\n");

            return builder.ToString();
        }
        public ulong SubjectID { get; }  // ID of the test subject who gave this result
        public List<Answer> Answers { get; }  // List of subject's answers
        public Dictionary<EmotionType, ushort> AccuracyInPieces { get; }  // Accuracies for each emotion type in pieces
        public Dictionary<EmotionType, float> AccuracyInPercents { get; }  // Accuracies for each emotion type in precents
        public ushort UnanswerdQuestionCount { get; }  // The number of questions to which the subject did not have time to answer
        public float UnansweredQuestionPercentage { get; }  // The percentage of questions to which the subject did not have time to answer
        public ushort SimilarEmotionsCount { get; }  // The number of answers which is similar to the true emotion type
        public Dictionary<EmotionType, float> Durations { get; }  // Time taken to complete the test (total and for each emotion) in milliseconds
        public Dictionary<EmotionType, float> MinReactionSpeeds { get; }  // Minimum reaction speed for each emotion in milliseconds
        public Dictionary<EmotionType, float> MeanReactionSpeeds { get; }  // Mean reaction speed for each emotion in milliseconds
        public Dictionary<EmotionType, float> MaxReactionSpeeds { get; }  // Maximum reaction speed for each emotion in milliseconds
        public Dictionary<EmotionType, Dictionary<EmotionSeverity, float>> MeanSpeedsBySubcategory { get; }  // Average speed for each emotion's subcategory in milliseconds
        private static Dictionary<EmotionType, ushort> CulcAccuraciesInPieces(List<Answer> answers)
        {
            Dictionary<EmotionType, ushort> accuracies = new Dictionary<EmotionType, ushort>()
            {
                { EmotionType.Anger, CountRightByType(answers, EmotionType.Anger) },
                { EmotionType.Astonishment, CountRightByType(answers, EmotionType.Astonishment) },
                { EmotionType.Disgust, CountRightByType(answers, EmotionType.Disgust) },
                { EmotionType.Fear, CountRightByType(answers, EmotionType.Fear) },
                { EmotionType.Happiness, CountRightByType(answers, EmotionType.Happiness) },
                { EmotionType.Sadness, CountRightByType(answers, EmotionType.Sadness) }
            };

            return accuracies;
        }
        private static Dictionary<EmotionType, float> CulcAccuraciesInPercents(List<Answer> answers,
            Dictionary<EmotionType, ushort> accuraciesInPieces)
        {
            /* 
             * All 'CountByType' values will never be 0.
             * There is no need in additional verification to devide by them.
             */
            float angerAccuracy = accuraciesInPieces[EmotionType.Anger] /
                (float)CountByType(answers, EmotionType.Anger) * 100.0f;
            float astonishmentAccuracy = accuraciesInPieces[EmotionType.Astonishment] /
                (float)CountByType(answers, EmotionType.Astonishment) * 100.0f;
            float disgustAccuracy = accuraciesInPieces[EmotionType.Disgust] /
                (float)CountByType(answers, EmotionType.Disgust) * 100.0f;
            float fearAccuracy = accuraciesInPieces[EmotionType.Fear] /
                (float)CountByType(answers, EmotionType.Fear) * 100.0f;
            float happinessAccuracy = accuraciesInPieces[EmotionType.Happiness] /
                (float)CountByType(answers, EmotionType.Happiness) * 100.0f;
            float sadnessAccuracy = accuraciesInPieces[EmotionType.Sadness] /
                (float)CountByType(answers, EmotionType.Sadness) * 100.0f;

            Dictionary<EmotionType, float> accuracies = new Dictionary<EmotionType, float>()
            {
                { EmotionType.Anger, angerAccuracy },
                { EmotionType.Astonishment, astonishmentAccuracy },
                { EmotionType.Disgust, disgustAccuracy },
                { EmotionType.Fear, fearAccuracy },
                { EmotionType.Happiness, happinessAccuracy },
                { EmotionType.Sadness, sadnessAccuracy }
            };

            return accuracies;
        }
        private static Dictionary<EmotionType, float> CulcDurations(List<Answer> answers)
        {
            Dictionary<EmotionType, float> durations = new Dictionary<EmotionType, float>()
            {
                { EmotionType.Anger, CulcDurationForType(answers, EmotionType.Anger) },
                { EmotionType.Astonishment, CulcDurationForType(answers, EmotionType.Astonishment) },
                { EmotionType.Disgust, CulcDurationForType(answers, EmotionType.Disgust) },
                { EmotionType.Fear, CulcDurationForType(answers, EmotionType.Fear) },
                { EmotionType.Happiness, CulcDurationForType(answers, EmotionType.Happiness) },
                { EmotionType.Sadness, CulcDurationForType(answers, EmotionType.Sadness) }
            };

            return durations;
        }
        private static Dictionary<EmotionType, float> CulcMinSpeeds(List<Answer> answers)
        {
            float angerMinSpeed = FilterByEmotionType(answers, EmotionType.Anger).Where(answer => answer.ElapsedTime != -1.0f).Min(answer => answer.ElapsedTime);
            float astonishmentMinSpeed = FilterByEmotionType(answers, EmotionType.Astonishment).Where(answer => answer.ElapsedTime != -1.0f).Min(answer => answer.ElapsedTime);
            float disgustMinSpeed = FilterByEmotionType(answers, EmotionType.Disgust).Where(answer => answer.ElapsedTime != -1.0f).Min(answer => answer.ElapsedTime);
            float fearMinSpeed = FilterByEmotionType(answers, EmotionType.Fear).Where(answer => answer.ElapsedTime != -1.0f).Min(answer => answer.ElapsedTime);
            float happinessMinSpeed = FilterByEmotionType(answers, EmotionType.Happiness).Where(answer => answer.ElapsedTime != -1.0f).Min(answer => answer.ElapsedTime);
            float sadnessMinSpeed = FilterByEmotionType(answers, EmotionType.Sadness).Where(answer => answer.ElapsedTime != -1.0f).Min(answer => answer.ElapsedTime);

            Dictionary<EmotionType, float> minSpeeds = new Dictionary<EmotionType, float>()
            {
                { EmotionType.Anger, angerMinSpeed },
                { EmotionType.Astonishment, astonishmentMinSpeed },
                { EmotionType.Disgust, disgustMinSpeed },
                { EmotionType.Fear, fearMinSpeed },
                { EmotionType.Happiness, happinessMinSpeed },
                { EmotionType.Sadness, sadnessMinSpeed }
            };

            return minSpeeds;
        }
        private static Dictionary<EmotionType, float> CulcMeanSpeeds(List<Answer> answers)
        {
            var filteredAnger = FilterByEmotionType(answers, EmotionType.Anger).Where(answer => answer.ElapsedTime != -1.0f);
            var filteredAstonishment = FilterByEmotionType(answers, EmotionType.Astonishment).Where(answer => answer.ElapsedTime != -1.0f);
            var filteredDisgust = FilterByEmotionType(answers, EmotionType.Disgust).Where(answer => answer.ElapsedTime != -1.0f);
            var filteredFear = FilterByEmotionType(answers, EmotionType.Fear).Where(answer => answer.ElapsedTime != -1.0f);
            var filteredHappiness = FilterByEmotionType(answers, EmotionType.Happiness).Where(answer => answer.ElapsedTime != -1.0f);
            var filteredSadness = FilterByEmotionType(answers, EmotionType.Sadness).Where(answer => answer.ElapsedTime != -1.0f);

            float angerMeanSpeed = filteredAnger.Sum(answer => answer.ElapsedTime) /
                filteredAnger.Count();
            float astonishmentMeanSpeed = filteredAstonishment.Sum(answer => answer.ElapsedTime) /
                filteredAstonishment.Count();
            float disgustMeanSpeed = filteredDisgust.Sum(answer => answer.ElapsedTime) /
                filteredDisgust.Count();
            float fearMeanSpeed = filteredFear.Sum(answer => answer.ElapsedTime) /
                filteredFear.Count();
            float happinessMeanSpeed = filteredHappiness.Sum(answer => answer.ElapsedTime) /
                filteredHappiness.Count();
            float sadnessMeanSpeed = filteredSadness.Sum(answer => answer.ElapsedTime) /
                filteredSadness.Count();

            Dictionary<EmotionType, float> meanSpeeds = new Dictionary<EmotionType, float>()
            {
                { EmotionType.Anger, angerMeanSpeed },
                { EmotionType.Astonishment, astonishmentMeanSpeed },
                { EmotionType.Disgust, disgustMeanSpeed },
                { EmotionType.Fear, fearMeanSpeed },
                { EmotionType.Happiness, happinessMeanSpeed },
                { EmotionType.Sadness, sadnessMeanSpeed }
            };

            return meanSpeeds;
        }
        private static Dictionary<EmotionType, float> CulcMaxSpeeds(List<Answer> answers)
        {
            float angerMaxSpeed = FilterByEmotionType(answers, EmotionType.Anger).Where(answer => answer.ElapsedTime != -1.0f).Max(answer => answer.ElapsedTime);
            float astonishmentMaxSpeed = FilterByEmotionType(answers, EmotionType.Astonishment).Where(answer => answer.ElapsedTime != -1.0f).Max(answer => answer.ElapsedTime);
            float disgustMaxSpeed = FilterByEmotionType(answers, EmotionType.Disgust).Where(answer => answer.ElapsedTime != -1.0f).Max(answer => answer.ElapsedTime);
            float fearMaxSpeed = FilterByEmotionType(answers, EmotionType.Fear).Where(answer => answer.ElapsedTime != -1.0f).Max(answer => answer.ElapsedTime);
            float happinessMaxSpeed = FilterByEmotionType(answers, EmotionType.Happiness).Where(answer => answer.ElapsedTime != -1.0f).Max(answer => answer.ElapsedTime);
            float sadnessMaxSpeed = FilterByEmotionType(answers, EmotionType.Sadness).Where(answer => answer.ElapsedTime != -1.0f).Max(answer => answer.ElapsedTime);

            Dictionary<EmotionType, float> maxSpeeds = new Dictionary<EmotionType, float>()
            {
                { EmotionType.Anger, angerMaxSpeed },
                { EmotionType.Astonishment, astonishmentMaxSpeed },
                { EmotionType.Disgust, disgustMaxSpeed },
                { EmotionType.Fear, fearMaxSpeed },
                { EmotionType.Happiness, happinessMaxSpeed },
                { EmotionType.Sadness, sadnessMaxSpeed }
            };

            return maxSpeeds;
        }
        public static Dictionary<EmotionType, Dictionary<EmotionSeverity, float>> CulcMeanSpeedsBySubcategory(List<Answer> answers)
        {
            Dictionary<EmotionType, Dictionary<EmotionSeverity, float>> speeds = new Dictionary<EmotionType, Dictionary<EmotionSeverity, float>>()
            {
                { EmotionType.Anger, CulcMeanSpeedsForType(answers, EmotionType.Anger) },
                { EmotionType.Astonishment, CulcMeanSpeedsForType(answers, EmotionType.Astonishment) },
                { EmotionType.Disgust, CulcMeanSpeedsForType(answers, EmotionType.Disgust) },
                { EmotionType.Fear, CulcMeanSpeedsForType(answers, EmotionType.Fear) },
                { EmotionType.Happiness, CulcMeanSpeedsForType(answers, EmotionType.Happiness) },
                { EmotionType.Sadness, CulcMeanSpeedsForType(answers, EmotionType.Sadness) }
            };

            return speeds;
        }
        private static Dictionary<EmotionSeverity, float> CulcMeanSpeedsForType(List<Answer> answers, EmotionType type)
        {
            var filtered = FilterByEmotionType(answers, type).Where(answer => answer.ElapsedTime != -1.0f);
            var weak = filtered.Where(answer => answer.GetSeverity() == EmotionSeverity.Weak);
            var average = filtered.Where(answer => answer.GetSeverity() == EmotionSeverity.Average);
            var strong = filtered.Where(answer => answer.GetSeverity() == EmotionSeverity.Strong);

            float weakMeanSpeed = weak.Sum(answer => answer.ElapsedTime) / weak.Count();
            float averageMeanSpeed = average.Sum(answer => answer.ElapsedTime) / average.Count();
            float strongMeanSpeed = strong.Sum(answer => answer.ElapsedTime) / strong.Count();

            Dictionary<EmotionSeverity, float> meanSpeeds = new Dictionary<EmotionSeverity, float>()
            {
                { EmotionSeverity.Weak, weakMeanSpeed },
                { EmotionSeverity.Average, averageMeanSpeed },
                { EmotionSeverity.Strong, strongMeanSpeed }
            };

            return meanSpeeds;
        }
        private static List<Answer> FilterByEmotionType(List<Answer> answers, EmotionType type)
        {
            return answers.Where(answer => answer.GetRight() == type).ToList();
        }
        /*
         * This method counts questions with a given emotion type
         */
        private static ushort CountByType(List<Answer> answers, EmotionType type)
        {
            List<Answer> filtered = FilterByEmotionType(answers, type);
            Debug.Assert(filtered.Count() > 0, $"Expected quantity of type '{type.ToString("G")}' to be greater then 0.");
            return Convert.ToUInt16(filtered.Count());
        }
        /*
         * This method counts right answers for a given emotion type
         */
        private static ushort CountRightByType(List<Answer> answers, EmotionType type)
        {
            return Convert.ToUInt16(answers.Where(answer => (answer.UserInput == type) && (answer.IsRight())).Count());
        }
        private static ushort CulcSimilarAnswersCount(List<Answer> answers)
        {
            var similars = answers.Where(answer => answer.IsSimilar());
            return Convert.ToUInt16(similars.Count());
        }
        private static float CulcDurationForType(List<Answer> answers, EmotionType type)
        {
            var filtered = answers.Where(answer => (answer.GetRight() == type) && (answer.ElapsedTime != -1.0f));
            float duration = filtered.Sum(answer => answer.ElapsedTime);
            return duration;
        }
    }
}

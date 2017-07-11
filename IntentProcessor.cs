using System;
using System.Linq;

using Newtonsoft.Json;

namespace ChattyCycleCount
{
    public class IntentProcessor
    {
        private const string INTENT_NONE = "None";
        private const string INTENT_CYCLECOUNT = "CycleCount";
        private const string INTENT_FINDPATIENT = "FindPatient";
        private const string INTENT_MAKECOFFEE = "MakeCoffee";
        private const string INTENT_TEMPERATURE = "Temperature";
        private const string INTENT_HEYOMNIC = "HeyNick";
        private const string INTENT_STOP = "Stop";
        private CycleCountIntentHandler cycleCountIntentHandler = new CycleCountIntentHandler();
        private string runningIntent;

        public int ProcessIntent(
            string intentstring,
            Action<string> writeUserMessage,
            Action<string> writeSystemMessage,
            Action<string> intetnMessage)
        {
            var result = JsonConvert.DeserializeObject<LuisResult>(intentstring);

            writeUserMessage($"{result.Query}");
            var topIntent = result.Intents[0];
            if (topIntent.Score > 0.6 && (string.IsNullOrWhiteSpace(this.runningIntent) || this.runningIntent=="None"))
            {
                this.runningIntent = topIntent.Name;
            }

            intetnMessage($"Rnning Intent: {this.runningIntent}");
            if (this.runningIntent == "CycleCount")
            {
                var done = this.cycleCountIntentHandler.ProcessCycleCount(writeSystemMessage, result);
                if (done)
                {
                    cycleCountIntentHandler = new CycleCountIntentHandler();
                    this.runningIntent = string.Empty;
                }
            }
            else if (this.runningIntent == INTENT_FINDPATIENT)
            {
                var name = string.Empty;
                if (result.Entities.Any())
                {
                    name = result.Entities[0]?.Name;
                }
                if (!string.IsNullOrWhiteSpace(name))
                {
                    writeSystemMessage($"I found patient {name} with Medical Record Number {52346}");
                }
                else
                {
                    writeSystemMessage($"Which Patient record would you like me to pull up?");
                }
                //writeSystemMessage(
                //    "Sorry, I have not been trained to find a patient. Please ask Cleen Cheet and Champaa to train me on how to find a patient.");
                this.runningIntent = string.Empty;
            }
            else if (this.runningIntent == INTENT_MAKECOFFEE)
            {
                writeSystemMessage(
                    "Sorry, I have been trained to assist you with medication inventory only. I don't know how to make coffee. Please go to the break room and help yourself.");
                this.runningIntent = string.Empty;
            }
            else if (this.runningIntent == INTENT_TEMPERATURE)
            {
                writeSystemMessage(
                    "Currenlty it is 71 degrees fahrenheit.");
                this.runningIntent = string.Empty;
            }
            else if (this.runningIntent == INTENT_HEYOMNIC)
            {
                writeSystemMessage(
                    "Hello Rajesh, how can I assist you?");
                this.runningIntent = string.Empty;
                return 1;
            }
            else if (this.runningIntent == INTENT_STOP)
            {
                writeSystemMessage(
                    "Good bye, Rajesh");
                this.runningIntent = string.Empty;
                return 2;
            }
            else
            {
                writeSystemMessage("Sorry, I did not understand what you said. Please say it again...");
                this.runningIntent = string.Empty;
            }

            return 0;
        }
    }

    public class LuisResult
        {
            //public Dialog DialogResponse { get; set; }
            // public IDictionary<string, IList<Entity>> Entities { get; set; }
            public Intent[] Intents { get; set; }
            public string Query { get; set; }
            public Entity[] Entities { get; set; }
        }

        public class Intent
        {
            [JsonProperty(PropertyName = "Intent")]
            public string Name { get; set; }

            public double Score { get; set; }
        }

        public class Entity
        {
            [JsonProperty(PropertyName = "entity")]
            public string Name { get; set; }

            public double Score { get; set; }
            public string Type { get; set; }

            public int StartIndex { get; set; }
            public int EndIndex { get; set; }
        }
    }

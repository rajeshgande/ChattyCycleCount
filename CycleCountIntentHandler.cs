using System;
using System.Linq;

namespace ChattyCycleCount
{
    public class CycleCountIntentHandler
    {
        private string med { get; set; }
        private string units { get; set; }
        private string strength { get; set; }
        private int quantity = -1;
        private int counter = 0;
        private int medcounter = 0;
        private int medEndIndex = 0;

        public CycleCountIntentHandler()
        {
            counter = 0;
        }
        public bool ProcessCycleCount(Action<string> writeSystemMessage, LuisResult intentLuisResult)
        {
            if (intentLuisResult.Entities != null && intentLuisResult.Entities.Any())
            {
                foreach (var entity in intentLuisResult.Entities)
                {
                    // writeSystemMessage($"{entity.Type}:{entity.Name}");
                    switch (entity.Type)
                    {
                        case "med":
                            med = entity.Name;
                            break;
                        case "medication":
                            var components = entity.Name.Split(' ');
                            med = components[0];
                            strength = components[1];
                            units = components[2];
                            medEndIndex = entity.EndIndex;
                            break;
                        case "builtin.number":
                            int qty;
                            if (int.TryParse(entity.Name, out qty))
                            {
                                if(entity.StartIndex >= medEndIndex)
                                {
                                    quantity = qty;
                                }
                            }
                            break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(med) && quantity > -1)
                {
                    var client = new CpClient();
                    var result1 = client.CycleCount(med, quantity, strength, units);
                    if (result1 == 1)
                    {
                        writeSystemMessage($"Quantity on hand for {med} {strength}{units} has been updated to {quantity}");
                       // writeSystemMessage("Cycle Count Successfull.");
                    }
                    else if (result1 == 0)
                    {
                        writeSystemMessage($"Cycle Count '{med}' failed. Item not found");
                    }
                    else
                    {
                        writeSystemMessage($"Cycle Count '{med}' failed. Error Occured.");
                    }
                    return true;
                }

                else if(string.IsNullOrWhiteSpace(med))
                {
                    this.medcounter++;
                    var msg =
                        this.medcounter == 0
                            ? $"Which item would you like to cycle count?"
                            : $"Sorry, I did not understand. Please say the med name again or scan a barcode.";
                    writeSystemMessage(msg);
                }
                else if(quantity < 0)
                {
                    writeSystemMessage($"What is the quantity on hand?");
                }
            }
            else
            {
                writeSystemMessage($"I see you want to perform a cycle count. Please say the item name and its quantity on hand.");
            }
            counter++;
            if (counter > 3)
                return true;
            return false;
        }
    }
}
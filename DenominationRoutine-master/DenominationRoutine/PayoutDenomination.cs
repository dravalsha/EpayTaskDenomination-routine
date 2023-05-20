namespace DenominationRoutine
{
    class PayoutDenomination
    {
        int ten_cartridge = 10, fifty_cartridge = 50, hundred_cartridge = 100;
        string paySign = " EUR ";
        string multiSign = " x ";
        string plusSign = " + ";

        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public List<string> PayoutDenominations(int number)
        {
            var items = new List<string>();
            int modTen = CalculateAmountMod(number, ten_cartridge);
            int modFifty = CalculateAmountMod(number, fifty_cartridge);
            int modHundred = CalculateAmountMod(number, hundred_cartridge);

            if (modTen == 0) items.Add(OnlyTenCombination(number));
            if (modFifty == 0) items.Add(OnlyFiftyCombination(number));
            if (modHundred == 0) items.Add(OnlyHundredCombination(number));

            if (modTen == 0 && modFifty > 0 && number > fifty_cartridge)
            {
                int fiftyMult = number / fifty_cartridge;
                items.Add(fiftyMult.ToString() + multiSign + fifty_cartridge + paySign + plusSign + OnlyTenCombination(modFifty));
            }

            if (modTen == 0 && modFifty == 0)
            {
                int fiftyMult = number / fifty_cartridge;
                if (fiftyMult > 1)
                {
                    items.Add((fiftyMult - 1).ToString() + multiSign + fifty_cartridge + paySign + plusSign + OnlyTenCombination(fifty_cartridge));
                }
            }

            if (number > hundred_cartridge && modTen == 0 && modFifty > 0 && modHundred > 0)
            {
                int multHundred = number / hundred_cartridge;
                if (multHundred >= 1)
                {
                    string combs = multHundred.ToString() + multiSign + hundred_cartridge + paySign;
                    if (modHundred > 50)
                    {
                        string combination = combs + plusSign
                            + (modHundred / fifty_cartridge).ToString() + multiSign + fifty_cartridge + paySign + plusSign
                            + (modFifty / ten_cartridge).ToString() + multiSign + ten_cartridge + paySign;

                        items.Add(combination);
                    }
                    else
                    {
                        string combination = combs + plusSign
                                             + (modHundred / ten_cartridge).ToString() + multiSign + ten_cartridge + paySign;

                        items.Add(combination);
                    }
                }
            }

            return items;
        }

        int CalculateAmountMod(int payout, int unit)
        {
            return payout % unit;
        }

        public string OnlyTenCombination(int number)
        {
            return (number / ten_cartridge).ToString() + multiSign + ten_cartridge.ToString() + paySign;
        }
        public string OnlyFiftyCombination(int number)
        {
            return (number / fifty_cartridge).ToString() + multiSign + fifty_cartridge.ToString() + paySign;
        }
        public string OnlyHundredCombination(int number)
        {
            return (number / hundred_cartridge).ToString() + multiSign + hundred_cartridge.ToString() + paySign;
        }

        static void Main(string[] args)
        {
            string? inputChars;

            do
            {
                Console.Write(" Enter amount : ");
                inputChars = Console.ReadLine();
                var combinations = new PayoutDenomination();
                int number;
                if (int.TryParse(inputChars, out number))
                {
                    foreach (var item in combinations.PayoutDenominations(number))
                    {
                        Console.WriteLine(item);
                    }
                }

                Console.WriteLine();

            } while (inputChars != "x");

        }

    }

}


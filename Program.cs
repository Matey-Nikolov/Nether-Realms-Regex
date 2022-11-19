namespace Nether_Realms_Regex
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    class Program
    {
        static void Main()
        {
            //Dictionary<string, Dictionary<int, double>> nameHealthDamage = new Dictionary<string, Dictionary<int, double>>();

            List<NameHealthDamage> nameHealthDamages = new List<NameHealthDamage>();

            List<string> codeText = Console.ReadLine()
                .Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            int health = 0;
            double damage = 0;

            foreach (string text in codeText)
            {

                string patternWordHealth = @"[A-za-z]+";
                MatchCollection codeWordMatch = Regex.Matches(text, patternWordHealth);

                string letter = "";
                foreach (var chr in codeWordMatch)
                    letter += chr.ToString();

                foreach (char chr in letter)
                    health += (int)(chr);

                string patternDamage = @"[-?\d.?]+";
                MatchCollection codeNumbers = Regex.Matches(text, patternDamage);

                foreach (var digit in codeNumbers)
                    damage += double.Parse(digit.ToString());

                string paternMultiaOrDivision = @"[\/|*]+";
                MatchCollection multiAndDivision = Regex.Matches(text, paternMultiaOrDivision);

                foreach (var item1 in multiAndDivision)
                {
                    foreach (char item2 in item1.ToString())
                    {
                        if (item2 == '*')
                        {
                            damage *= 2;
                        }
                        else if (item2 == '/')
                        {
                            damage /= 2;
                        }
                    }
                }

                nameHealthDamages.Add(new NameHealthDamage()
                {
                    Name = text,
                    Health = health,
                    Damage = damage
                });

                health = 0;
                damage = 0;
            }

            foreach (var NHD in nameHealthDamages
                .OrderBy(x => x.Name))
            {
                Console.WriteLine($"{NHD.Name} - {NHD.Health} health, {NHD.Damage:f2} damage");
            }

        }
    }
}

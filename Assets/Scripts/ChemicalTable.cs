using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class ChemicalTable
    {
        private Dictionary<BigInteger, Chemical> formulas = new Dictionary<BigInteger, Chemical>();
        private Dictionary<string, BigInteger> identifiers = new Dictionary<string, BigInteger>();

        public const int BASE_FORMULA_SHIFT = 8;

        private static ChemicalTable instance = null;
        public static ChemicalTable Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ChemicalTable();
                    instance.InitializeTable();
                }
                return instance;
            }
        }
        public ChemicalTable()
        {

        }
        public Chemical this[BigInteger i]
        {
            get { return formulas[i]; }
        }

        public Chemical this[string name]
        {
            get { return formulas[identifiers[name]]; }
        }

        public void InitializeTable()
        {
            var frogMilk = RegisterChemical("Frog Milk", 0b1, true, false);
            var crystals = RegisterChemical("Crystals", 0b10, true, false);
            var dragonBreath = RegisterChemical("Dragon Breath", 0b11, true, false);
            var witchSpice = RegisterChemical("Witch Spice", 0b100, true, false);
            var goop = RegisterChemical("Goop", 0b101, true, false);

            var royalGreenTea = RegisterChemical("Royal Green Tea",
                new Chemical[] { frogMilk, crystals });
            var cinnamonTea = RegisterChemical("Cinnamon Tea",
                new Chemical[] { frogMilk, dragonBreath });
            var fireSpice = RegisterChemical("Fire Spice",
                new Chemical[] { dragonBreath, witchSpice });

            var heatedFrogTea = RegisterChemical("Heated Frog Tea",
                new Chemical[] { fireSpice, frogMilk });
            var spicedCinnamonTea = RegisterChemical("Spiced Cinnamon Tea",
                new Chemical[] { witchSpice, cinnamonTea });
        }
        private BigInteger GetProduct(Chemical[] ingredients)
        {
            var sortedIngredients = ingredients.OrderByDescending(ing => ing.Identifier).ToArray();
            BigInteger productId = 0;
            for (int i = 0; i < sortedIngredients.Length - 1; i++)
            {
                productId = sortedIngredients[i].Identifier << sortedIngredients[i].FormulaShift;
            }
            return productId | sortedIngredients[sortedIngredients.Length - 1].Identifier;
        }

        private Chemical RegisterChemical(string name, Chemical[] formula, bool isIngredient = true, bool isProduct = true)
        {
            var shift = formula.Max(c => c.FormulaShift) * 2;
            return RegisterChemical(name, GetProduct(formula), isIngredient, isProduct, shift);
        }
        private Chemical RegisterChemical(string name, BigInteger identifier, bool isIngredient = true, bool isProduct = true, int shift = BASE_FORMULA_SHIFT)
        {
            if (!formulas.ContainsKey(identifier) && !identifiers.ContainsKey(name))
            {
                var newChem = new Chemical(name, identifier, isIngredient, false, shift);
                formulas[identifier] = newChem;
                identifiers[name] = identifier;
                return newChem;
            }
            else
            {
                throw new Exception($"chemical ({name}, {identifier}) has already been added");
            }
        }


        public Chemical Combine(Chemical[] ingredients)
        {
            if (!ingredients.All(i => i.IsIngredient)) throw new Exception($"Not a valid ingredient");
            var productId = GetProduct(ingredients);
            try
            {
                return formulas[productId];
            }
            catch
            {
                return formulas[identifiers["Goop"]];
            }
        }
    }
}

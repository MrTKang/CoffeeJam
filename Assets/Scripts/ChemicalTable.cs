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
            var crystals = RegisterChemical("Pure Crystals", 0b10, true, false);
            var magicLeaves = RegisterChemical("Magic Leaves", 0b11, true, false);

            var sweetTea = RegisterChemical("Sweet Tea",
                new Chemical[] { crystals, magicLeaves });
            var condensedMilk = RegisterChemical("Condensed Milk",
                new Chemical[] { frogMilk, crystals });
            var slime = RegisterChemical("Slime",
                new Chemical[] { frogMilk, magicLeaves });
            var sweetPearls = RegisterChemical("Sweet Pearls",
                new Chemical[] { crystals, crystals });

            var goop = RegisterChemical("Goop",
                new Chemical[] { condensedMilk, slime });
            var soulCream = RegisterChemical("Soul Cream",
                new Chemical[] { condensedMilk, sweetPearls });
            var bubbly = RegisterChemical("Bubbly",
                new Chemical[] { slime, sweetPearls });
            var arnoldPalmer= RegisterChemical("Arnold Palmer",
                new Chemical[] { sweetTea, slime });
            var bobbaGreenTea = RegisterChemical("Boba Green Tea",
                new Chemical[] { sweetPearls, sweetTea });

            var syrup = RegisterChemical("Syrup",
                new Chemical[] { goop, soulCream });
            var creamSoda = RegisterChemical("Cream Soda",
                new Chemical[] { soulCream, bubbly });
            var caffeine = RegisterChemical("Caffeine",
                new Chemical[] { bubbly, arnoldPalmer});
            var bobaGoop = RegisterChemical("Boba Goop",
                new Chemical[] { bobbaGreenTea, soulCream });

            var caffeinatedSyrup = RegisterChemical("Caffeinated Syrup",
                new Chemical[] { caffeine, syrup });
            var creamSodaSyrup = RegisterChemical("Cream Soda Syrup",
                new Chemical[] { syrup, creamSoda });
            var magicGoop = RegisterChemical("Magic Goop",
                new Chemical[] { bobaGoop, creamSoda });

            var coffeeSyrup = RegisterChemical("Coffee Syrup",
                new Chemical[] { caffeinatedSyrup, magicGoop });
            var sodaCream = RegisterChemical("Soda Cream",
                new Chemical[] { creamSodaSyrup, magicGoop });

            var coffee = RegisterChemical("Coffee",
                new Chemical[] { coffeeSyrup, sodaCream });

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

        public List<string> GetChemNames()
        {
            return identifiers.Keys.ToList();
        }
    }
}

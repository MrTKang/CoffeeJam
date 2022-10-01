using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class Chemical
    {
        public Chemical(string name, BigInteger identifier, bool isIngredient, bool isProduct, int shift)
        {
            Name = name;
            Identifier = identifier;
            IsIngredient = isIngredient;
            IsProduct = isProduct;
            FormulaShift = shift;
        }
        public bool IsIngredient { get; set; }
        public bool IsProduct { get; set; }
        public string Name { get; set; }
        public BigInteger Identifier { get; set; }
        public int FormulaShift { get; internal set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermany.AoC._2016._02
{
    public class Keypad : Dictionary<char, Node>
    {
        public static Keypad GetPart1Keypad()
        {
            var keypad = new Keypad();

            for (var i = 1; i <= 9; i++)
            {
                var c = Convert.ToChar(i.ToString("X"));
                keypad.Add(c, new Node(c));
            }

            keypad['1'].SetNodes(null, keypad['4'], null, keypad['2']);
            keypad['2'].SetNodes(null, keypad['5'], keypad['1'], keypad['3']);
            keypad['3'].SetNodes(null, keypad['6'], keypad['2'], null);
            keypad['4'].SetNodes(keypad['1'], keypad['7'], null, keypad['5']);
            keypad['5'].SetNodes(keypad['2'], keypad['8'], keypad['4'], keypad['6']);
            keypad['6'].SetNodes(keypad['3'], keypad['9'], keypad['5'], null);
            keypad['7'].SetNodes(keypad['4'], null, null, keypad['8']);
            keypad['8'].SetNodes(keypad['5'], null, keypad['7'], keypad['9']);
            keypad['9'].SetNodes(keypad['6'], null, keypad['8'], null);

            return keypad;
        }

        public static Keypad GetPart2Keypad()
        {
            var keypad = new Keypad();

            for (var i = 1; i <= 13; i++)
            {
                var c = Convert.ToChar(i.ToString("X"));
                keypad.Add(c, new Node(c));
            }

            keypad['1'].SetNodes(null, keypad['3'], null, null);
            keypad['2'].SetNodes(null, keypad['6'], null, keypad['3']);
            keypad['3'].SetNodes(keypad['1'], keypad['7'], keypad['2'], keypad['4']);
            keypad['4'].SetNodes(null, keypad['8'], keypad['3'], null);
            keypad['5'].SetNodes(null, null, null, keypad['6']);
            keypad['6'].SetNodes(keypad['2'], keypad['A'], keypad['5'], keypad['7']);
            keypad['7'].SetNodes(keypad['3'], keypad['B'], keypad['6'], keypad['8']);
            keypad['8'].SetNodes(keypad['4'], keypad['C'], keypad['7'], keypad['9']);
            keypad['9'].SetNodes(null, null, keypad['8'], null);
            keypad['A'].SetNodes(keypad['6'], null, null, keypad['B']);
            keypad['B'].SetNodes(keypad['7'], keypad['D'], keypad['A'], keypad['C']);
            keypad['C'].SetNodes(keypad['8'], null, keypad['B'], null);
            keypad['D'].SetNodes(keypad['B'], null, null, null);

            return keypad;
        }
    }
}

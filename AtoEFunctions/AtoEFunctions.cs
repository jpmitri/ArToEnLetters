using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace AtoEFunctions
{

    public ref struct ArCharacters
    {
        ReadOnlySpan<char> Ar_Init = "ابتثجحخدذرزسشصضطظعغفقكلمنهويىةؤءئ".AsSpan();

        public ArCharacters()
        {
        }
        public bool ArToEnChar(String input, out String? En)
        {
            En = null;
            bool retres = false;
            input.Trim('0', '1', '2', '3', '4', '5', '6', '7', '8', '9');
            StringBuilder EnSB = new(input);
            ReadOnlySpan<char> Sinput = input.AsSpan();
            ref char searchSpace = ref MemoryMarshal.GetReference(Sinput);
            for (int i = 0; i < Sinput.Length; i++)
            {
                char currentchar = Unsafe.Add(ref searchSpace, i);
                if (Ar_Init.Contains(currentchar))
                {
                    retres = true;

                    EnSB[i] = currentchar switch
                    {
                        'ا' => 'a',
                        'ب' => 'b',
                        'ت' => 't',
                        'ث' => 't',
                        'ج' => 'j',
                        'ح' => 'h',
                        'خ' => '5',
                        'د' => 'd',
                        'ذ' => 'z',
                        'ر' => 'r',
                        'ز' => 'z',
                        'س' => 's',
                        'ش' => '7',
                        'ص' => 's',
                        'ض' => 'd',
                        'ط' => 't',
                        'ظ' => 'z',
                        'ع' => '3',
                        'غ' => '8',
                        'ف' => 'f',
                        'ق' => 'k',
                        'ك' => 'k',
                        'ل' => 'l',
                        'م' => 'm',
                        'ن' => 'n',
                        'ه' => 'h',
                        'و' => 'w',
                        'ي' => 'y',
                        'ى' => 'a',
                        'ة' => 'h',
                        'ؤ' => 'o',
                        'ء' => 'a',
                        'ئ' => 'i',
                        _ => throw new NotImplementedException(),
                    };
                }
            }
            if (retres)
            {
                EnSB.Replace("7", "ch");
                EnSB.Replace("8", "gh");
                EnSB.Replace("5", "kh");
                En = EnSB.ToString();
            }

            return retres;
        }
    }

}

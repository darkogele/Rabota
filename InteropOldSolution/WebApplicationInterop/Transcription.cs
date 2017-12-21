// Decompiled with JetBrains decompiler
// Type: WebApplicationInterop.Transcription
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

namespace WebApplicationInterop
{
  public class Transcription
  {
    public string DoTranscription(string word)
    {
      try
      {
        string str1 = string.Empty;
        for (int startIndex = 0; startIndex < word.Length; ++startIndex)
        {
          if (3 <= word.Length - startIndex)
          {
            string str2 = this.Compute(word.Substring(startIndex, 3));
            if (!str2.Equals(string.Empty))
            {
              str1 += str2;
              startIndex += 2;
            }
            else if (2 <= word.Length)
            {
              string str3 = this.Compute(word.Substring(startIndex, 2));
              if (!str3.Equals(string.Empty))
              {
                str1 += str3;
                ++startIndex;
              }
              else if (1 <= word.Length)
              {
                string str4 = this.Compute(word.Substring(startIndex, 1));
                if (!str4.Equals(string.Empty))
                  str1 += str4;
              }
            }
          }
          else if (2 <= word.Length - startIndex)
          {
            string str2 = this.Compute(word.Substring(startIndex, 2));
            if (!str2.Equals(string.Empty))
            {
              str1 += str2;
              ++startIndex;
            }
            else if (1 <= word.Length)
            {
              string str3 = this.Compute(word.Substring(startIndex, 1));
              if (!str3.Equals(string.Empty))
                str1 += str3;
            }
          }
          else if (1 <= word.Length - startIndex)
          {
            string str2 = this.Compute(word.Substring(startIndex, 1));
            if (!str2.Equals(string.Empty))
              str1 += str2;
          }
        }
        return str1;
      }
      catch
      {
        return "";
      }
    }

    private string Compute(string word)
    {
      switch (word)
      {
        case " ":
          return "";
        case "1":
          return "1";
        case "2":
          return "2";
        case "3":
          return "3";
        case "4":
          return "4";
        case "5":
          return "5";
        case "6":
          return "6";
        case "7":
          return "7";
        case "8":
          return "8";
        case "9":
          return "9";
        case "0":
          return "0";
        case "!":
          return "!";
        case "@":
          return "@";
        case "#":
          return "#";
        case "$":
          return "$";
        case "%":
          return "%";
        case "^":
          return "^";
        case "&":
          return "&";
        case "*":
          return "*";
        case "(":
          return "(";
        case ")":
          return ")";
        case "_":
          return "_";
        case "-":
          return "-";
        case "=":
          return "=";
        case "+":
          return "+";
        case "\\":
          return "\\";
        case "|":
          return "|";
        case "}":
          return "}";
        case "]":
          return "]";
        case "{":
          return "{";
        case "[":
          return "[";
        case "\"":
          return "\"";
        case "'":
          return "'";
        case ";":
          return ";";
        case ":":
          return ":";
        case "?":
          return "?";
        case ".":
          return ".";
        case "/":
          return "/";
        case ">":
          return ">";
        case "<":
          return "<";
        case "А":
          return "A";
        case "а":
          return "a";
        case "Б":
          return "B";
        case "б":
          return "b";
        case "В":
          return "V";
        case "в":
          return "v";
        case "Г":
          return "G";
        case "г":
          return "g";
        case "Д":
          return "D";
        case "д":
          return "d";
        case "Ѓ":
          return "Gj";
        case "gj":
          return "ѓ";
        case "Е":
          return "E";
        case "е":
          return "e";
        case "Ж":
          return "Zh";
        case "ж":
          return "zh";
        case "З":
          return "Z";
        case "з":
          return "z";
        case "Ѕ":
          return "Dz";
        case "ѕ":
          return "dz";
        case "И":
          return "I";
        case "и":
          return "i";
        case "Ј":
          return "J";
        case "ј":
          return "j";
        case "К":
          return "K";
        case "к":
          return "k";
        case "Л":
          return "L";
        case "л":
          return "l";
        case "Љ":
          return "Lj";
        case "љ":
          return "lj";
        case "М":
          return "M";
        case "м":
          return "m";
        case "Н":
          return "N";
        case "н":
          return "n";
        case "Њ":
          return "Nj";
        case "њ":
          return "nj";
        case "О":
          return "O";
        case "о":
          return "o";
        case "П":
          return "P";
        case "п":
          return "p";
        case "Р":
          return "R";
        case "р":
          return "r";
        case "С":
          return "S";
        case "с":
          return "s";
        case "Т":
          return "T";
        case "т":
          return "t";
        case "Ќ":
          return "Kj";
        case "ќ":
          return "kj";
        case "У":
          return "U";
        case "у":
          return "u";
        case "Ф":
          return "F";
        case "ф":
          return "f";
        case "Х":
          return "H";
        case "х":
          return "h";
        case "Ц":
          return "C";
        case "ц":
          return "c";
        case "Ч":
          return "Ch";
        case "ч":
          return "ch";
        case "Џ":
          return "Dj";
        case "џ":
          return "dj";
        case "Ш":
          return "Sh";
        case "ш":
          return "sh";
        default:
          return string.Empty;
      }
    }
  }
}

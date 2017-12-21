using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteropCyrilic
{
    class Program
    {
        static void Main(string[] args)
        {
            string value = "vlatche";
            string outputValue = value.ToCyrilic();

            Console.WriteLine(outputValue);
            Console.ReadLine();
        }

    }
    public static class ExtensionMethods
    {
        public static string ToCyrilic(this string value)
        {
            try
            {
                string result = "";
                if (value.Length > 0)
                {
                    var map = new Dictionary<string, string>
                    {
                        {"a", "а"},
                        {"b", "б"},
                        
                        {"ch", "ч"},
                        {"c", "ц"},
                        {"dj", "џ"}, // case
                        {"dz", "ѕ"}, // case
                        {"d", "д"},
                        {"e", "е"},
                        {"f", "ф"},
                        {"gј", "ѓ"}, // case
                        {"g", "г"},
                        {"h", "х"},
                        {"i", "и"},
                        {"j", "џ"},
                        {"k", "к"},
                        
                        {"lj", "љ"},
                        {"l", "л"},
                        {"m", "м"},
                        {"nj", "њ"}, // case
                        {"n", "н"},
                        {"o", "о"},
                        {"p", "п"},
                        //{"q", ""},
                        {"r", "р"},
                        {"sh", "ш"}, // case
                        {"s", "с"},
                        {"t", "т"},
                        {"u", "у"},
                        {"v", "в"},
                        //{"w", ""},
                        //{"x", ""},
                        //{"y", ""},
                        {"z", "з"},

                    };

                    result = string.Concat(value.Select(c => map[c.ToString()]));
                    //result = string.Join(",", map.Select(m => m.Key + "-" + m.Value).ToArray());


                    var originalResult = result;
                    for (int i = 0; i < result.Length - 1; i++)
                    {
                        if (map.ContainsValue(result[i].ToString()))
                        {
                            string output = String.Empty;
                            string chars = result[i].ToString() + result[i + 1].ToString();
                            switch (chars)
                            {
                                case "цх":
                                    output = "ч";
                                    originalResult = originalResult.Remove(i);
                                    //originalResult = originalResult.Remove(i);
                                    originalResult = originalResult.Insert(i, output);
                                    break;

                                // opfatete gi site case-ovi gore vo komentar gi ima

                                default:
                                    originalResult = originalResult + result[i + 1];
                                    break;

                            }
                        }
                    }

                    return originalResult;
                }
                else
                {
                    throw new Exception("Должината на влезната низа е 0");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

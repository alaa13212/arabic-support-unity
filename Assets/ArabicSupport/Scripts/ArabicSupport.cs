#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace ArabicSupport
{

//-----------------------------------------------------------------------------
/// <summary>
/// This is an Open Source File Created by: Abdullah Konash. Twitter: @konash
/// This File allow the users to use arabic text in XNA and Unity platform.
/// It flips the characters and replace them with the appropriate ones to connect the letters in the correct way.
/// 
/// The project is available on GitHub here: https://github.com/Konash/arabic-support-unity
/// Unity Asset Store link: https://www.assetstore.unity3d.com/en/#!/content/2674
/// Please help in improving the plugin. 
/// 
/// I would love to see the work you use this plugin for. Send me a copy at: abdullah.konash[at]gmail[dot]com
/// </summary>
/// 
/// <license>
/// MIT License
/// 
/// Copyright(c) 2018
/// Abdullah Konash
/// 
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// /// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
/// 
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
/// 
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.
/// </license>

//-----------------------------------------------------------------------------


	
	public class ArabicFixer
	{	
		/// <summary>
		/// Fix the specified string.
		/// </summary>
		/// <param name='str'>
		/// String to be fixed.
		/// </param>
		public static string Fix(string str) => Fix(str, false, true);

		public static string Fix(string str, bool rtl)
		{
			if(rtl)
			{
				return Fix(str);
			}
			else
			{
				string[] words = str.Split(null);
				string result = "";
				string arabicToIgnore = "";
				foreach(string word in words)
				{
					if(char.IsLower(word.ToLower()[word.Length/2]))
					{
						result += Fix(arabicToIgnore) + word + " ";
						arabicToIgnore = "";
					}
					else
					{
						arabicToIgnore += word + " ";
					}
				}
				if(arabicToIgnore != "")
					result += Fix(arabicToIgnore);
				
				return result;
			}
		}
		
		/// <summary>
		/// Fix the specified string with customization options.
		/// </summary>
		/// <param name='str'>
		/// String to be fixed.
		/// </param>
		/// <param name='showTashkeel'>
		/// Show tashkeel.
		/// </param>
		/// <param name='useHinduNumbers'>
		/// Use hindu numbers.
		/// </param>
		public static string Fix(string str, bool showTashkeel, bool useHinduNumbers)
		{
			ArabicFixerTool.showTashkeel = showTashkeel;
			ArabicFixerTool.useHinduNumbers =useHinduNumbers;
			
			if(str.Contains("\n"))
				str = str.Replace("\n", Environment.NewLine);

			string[] stringSeparators = {Environment.NewLine};
			string[] lines = str.Split(stringSeparators, StringSplitOptions.None);

			return string.Join(Environment.NewLine, lines.Select(ArabicFixerTool.FixLine));

		}

        public static string Fix(string str, bool showTashkeel, bool combineTashkeel, bool useHinduNumbers)
        {
            ArabicFixerTool.combineTashkeel = combineTashkeel;
            return Fix(str, showTashkeel, useHinduNumbers);
        }


    }
	
}

/// <summary>
/// Arabic Contextual forms General - Unicode
/// </summary>
internal enum IsolatedArabicLetters
{
	Hamza = 0xFE80,
	Alef = 0xFE8D,
	AlefHamza = 0xFE83,
	WawHamza = 0xFE85,
	AlefMaksoor = 0xFE87,
	AlefMaksora = 0xFBFC,
	HamzaNabera = 0xFE89,
	Ba = 0xFE8F,
	Ta = 0xFE95,
	Tha2 = 0xFE99,
	Jeem = 0xFE9D,
	H7aa = 0xFEA1,
	Khaa2 = 0xFEA5,
	Dal = 0xFEA9,
	Thal = 0xFEAB,
	Ra2 = 0xFEAD,
	Zeen = 0xFEAF,
	Seen = 0xFEB1,
	Sheen = 0xFEB5,
	S9a = 0xFEB9,
	Dha = 0xFEBD,
	T6a = 0xFEC1,
	T6ha = 0xFEC5,
	Ain = 0xFEC9,
	Gain = 0xFECD,
	Fa = 0xFED1,
	Gaf = 0xFED5,
	Kaf = 0xFED9,
	Lam = 0xFEDD,
	Meem = 0xFEE1,
	Noon = 0xFEE5,
	Ha = 0xFEE9,
	Waw = 0xFEED,
	Ya = 0xFEF1,
	AlefMad = 0xFE81,
	TaMarboota = 0xFE93,
	PersianPe = 0xFB56,  	// Persian Letters;
	PersianChe = 0xFB7A,
	PersianZe = 0xFB8A,
	PersianGaf = 0xFB92,
	PersianGaf2 = 0xFB8E,
	PersianYeh = 0xFBFC,
	
}

/// <summary>
/// Arabic Contextual forms - Isolated
/// </summary>
internal enum GeneralArabicLetters
{
	Hamza = 0x0621,
	Alef = 0x0627,
	AlefHamza = 0x0623,
	WawHamza = 0x0624,
	AlefMaksoor = 0x0625,
	AlefMagsora = 0x0649,
	HamzaNabera = 0x0626,
	Ba = 0x0628,
	Ta = 0x062A,
	Tha2 = 0x062B,
	Jeem = 0x062C,
	H7aa = 0x062D,
	Khaa2 = 0x062E,
	Dal = 0x062F,
	Thal = 0x0630,
	Ra2 = 0x0631,
	Zeen = 0x0632,
	Seen = 0x0633,
	Sheen = 0x0634,
	S9a = 0x0635,
	Dha = 0x0636,
	T6a = 0x0637,
	T6ha = 0x0638,
	Ain = 0x0639,
	Gain = 0x063A,
	Fa = 0x0641,
	Gaf = 0x0642,
	Kaf = 0x0643,
	Lam = 0x0644,
	Meem = 0x0645,
	Noon = 0x0646,
	Ha = 0x0647,
	Waw = 0x0648,
	Ya = 0x064A,
	AlefMad = 0x0622,
	TaMarboota = 0x0629,
	PersianPe = 0x067E,		// Persian Letters;
	PersianChe = 0x0686,
	PersianZe = 0x0698,
	PersianGaf = 0x06AF,
	PersianGaf2 = 0x06A9,
	PersianYeh = 0x06CC,
	
}

/// <summary>
/// Sets up and creates the conversion table 
/// </summary>
internal static class ArabicTable
{
	private static Dictionary<int, int> mapList;

	public static List<int> BlocksConnection = new List<int>
	{
		(int) IsolatedArabicLetters.Alef,
		(int) IsolatedArabicLetters.Dal,
		(int) IsolatedArabicLetters.Thal,
		(int) IsolatedArabicLetters.Ra2,
		(int) IsolatedArabicLetters.Zeen,
		(int) IsolatedArabicLetters.PersianZe,
		(int) IsolatedArabicLetters.Waw,
		(int) IsolatedArabicLetters.AlefMad,
		(int) IsolatedArabicLetters.AlefHamza,
		(int) IsolatedArabicLetters.Hamza,
		(int) IsolatedArabicLetters.AlefMaksoor,
		(int) IsolatedArabicLetters.WawHamza,
	};
	
	/// <summary>
	/// Setting up the conversion table
	/// </summary>
	static ArabicTable()
	{
		mapList = new Dictionary<int, int>
		{
			{(int) GeneralArabicLetters.Hamza, (int) IsolatedArabicLetters.Hamza},
			{(int) GeneralArabicLetters.Alef, (int) IsolatedArabicLetters.Alef},
			{(int) GeneralArabicLetters.AlefHamza, (int) IsolatedArabicLetters.AlefHamza},
			{(int) GeneralArabicLetters.WawHamza, (int) IsolatedArabicLetters.WawHamza},
			{(int) GeneralArabicLetters.AlefMaksoor, (int) IsolatedArabicLetters.AlefMaksoor},
			{(int) GeneralArabicLetters.AlefMagsora, (int) IsolatedArabicLetters.AlefMaksora},
			{(int) GeneralArabicLetters.HamzaNabera, (int) IsolatedArabicLetters.HamzaNabera},
			{(int) GeneralArabicLetters.Ba, (int) IsolatedArabicLetters.Ba},
			{(int) GeneralArabicLetters.Ta, (int) IsolatedArabicLetters.Ta},
			{(int) GeneralArabicLetters.Tha2, (int) IsolatedArabicLetters.Tha2},
			{(int) GeneralArabicLetters.Jeem, (int) IsolatedArabicLetters.Jeem},
			{(int) GeneralArabicLetters.H7aa, (int) IsolatedArabicLetters.H7aa},
			{(int) GeneralArabicLetters.Khaa2, (int) IsolatedArabicLetters.Khaa2},
			{(int) GeneralArabicLetters.Dal, (int) IsolatedArabicLetters.Dal},
			{(int) GeneralArabicLetters.Thal, (int) IsolatedArabicLetters.Thal},
			{(int) GeneralArabicLetters.Ra2, (int) IsolatedArabicLetters.Ra2},
			{(int) GeneralArabicLetters.Zeen, (int) IsolatedArabicLetters.Zeen},
			{(int) GeneralArabicLetters.Seen, (int) IsolatedArabicLetters.Seen},
			{(int) GeneralArabicLetters.Sheen, (int) IsolatedArabicLetters.Sheen},
			{(int) GeneralArabicLetters.S9a, (int) IsolatedArabicLetters.S9a},
			{(int) GeneralArabicLetters.Dha, (int) IsolatedArabicLetters.Dha},
			{(int) GeneralArabicLetters.T6a, (int) IsolatedArabicLetters.T6a},
			{(int) GeneralArabicLetters.T6ha, (int) IsolatedArabicLetters.T6ha},
			{(int) GeneralArabicLetters.Ain, (int) IsolatedArabicLetters.Ain},
			{(int) GeneralArabicLetters.Gain, (int) IsolatedArabicLetters.Gain},
			{(int) GeneralArabicLetters.Fa, (int) IsolatedArabicLetters.Fa},
			{(int) GeneralArabicLetters.Gaf, (int) IsolatedArabicLetters.Gaf},
			{(int) GeneralArabicLetters.Kaf, (int) IsolatedArabicLetters.Kaf},
			{(int) GeneralArabicLetters.Lam, (int) IsolatedArabicLetters.Lam},
			{(int) GeneralArabicLetters.Meem, (int) IsolatedArabicLetters.Meem},
			{(int) GeneralArabicLetters.Noon, (int) IsolatedArabicLetters.Noon},
			{(int) GeneralArabicLetters.Ha, (int) IsolatedArabicLetters.Ha},
			{(int) GeneralArabicLetters.Waw, (int) IsolatedArabicLetters.Waw},
			{(int) GeneralArabicLetters.Ya, (int) IsolatedArabicLetters.Ya},
			{(int) GeneralArabicLetters.AlefMad, (int) IsolatedArabicLetters.AlefMad},
			{(int) GeneralArabicLetters.TaMarboota, (int) IsolatedArabicLetters.TaMarboota},
			
			// Persian Letters;
			{(int) GeneralArabicLetters.PersianPe, (int) IsolatedArabicLetters.PersianPe},
			{(int) GeneralArabicLetters.PersianChe, (int) IsolatedArabicLetters.PersianChe},
			{(int) GeneralArabicLetters.PersianZe, (int) IsolatedArabicLetters.PersianZe},
			{(int) GeneralArabicLetters.PersianGaf, (int) IsolatedArabicLetters.PersianGaf},
			{(int) GeneralArabicLetters.PersianGaf2, (int) IsolatedArabicLetters.PersianGaf2},
			{(int) GeneralArabicLetters.PersianYeh, (int) IsolatedArabicLetters.PersianYeh}
		};
	}

	internal static int Convert(int toBeConverted)
	{
		if (mapList.ContainsKey(toBeConverted))
			return mapList[toBeConverted];
		return toBeConverted;
	}

	internal static bool IsIsolatedForm(int ch)
	{
		return ch <= (char)0xFEFF && ch >= (char)0xFE70 || mapList.ContainsValue(ch);
	}
	
	
}


internal class TashkeelLocation
{
	public char tashkeel;
	public int position;
	public TashkeelLocation(char tashkeel, int position)
	{
		this.tashkeel = tashkeel;
		this.position = position;
	}
}


internal class ArabicFixerTool
{
	internal static bool showTashkeel = true;
    internal static bool combineTashkeel = true;
    internal static bool useHinduNumbers = false;
	
	
	internal static string RemoveTashkeel(string str, out List<TashkeelLocation> tashkeelLocation)
	{
		tashkeelLocation = new List<TashkeelLocation>();
		char[] letters = str.ToCharArray();

		int index = 0;
		for (int i = 0; i < letters.Length; i++) {
			if (letters [i] == (char)0x064B) { // Tanween Fatha
				tashkeelLocation.Add (new TashkeelLocation ((char)0x064B, i));
				index++;
			}
            else if (letters [i] == (char)0x064C) { // Tanween Damma
				tashkeelLocation.Add (new TashkeelLocation ((char)0x064C, i));
				index++;
			}
            else if (letters [i] == (char)0x064D){ // Tanween Kasra
				tashkeelLocation.Add (new TashkeelLocation ((char)0x064D, i));
				index++;
			}
            else if (letters [i] == (char)0x064E) { // Fatha
				if(index > 0 && combineTashkeel)
				{
					if(tashkeelLocation[index-1].tashkeel == (char)0x0651 ) // Shadda
					{
						tashkeelLocation [index - 1].tashkeel = (char)0xFC60; // Shadda With Fatha
						continue;
					}
				}

				tashkeelLocation.Add (new TashkeelLocation ((char)0x064E, i));
				index++;
			}
            else if (letters [i] == (char)0x064F) { // DAMMA
				if (index > 0 && combineTashkeel) {
					if (tashkeelLocation [index - 1].tashkeel == (char)0x0651) { // SHADDA
						tashkeelLocation [index - 1].tashkeel = (char)0xFC61; // Shadda With DAMMA
						continue;
					}
				}
				tashkeelLocation.Add (new TashkeelLocation ((char)0x064F, i));
				index++;
			}
            else if (letters [i] == (char)0x0650) { // KASRA
				if (index > 0 && combineTashkeel) {
					if (tashkeelLocation [index - 1].tashkeel == (char)0x0651) { // SHADDA
						tashkeelLocation [index - 1].tashkeel = (char)0xFC62; // Shadda With KASRA
						continue;
					}
				}
				tashkeelLocation.Add (new TashkeelLocation ((char)0x0650, i));
				index++;
			}
            else if (letters [i] == (char)0x0651) { // SHADDA
				if(index > 0 && combineTashkeel)
				{
					if(tashkeelLocation[index-1].tashkeel == (char)0x064E ) // FATHA
					{
						tashkeelLocation [index - 1].tashkeel = (char)0xFC60; // Shadda With Fatha
						continue;
					}

					if(tashkeelLocation[index-1].tashkeel == (char)0x064F ) // DAMMA
					{
						tashkeelLocation [index - 1].tashkeel = (char)0xFC61; // Shadda With DAMMA
						continue;
					}

					if(tashkeelLocation[index-1].tashkeel == (char)0x0650 ) // KASRA
					{
						tashkeelLocation [index - 1].tashkeel = (char)0xFC62; // Shadda With KASRA
						continue;
					}
				}

				tashkeelLocation.Add (new TashkeelLocation ((char)0x0651, i));
				index++;
			}
            else if (letters [i] == (char)0x0652) { // SUKUN
				tashkeelLocation.Add (new TashkeelLocation ((char)0x0652, i));
				index++;
			}
            else if (letters [i] == (char)0x0653) { // MADDAH ABOVE
				tashkeelLocation.Add (new TashkeelLocation ((char)0x0653, i));
				index++;
			}
		}
		
		string[] split = str.Split(new char[]{(char)0x064B,(char)0x064C,(char)0x064D,
			(char)0x064E,(char)0x064F,(char)0x0650,
		
			(char)0x0651,(char)0x0652,(char)0x0653,(char)0xFC60,(char)0xFC61,(char)0xFC62});
		str = "";
		
		foreach(string s in split)
		{
			str += s;
		}
		
		return str;
	}
	
	internal static char[] ReturnTashkeel(char[] letters, List<TashkeelLocation> tashkeelLocation)
	{
		char[] lettersWithTashkeel = new char[letters.Length + tashkeelLocation.Count];
		
		int letterWithTashkeelTracker = 0;
		for(int i = 0; i<letters.Length; i++)
		{
			lettersWithTashkeel[letterWithTashkeelTracker] = letters[i];
			letterWithTashkeelTracker++;
			foreach(TashkeelLocation hLocation in tashkeelLocation)
			{
				if(hLocation.position == letterWithTashkeelTracker)
				{
					lettersWithTashkeel[letterWithTashkeelTracker] = hLocation.tashkeel;
					letterWithTashkeelTracker++;
				}
			}
		}
		
		return lettersWithTashkeel;
	}
	
	/// <summary>
	/// Converts a string to a form in which the sting will be displayed correctly for arabic text.
	/// </summary>
	/// <param name="str">String to be converted. Example: "Aaa"</param>
	/// <returns>Converted string. Example: "aa aaa A" without the spaces.</returns>
	internal static string FixLine(string str)
	{
		List<TashkeelLocation> tashkeelLocation;
		
		string originString = RemoveTashkeel(str, out tashkeelLocation);
		
		char[] lettersOrigin = originString.ToCharArray();
		char[] lettersFinal = originString.ToCharArray();
		
		
		for (int i = 0; i < lettersOrigin.Length; i++)
		{
			lettersOrigin[i] = (char)ArabicTable.Convert(lettersOrigin[i]);
		}
		
		for (int i = 0; i < lettersOrigin.Length; i++)
		{
			bool skip = false;
			
			// For special Lam Letter connections.
			if (lettersOrigin[i] == (char)IsolatedArabicLetters.Lam && i < lettersOrigin.Length - 1)
			{
				skip = FixSpecialLam(lettersOrigin, i, lettersFinal);
			}

			if (!IsIgnoredCharacter(lettersOrigin[i]))
			{
				if (IsMiddleLetter(lettersOrigin, i))
					lettersFinal[i] = (char)(lettersOrigin[i] + 3);
				else if (IsFinishingLetter(lettersOrigin, i))
					lettersFinal[i] = (char)(lettersOrigin[i] + 1);
				else if (IsLeadingLetter(lettersOrigin, i))
					lettersFinal[i] = (char)(lettersOrigin[i] + 2);
			}

			if (skip)
				i++;
			
			//chaining numbers to hindu
			if(useHinduNumbers && lettersOrigin[i] >= 0x0030 && lettersOrigin[i] <= 0x0039){
				lettersFinal[i] = (char) (lettersOrigin[i] + 0x0630);
			}
			
		}
		
		
		
		//Return the Tashkeel to their places.
		if(showTashkeel)
			lettersFinal = ReturnTashkeel(lettersFinal, tashkeelLocation);
		
		
		List<char> list = new List<char>();
		
		List<char> numberList = new List<char>();
		
		for (int i = lettersFinal.Length - 1; i >= 0; i--)
		{
			if (char.IsPunctuation(lettersFinal[i]) && i>0 && i < lettersFinal.Length-1 &&
			    (char.IsPunctuation(lettersFinal[i-1]) || char.IsPunctuation(lettersFinal[i+1])))
			{
				if (lettersFinal[i] == '(')
					list.Add(')');
				else if (lettersFinal[i] == ')')
					list.Add('(');
				else if (lettersFinal[i] == '<')
					list.Add('>');
				else if (lettersFinal[i] == '>')
					list.Add('<');
				else if (lettersFinal[i] == '[')
					list.Add(']');
				else if (lettersFinal[i] == ']')
					list.Add('[');
				else if (lettersFinal[i] != 0xFFFF)
					list.Add(lettersFinal[i]);
			}
			// For cases where english words and arabic are mixed. This allows for using arabic, english and numbers in one sentence.
			else if(lettersFinal[i] == ' ' && i > 0 && i < lettersFinal.Length-1 &&
			        (char.IsLower(lettersFinal[i-1]) || char.IsUpper(lettersFinal[i-1]) || char.IsNumber(lettersFinal[i-1])) &&
			        (char.IsLower(lettersFinal[i+1]) || char.IsUpper(lettersFinal[i+1]) ||char.IsNumber(lettersFinal[i+1])))
				
			{
				numberList.Add(lettersFinal[i]);
			}
			
			else if (char.IsNumber(lettersFinal[i]) || char.IsLower(lettersFinal[i]) ||
			         char.IsUpper(lettersFinal[i]) || char.IsSymbol(lettersFinal[i]) ||
			         char.IsPunctuation(lettersFinal[i]))// || lettersFinal[i] == '^') //)
			{
				
				if (lettersFinal[i] == '(')
					numberList.Add(')');
				else if (lettersFinal[i] == ')')
					numberList.Add('(');
				else if (lettersFinal[i] == '<')
					numberList.Add('>');
				else if (lettersFinal[i] == '>')
					numberList.Add('<');
				else if (lettersFinal[i] == '[')
					list.Add(']');
				else if (lettersFinal[i] == ']')
					list.Add('[');
				else
					numberList.Add(lettersFinal[i]);
			}
			else if( (lettersFinal[i] >= (char)0xD800 && lettersFinal[i] <= (char)0xDBFF) ||
			        (lettersFinal[i] >= (char)0xDC00 && lettersFinal[i] <= (char)0xDFFF))
			{
				numberList.Add(lettersFinal[i]);
			}
			else
			{
				if (numberList.Count > 0)
				{
					for (int j = 0; j < numberList.Count; j++)
						list.Add(numberList[numberList.Count - 1 - j]);
					numberList.Clear();
				}
				if (lettersFinal[i] != 0xFFFF)
					list.Add(lettersFinal[i]);
				
			}
		}
		if (numberList.Count > 0)
		{
			for (int j = 0; j < numberList.Count; j++)
				list.Add(numberList[numberList.Count - 1 - j]);
			numberList.Clear();
		}
		
		// Moving letters from a list to an array.
		lettersFinal = new char[list.Count];
		for (int i = 0; i < lettersFinal.Length; i++)
			lettersFinal[i] = list[i];
		
		
		str = new string(lettersFinal);
		return str;
	}

	private static bool FixSpecialLam(char[] lettersOrigin, int i, char[] lettersFinal)
	{
		if (lettersOrigin[i + 1] == (char) IsolatedArabicLetters.AlefMaksoor)
		{
			lettersOrigin[i] = (char) 0xFEF7;
			lettersFinal[i + 1] = (char) 0xFFFF;
			return true;
		}
		if (lettersOrigin[i + 1] == (char) IsolatedArabicLetters.Alef)
		{
			lettersOrigin[i] = (char) 0xFEF9;
			lettersFinal[i + 1] = (char) 0xFFFF;
			return true;
		}
		if (lettersOrigin[i + 1] == (char) IsolatedArabicLetters.AlefHamza)
		{
			lettersOrigin[i] = (char) 0xFEF5;
			lettersFinal[i + 1] = (char) 0xFFFF;
			return true;
		}
		if (lettersOrigin[i + 1] == (char) IsolatedArabicLetters.AlefMad)
		{
			lettersOrigin[i] = (char) 0xFEF3;
			lettersFinal[i + 1] = (char) 0xFFFF;
			return true;
		}

		return false;
	}

	/// <summary>
	/// English letters, numbers and punctuation characters are ignored. This checks if the ch is an ignored character.
	/// </summary>
	/// <param name="ch">The character to be checked for skipping</param>
	/// <returns>True if the character should be ignored, false if it should not be ignored.</returns>
	internal static bool IsIgnoredCharacter(char ch)
	{
		return char.IsPunctuation(ch) ||
		       char.IsNumber(ch) ||
		       char.IsLower(ch) ||
		       char.IsUpper(ch) ||
		       char.IsSymbol(ch) ||
		       !ArabicTable.IsIsolatedForm(ch);
	}
	
	/// <summary>
	/// Checks if the letter at index value is a leading character in Arabic or not.
	/// </summary>
	/// <param name="letters">The whole word that contains the character to be checked</param>
	/// <param name="index">The index of the character to be checked</param>
	/// <returns>True if the character at index is a leading character, else, returns false</returns>
	internal static bool IsLeadingLetter(char[] letters, int index)
	{
		bool lettersThatCannotBeBeforeALeadingLetter = 
			index == 0
           || char.IsWhiteSpace(letters[index - 1])
           || char.IsSymbol(letters[index - 1])
           || char.IsPunctuation(letters[index - 1])
           || ArabicTable.BlocksConnection.Contains(letters[index - 1]);

		bool lettersThatCannotBeALeadingLetter = 
			!char.IsWhiteSpace(letters[index])
			&& !ArabicTable.BlocksConnection.Contains(letters[index]);

		bool lettersThatCannotBeAfterLeadingLetter = 
			index < letters.Length - 1 
			&& !char.IsWhiteSpace(letters[index + 1])
			&& !char.IsPunctuation(letters[index + 1] )
			&& !char.IsNumber(letters[index + 1])
			&& !char.IsSymbol(letters[index + 1])
			&& !char.IsLower(letters[index + 1])
			&& !char.IsUpper(letters[index + 1])
			&& letters[index + 1] != (int)IsolatedArabicLetters.Hamza;

		return lettersThatCannotBeBeforeALeadingLetter && lettersThatCannotBeALeadingLetter && lettersThatCannotBeAfterLeadingLetter;
	}
	
	/// <summary>
	/// Checks if the letter at index value is a finishing character in Arabic or not.
	/// </summary>
	/// <param name="letters">The whole word that contains the character to be checked</param>
	/// <param name="index">The index of the character to be checked</param>
	/// <returns>True if the character at index is a finishing character, else, returns false</returns>
	internal static bool IsFinishingLetter(char[] letters, int index)
	{
		bool lettersThatCannotBeBeforeAFinishingLetter =
			index > 0
			&& !char.IsWhiteSpace(letters[index - 1])
			&& !char.IsPunctuation(letters[index - 1])
			&& !char.IsSymbol(letters[index - 1])
			&& !ArabicTable.BlocksConnection.Contains(letters[index - 1]);

		bool lettersThatCannotBeFinishingLetters = 
			!char.IsWhiteSpace(letters[index])&& letters[index]
			!= (int)IsolatedArabicLetters.Hamza;

		return lettersThatCannotBeBeforeAFinishingLetter && lettersThatCannotBeFinishingLetters;
	}

	/// <summary>
	/// Checks if the letter at index value is a middle character in Arabic or not.
	/// </summary>
	/// <param name="letters">The whole word that contains the character to be checked</param>
	/// <param name="index">The index of the character to be checked</param>
	/// <returns>True if the character at index is a middle character, else, returns false</returns>
	internal static bool IsMiddleLetter(char[] letters, int index)
	{
		bool lettersThatCannotBeMiddleLetters =
			index > 0
			&& !ArabicTable.BlocksConnection.Contains(letters[index]);

		bool lettersThatCannotBeBeforeMiddleCharacters =
			index > 0
			&& !char.IsWhiteSpace(letters[index - 1])
			&& !char.IsPunctuation(letters[index - 1])
			&& !char.IsSymbol(letters[index - 1])
			&& !ArabicTable.BlocksConnection.Contains(letters[index - 1]);

		bool lettersThatCannotBeAfterMiddleCharacters =
			index < letters.Length - 1
			&& !char.IsWhiteSpace(letters[index + 1])
			&& !char.IsNumber(letters[index + 1])
			&& !char.IsSymbol(letters[index + 1])
			&& !char.IsPunctuation(letters[index + 1])
			&& letters[index + 1] != (int) IsolatedArabicLetters.Hamza;

		return lettersThatCannotBeAfterMiddleCharacters && lettersThatCannotBeBeforeMiddleCharacters && lettersThatCannotBeMiddleLetters;
	}
}

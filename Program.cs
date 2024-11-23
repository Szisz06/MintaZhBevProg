using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MintaZhBevProg
{
    internal class Program
    {

        static string[,] Betolt (string fajlNev)
        {
            string[] lines = File.ReadAllLines (fajlNev);
            string [] data = lines[0].Split(';');
            string[,] outMatrix = new string[lines.Length,data.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                data = lines[i].Split(';');
                for (int j = 0; j < data.Length; j++) {
                    outMatrix[i, j] = data[j];
                }
            }
            return outMatrix;
        }

        static void Kiir(string[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                { Console.Write($"{matrix[i, j]}\t"); }
                Console.WriteLine();
            }
        }

        static string LegnagyobbHullam(string[,] matrix)
        {
            int maxI = 1; //első sor miatt
            int maxJ = 3; //harmadiktól nézett adatokkal dolgozunk talán ?
            for (int i = 1;i < matrix.GetLength(0);i++)
            {
                for(int j = 3;j < matrix.GetLength(1);j++)
                {
                    double maxElem = double.Parse(matrix[maxI,maxJ]);
                    double currentElem = double.Parse(matrix[i,j]);
                    if (currentElem > maxElem)
                    {
                        maxI = i;
                        maxJ = j;
                    }
                }
            }
            return matrix[maxI, 0].ToString();
        }
        static bool VanE(string[,] matrix, string pilotaNeve, string insulasHelyszin)
        { 
            for(int i = 1; i < matrix.GetLength(0); i++)
            { if (matrix[i, 1] == pilotaNeve && matrix[i, 2] == insulasHelyszin) { return true; } } return false;
        }
        static bool VanE2(string[,] matrix, string pilotaNeve, string insulasHelyszin)
        {
            bool vanPilota = false;
            bool vanIndulasHelyszine = false;
            for(int i = 1;i < matrix.GetLength(0);i++)
            {
                for (int j = 1; j < matrix.GetLength(1); j++)
                { if (matrix[i, 1] == pilotaNeve)
                    { vanPilota = true; }
                    if (matrix[i, 1] == insulasHelyszin)
                    { vanIndulasHelyszine = true; }
                }if (vanPilota && vanIndulasHelyszine) { return true; }
            }return false;
        }
        static void Kerekit(ref string[,]  matrix)
        {
            for(int i = 1;  i < matrix.GetLength(0); i++)
            {
                for (int j = 3; j < matrix.GetLength(1); j++)
                {
                    
                    double number = Math.Ceiling(double.Parse(matrix[i, j].Replace(',','.'), System.Globalization.CultureInfo.InvariantCulture));
                    matrix[i,j] = number.ToString();
                }
            }
        }

        static string[,] Betolt2(string fajlNev)
        {
            int rowNumber = 0;
            int clnNuber = 0;
            StreamReader myReader = new StreamReader(fajlNev, System.Text.Encoding.Unicode);
            while (!myReader.EndOfStream) {
                string row  = myReader.ReadLine();
                if (rowNumber == 0) {
                    clnNuber = row.Split(';').Length ;
                }
                rowNumber++;
            }
            myReader.Close();
            string[,] outMatrix = new string[rowNumber, clnNuber];
            StreamReader myReader2 = new StreamReader(fajlNev, System.Text.Encoding.Unicode);
            {
                int i = 0;
                while (!myReader2.EndOfStream)
                {
                    string row = myReader2.ReadLine();
                    string[] elements = row.Split(';');
                    for (int j = 0; j < elements.Length; j++)
                    {
                        outMatrix[i, j] = elements[j];
                        j++;
                    }
                }
            }myReader2.Close();
            return outMatrix;
        }
        static void Main(string[] args)
        {
            string[,] matrix = Betolt("meres.csv");
            Console.WindowWidth =matrix.GetLength(0)*20;
            Kiir(matrix);
            Console.WriteLine(" legnagyobb hullám dáruma: "+ LegnagyobbHullam(matrix));
            Console.WriteLine("Kérem a pilóta nevét: ");
            string pilotaNeve = Console.ReadLine(); 
            Console.WriteLine("Kérem az indulás helyét : ");
            string insulasHelyszin = Console.ReadLine();
            if (VanE(matrix, pilotaNeve, insulasHelyszin)) {
                Console.WriteLine("Van mérés");
            }
            else { Console.WriteLine("nins mérés"); }
            Kerekit(ref matrix);
            Kiir(matrix);
        }
    }
}

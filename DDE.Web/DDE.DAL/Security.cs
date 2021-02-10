using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace DDE.DAL
{
   public class Security
    {
        string a = "ab", A = "AB";

        string b = "ab", B = "AB";
        string c = "cd", C = "CD";
        string d = "dc", D = "DC";
        string e = "ef", E = "EF";
        string f = "fe", F = "FE";
        string g = "gh", G = "GH";
        string h = "hg", H = "HG";
        string i = "ij", I = "IJ";
        string j = "ji", J = "JI";
        string k = "kl", K = "KL";
        string l = "lk", L = "LK";
        string m = "mn", M = "MN";
        string n = "nm", N = "NM";
        string o = "op", O = "OP";
        string p = "po", P = "PO";
        string q = "qr", Q = "QR";
        string r = "rq", R = "RQ";
        string s = "st", S = "ST";
        string t = "ts", T = "TS";
        string u = "uv", U = "UV";
        string v = "vu", V = "VU";
        string w = "wx", W = "WX";
        string x = "xw", X = "XW";
        string y = "yz", Y = "YZ";
        string z = "zy", Z = "ZY";
        string num0 = "01";
        string num1 = "10";
        string num2 = "23";
        string num3 = "32";
        string num4 = "45";
        string num5 = "54";

        string num6 = "67";
        string num7 = "76";
        string num8 = "89";
        string num9 = "98";


        public String EncryptOneWay(String str)
        {
            String encrypted = "";
            for (int ii = 0; ii < str.Length; ii++)
            {
                if ((str.Substring(ii, 1) == "a"))
                {

                    encrypted = encrypted + a;
                }
                else if (str.Substring(ii, 1) == "A")
                {
                    encrypted = encrypted + A;

                }

                else if (str.Substring(ii, 1) == "b")
                {
                    encrypted = encrypted + b;

                }
                else if (str.Substring(ii, 1) == "B")
                {


                }
                else if (str.Substring(ii, 1) == "c")
                {
                    encrypted = encrypted + c;

                }
                else if (str.Substring(ii, 1) == "C")
                {
                    encrypted = encrypted + C;

                }


                else if (str.Substring(ii, 1) == "d")
                {
                    encrypted = encrypted + d;
                }
                else if (str.Substring(ii, 1) == "D")
                {

                    encrypted = encrypted + D;

                }

                else if (str.Substring(ii, 1) == "e")
                {
                    encrypted = encrypted + e;

                }
                else if (str.Substring(ii, 1) == "E")
                {
                    encrypted = encrypted + E;

                }

                else if (str.Substring(ii, 1) == "f")
                {
                    encrypted = encrypted + f;

                }
                else if (str.Substring(ii, 1) == "F")
                {
                    encrypted = encrypted + F;

                }

                else if (str.Substring(ii, 1) == "g")
                {
                    encrypted = encrypted + g;
                }
                else if (str.Substring(ii, 1) == "G")
                {
                    encrypted = encrypted + G;
                }

                else if (str.Substring(ii, 1) == "h")
                {
                    encrypted = encrypted + h;
                }
                else if (str.Substring(ii, 1) == "H")
                {
                    encrypted = encrypted + H;

                }
                else if (str.Substring(ii, 1) == "i")
                {

                    encrypted = encrypted + i;

                }
                else if (str.Substring(ii, 1) == "I")
                {
                    encrypted = encrypted + I;

                }
                else if (str.Substring(ii, 1) == "j")
                {

                    encrypted = encrypted + j;
                }
                else if (str.Substring(ii, 1) == "J")
                {
                    encrypted = encrypted + J;

                }
                else if (str.Substring(ii, 1) == "k")
                {
                    encrypted = encrypted + k;
                }

                else if (str.Substring(ii, 1) == "K")
                {
                    encrypted = encrypted + K;

                }
                else if (str.Substring(ii, 1) == "l")
                {
                    encrypted = encrypted + l;

                }
                else if (str.Substring(ii, 1) == "L")
                {
                    encrypted = encrypted + L;

                }
                else if (str.Substring(ii, 1) == "m")
                {
                    encrypted = encrypted + m;

                }
                else if (str.Substring(ii, 1) == "M")
                {

                    encrypted = encrypted + M;
                }
                else if (str.Substring(ii, 1) == "n")
                {
                    encrypted = encrypted + n;
                }
                else if (str.Substring(ii, 1) == "N")
                {
                    encrypted = encrypted + N;

                }

                else if (str.Substring(ii, 1) == "o")
                {
                    encrypted = encrypted + o;
                }

                else if (str.Substring(ii, 1) == "O")
                {
                    encrypted = encrypted + O;

                }
                else if (str.Substring(ii, 1) == "p")
                {
                    encrypted = encrypted + p;
                }
                else if (str.Substring(ii, 1) == "P")
                {

                    encrypted = encrypted + P;
                }
                else if (str.Substring(ii, 1) == "q")
                {
                    encrypted = encrypted + q;
                }
                else if (str.Substring(ii, 1) == "Q")
                {
                    encrypted = encrypted + Q;

                }
                else if (str.Substring(ii, 1) == "r")
                {
                    encrypted = encrypted + r;

                }

                else if (str.Substring(ii, 1) == "R")
                {
                    encrypted = encrypted + R;

                }
                else if (str.Substring(ii, 1) == "s")
                {
                    encrypted = encrypted + s;



                }
                else if (str.Substring(ii, 1) == "S")
                {

                    encrypted = encrypted + R;
                }

                else if (str.Substring(ii, 1) == "t")
                {
                    encrypted = encrypted + t;

                }
                else if (str.Substring(ii, 1) == "T")
                {
                    encrypted = encrypted + T;

                }

                else if (str.Substring(ii, 1) == "u")
                {
                    encrypted = encrypted + u;


                }
                else if (str.Substring(ii, 1) == "U")
                {
                    encrypted = encrypted + U;

                }
                else if (str.Substring(ii, 1) == "v")
                {
                    encrypted = encrypted + v;
                }

                else if (str.Substring(ii, 1) == "V")
                {

                    encrypted = encrypted + V;
                }
                else if (str.Substring(ii, 1) == "w")
                {
                    encrypted = encrypted + w;

                }
                else if (str.Substring(ii, 1) == "W")
                {

                    encrypted = encrypted + W;
                }
                else if (str.Substring(ii, 1) == "x")
                {
                    encrypted = encrypted + x;

                }
                else if (str.Substring(ii, 1) == "X")
                {
                    encrypted = encrypted + X;

                }
                else if (str.Substring(ii, 1) == "y")
                {
                    encrypted = encrypted + y;

                }
                else if (str.Substring(ii, 1) == "Y")
                {
                    encrypted = encrypted + Y;

                }
                else if (str.Substring(ii, 1) == "z")
                {
                    encrypted = encrypted + z;

                }
                else if (str.Substring(ii, 1) == "Z")
                {
                    encrypted = encrypted + Z;

                }
                else if (str.Substring(ii, 1) == "0")
                {
                    encrypted = encrypted + num0;

                }
                else if (str.Substring(ii, 1) == "1")
                {
                    encrypted = encrypted + num1;

                }
                else if (str.Substring(ii, 1) == "2")
                {
                    encrypted = encrypted + num2;

                }
                else if (str.Substring(ii, 1) == "3")
                {
                    encrypted = encrypted + num3;

                }
                else if (str.Substring(ii, 1) == "4")
                {
                    encrypted = encrypted + num4;

                }
                else if (str.Substring(ii, 1) == "5")
                {
                    encrypted = encrypted + num5;

                }
                else if (str.Substring(ii, 1) == "6")
                {
                    encrypted = encrypted + num6;

                }
                else if (str.Substring(ii, 1) == "7")
                {
                    encrypted = encrypted + num7;

                }
                else if (str.Substring(ii, 1) == "8")
                {
                    encrypted = encrypted + num8;

                }
                else if (str.Substring(ii, 1) == "9")
                {
                    encrypted = encrypted + num9;

                }
                else
                {

                    encrypted = encrypted + "";

                }

                if (encrypted.Length < 64)
                {
                    int addedvalue = 0;
                    for (int count = 0; count < (64 - encrypted.Length) - 2; count++)
                    {
                        encrypted = encrypted + '*';
                        addedvalue = count;
                    }
                    encrypted = encrypted + addedvalue;
                }



            }
            MD5 md5hash = MD5.Create();
            string encryptedhash = GetMd5Hash(md5hash, encrypted);

            return encryptedhash;



        }

        public String EncryptBothWay(String str)
        {
            String encrypted = "";
            for (int ii = 0; ii < str.Length; ii++)
            {
                if (str.Substring(ii, ii + 1) == a)
                {

                    encrypted = encrypted + a;
                }

                else if (str.Substring(ii, ii + 1) == b)
                {
                    encrypted = encrypted + b;

                }
                else if (str.Substring(ii, ii + 1) == c)
                {
                    encrypted = encrypted + c;

                }
                else if (str.Substring(ii, ii + 1) == d)
                {
                    encrypted = encrypted + d;
                }
                else if (str.Substring(ii, ii + 1) == e)
                {
                    encrypted = encrypted + e;

                }
                else if (str.Substring(ii, ii + 1) == f)
                {
                    encrypted = encrypted + f;

                }
                else if (str.Substring(ii, ii + 1) == g)
                {
                    encrypted = encrypted + g;
                }
                else if (str.Substring(ii, ii + 1) == h)
                {
                    encrypted = encrypted + h;
                }
                else if (str.Substring(ii, ii + 1) == i)
                {

                    encrypted = encrypted + i;

                }
                else if (str.Substring(ii, ii + 1) == j)
                {

                    encrypted = encrypted + j;
                }
                else if (str.Substring(ii, ii + 1) == k)
                {
                    encrypted = encrypted + k;
                }
                else if (str.Substring(ii, ii + 1) == l)
                {
                    encrypted = encrypted + l;

                }
                else if (str.Substring(ii, ii + 1) == m)
                {
                    encrypted = encrypted + m;

                }
                else if (str.Substring(ii, ii + 1) == n)
                {
                    encrypted = encrypted + n;
                }
                else if (str.Substring(ii, ii + 1) == o)
                {
                    encrypted = encrypted + o;
                }
                else if (str.Substring(ii, ii + 1) == p)
                {
                    encrypted = encrypted + p;
                }
                else if (str.Substring(ii, ii + 1) == q)
                {
                    encrypted = encrypted + q;
                }
                else if (str.Substring(ii, ii + 1) == r)
                {
                    encrypted = encrypted + r;

                }
                else if (str.Substring(ii, ii + 1) == s)
                {
                    encrypted = encrypted + s;



                }
                else if (str.Substring(ii, ii + 1) == t)
                {
                    encrypted = encrypted + t;

                }
                else if (str.Substring(ii, ii + 1) == u)
                {
                    encrypted = encrypted + u;


                }
                else if (str.Substring(ii, ii + 1) == v)
                {
                    encrypted = encrypted + v;
                }
                else if (str.Substring(ii, ii + 1) == w)
                {
                    encrypted = encrypted + w;

                }
                else if (str.Substring(ii, ii + 1) == x)
                {
                    encrypted = encrypted + x;

                }
                else if (str.Substring(ii, ii + 1) == y)
                {
                    encrypted = encrypted + y;

                }
                else if (str.Substring(ii, ii + 1) == z)
                {
                    encrypted = encrypted + z;

                }
                else if (str.Substring(ii, ii + 1) == num0)
                {
                    encrypted = encrypted + num0;

                }
                else if (str.Substring(ii, ii + 1) == num1)
                {
                    encrypted = encrypted + num1;

                }
                else if (str.Substring(ii, ii + 1) == num2)
                {
                    encrypted = encrypted + num2;

                }
                else if (str.Substring(ii, ii + 1) == num3)
                {
                    encrypted = encrypted + num3;

                }
                else if (str.Substring(ii, ii + 1) == num4)
                {
                    encrypted = encrypted + num4;

                }
                else if (str.Substring(ii, ii + 1) == num5)
                {
                    encrypted = encrypted + num5;

                }
                else if (str.Substring(ii, ii + 1) == num6)
                {
                    encrypted = encrypted + num6;

                }
                else if (str.Substring(ii, ii + 1) == num7)
                {
                    encrypted = encrypted + num7;

                }
                else if (str.Substring(ii, ii + 1) == num8)
                {
                    encrypted = encrypted + num8;

                }
                else if (str.Substring(ii, ii + 1) == num9)
                {
                    encrypted = encrypted + num9;

                }
                else
                {

                    encrypted = encrypted + "";

                }

                if (encrypted.Length < 64)
                {
                    int addedvalue = 0;
                    for (int count = 0; count < (64 - encrypted.Length) - 2; count++)
                    {
                        encrypted = encrypted + '*';
                        addedvalue = count;
                    }
                    encrypted = encrypted + addedvalue;
                }



            }



            return encrypted;
        }

        private string GetMd5Hash(MD5 md5hash, string encryptedascii)
        {

            byte[] data = md5hash.ComputeHash(Encoding.UTF8.GetBytes(encryptedascii));


            StringBuilder sBuilder = new StringBuilder();


            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }


            return sBuilder.ToString();
        }

        public bool verifypass(string input, string hash)
        {

            MD5 md5hash1 = MD5.Create();

            bool result = VerifyMd5Hash(md5hash1, input, hash);

            return result;
        }

        public bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {

            string hashOfInput = GetMd5Hash(md5Hash, input);


            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }




    }
}

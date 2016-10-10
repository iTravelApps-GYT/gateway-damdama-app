using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAJdamaProject.Models
{
    public class TajDama
    {
        public List<ModelAppcode> objHomeL { get; set; }
        public ModelAppcode objHome { get; set; }
    }
  
    public class Sessiondata
    {
        
    }
    public enum charset
    {
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        E = 5,
        F = 6,
        G = 7,
        H = 8,
        I = 9,
        J = 10,
        K = 11,
        L = 12,
        M = 13,
        N = 14,
        O = 15,
        P = 16,
        Q = 17,
        R = 18,
        S = 19,
        T = 20,
        U = 21,
        V = 22,
        W = 23,
        X = 24,
        Y = 25,
        Z = 26


    };
    public class ModelAppcode
    {
        public string QR { get; set; }
        public string ID { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string RoomNo { get; set; }
        public string Checkin { get; set; }
        public string Checkout { get; set; }
        public string DDLValue { get; set; }
        public string ImgURL { get; set; }
        public string val { get; set; }
    }
    public class GET_APPCODE
    {

        public string QR(string[] value)
        {
            string result = string.Empty;
            try
            {
                foreach (var item in value)
                {
                    if (item != null)
                    {
                        //$ in starting &#S2958$
                        result = string.Concat(result, "&#S2958$");

                        foreach (var ch in item)
                        {
                            // Each single digit will digit will become 2 digit number
                            if (Char.IsNumber(ch))
                            {
                                result = string.Concat(result, int.Parse(ch.ToString()) + 10); // + 10
                            }
                            // Space =50
                            else if (char.IsWhiteSpace(ch))
                            {
                                result = string.Concat(result, 50);
                            }
                            // hyphen  =49
                            else if (ch.ToString().Contains("-"))
                            {
                                result = string.Concat(result, 49);
                            }
                            // comma  =47
                            else if (ch.ToString().Contains(","))
                            {
                                result = string.Concat(result, 48);
                            }
                            // Each alphabetic character will become 2 digit number 
                            else
                            {
                                charset key = (charset)Enum.Parse(typeof(charset), ch.ToString(), true);
                                result = string.Concat(result, (int)key + 20); // + 20
                            }

                        }
                    }
                    if (result.Length > 0)
                    {
                        //$ at last &#S2958$
                        result = string.Concat(result, "&#S2958$");
                    }
                }
            }
            catch
            {

            }

            return result;
        }
    }
}
using System;
using System.Text.RegularExpressions;

public class ValidateCreditCard
{
    public static void Main(string[] args)
    {
        // Example usage for Visa 
        string visaCardNumber = "4111111111111111";
        var visaResult = ValidateCreditCardNumber(visaCardNumber);
        Console.WriteLine($"Card Number: {visaCardNumber}, Valid: {visaResult.Valid}, Bandeira: {visaResult.Bandeira}");

        // Example usage for MasterCard
        string masterCardNumber = "5277959558870483";
        var masterCardResult = ValidateCreditCardNumber(masterCardNumber);
        Console.WriteLine($"Card Number: {masterCardNumber}, Valid: {masterCardResult.Valid}, Bandeira: {masterCardResult.Bandeira}");

        // Example usage for American Express
        string amexCardNumber = "378282246310005";
        var amexResult = ValidateCreditCardNumber(amexCardNumber);
        Console.WriteLine($"Card Number: {amexCardNumber}, Valid: {amexResult.Valid}, Bandeira: {amexResult.Bandeira}");

        // Example usage for Discover
        string discoverCardNumber = "6011111111111117";
        var discoverResult = ValidateCreditCardNumber(discoverCardNumber);
        Console.WriteLine($"Card Number: {discoverCardNumber}, Valid: {discoverResult.Valid}, Bandeira: {discoverResult.Bandeira}");

        // Example usage for Diners Club
        string dinersCardNumber = "30569309025904";
        var dinersResult = ValidateCreditCardNumber(dinersCardNumber);
        Console.WriteLine($"Card Number: {dinersCardNumber}, Valid: {dinersResult.Valid}, Bandeira: {dinersResult.Bandeira}");

        // Example usage for JCB
        string jcbCardNumber = "3530111333300000";
        var jcbResult = ValidateCreditCardNumber(jcbCardNumber);
        Console.WriteLine($"Card Number: {jcbCardNumber}, Valid: {jcbResult.Valid}, Bandeira: {jcbResult.Bandeira}");

        // Example usage for Elo
        string eloCardNumber = "6363680000000000";
        var eloResult = ValidateCreditCardNumber(eloCardNumber);
        Console.WriteLine($"Card Number: {eloCardNumber}, Valid: {eloResult.Valid}, Bandeira: {eloResult.Bandeira}");

        // Example usage for Hipercard
        string hipercardCardNumber = "6062825624254001";
        var hipercardResult = ValidateCreditCardNumber(hipercardCardNumber);
        Console.WriteLine($"Card Number: {hipercardCardNumber}, Valid: {hipercardResult.Valid}, Bandeira: {hipercardResult.Bandeira}");

        // Example usage for Aura
        string auraCardNumber = "5078601870000127980";
        var auraResult = ValidateCreditCardNumber(auraCardNumber);
        Console.WriteLine($"Card Number: {auraCardNumber}, Valid: {auraResult.Valid}, Bandeira: {auraResult.Bandeira}");
    }

    public static Result ValidateCreditCardNumber(string cardNumber)
    {
        // Remove all non-digit characters
        string sanitized = Regex.Replace(cardNumber, @"\D", "");

        // Check if the card number is valid using Luhn Algorithm
        if (!LuhnCheck(sanitized))
        {
            return new Result(false, null);
        }

        // Determine the card issuer (bandeira)
        string bandeira = GetCardIssuer(sanitized);

        return new Result(true, bandeira);
    }

    public static bool LuhnCheck(string cardNumber)
    {
        int sum = 0;
        bool shouldDouble = false;

        // Loop through the card number digits in reverse order
        for (int i = cardNumber.Length - 1; i >= 0; i--)
        {
            int digit = int.Parse(cardNumber[i].ToString());

            if (shouldDouble)
            {
                digit *= 2;
                if (digit > 9)
                {
                    digit -= 9;
                }
            }

            sum += digit;
            shouldDouble = !shouldDouble;
        }

        return sum % 10 == 0;
    }

    public static string GetCardIssuer(string cardNumber)
    {
        var patterns = new (string Pattern, string Issuer)[]
        {
            (@"^4[0-9]{12}(?:[0-9]{3})?$", "visa"),
            (@"^5[1-5][0-9]{14}$", "mastercard"),
            (@"^3[47][0-9]{13}$", "amex"),
            (@"^6(?:011|5[0-9]{2})[0-9]{12}$", "discover"),
            (@"^3(?:0[0-5]|[68][0-9])[0-9]{11}$", "diners"),
            (@"^(?:2131|1800|35\d{3})\d{11}$", "jcb"),
            (@"^((636368)|(438935)|(504175)|(451416)|(636297)|(5067)|(4576)|(4011))\d+$", "elo"),
            (@"^(606282\d{10}(\d{3})?)|(3841\d{15})$", "hipercard"),
            (@"^50[0-9]{14,17}$", "aura")
        };

        foreach (var (pattern, issuer) in patterns)
        {
            if (Regex.IsMatch(cardNumber, pattern))
            {
                return issuer;
            }
        }

        return "unknown";
    }

    public class Result
    {
        public bool Valid { get; }
        public string Bandeira { get; }

        public Result(bool valid, string bandeira)
        {
            Valid = valid;
            Bandeira = bandeira;
        }
    }
}
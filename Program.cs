using CreditScoreCalculator.Models;
using System.Collections.Generic;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

// Helper method to calculate credit score
int CalculateCreditScore(CustomerRecords customer)
{
    double paymentHistory = customer.PaymentHistory;
    double creditUtilization = customer.CreditUtilization;
    int ageOfCreditHistory = customer.AgeOfCreditHistory;
    double score = (0.4 * paymentHistory) + (0.3 * (100 - creditUtilization)) + (0.3 * Math.Min(ageOfCreditHistory, 10));
    return (int)Math.Round(score);
}

// Helper method to determine risk status
string GetRiskStatus(int score) {
    if (score < 50)
        return "High Risk";
    else
        return "Low Risk";
}
;

// Read input (JSON array)
Console.WriteLine("Enter customers details as a JSON array:");
string? customerRecords = Console.ReadLine();
if (string.IsNullOrWhiteSpace(customerRecords))
{
    Console.WriteLine("No input provided.");
    return;
}

List<CustomerRecords>? customers = null;
try
{
    customers = JsonSerializer.Deserialize<List<CustomerRecords>>(customerRecords);
}
catch (Exception ex)
{
    Console.WriteLine($"Invalid input format: {ex.Message}");
    return;
}

if (customers == null || customers.Count == 0)
{
    Console.WriteLine("No valid customer records found.");
    return;
}

// Prepare report
var report = new List<object>();
foreach (var customer in customers)
{
    int score = CalculateCreditScore(customer);
    string risk = GetRiskStatus(score);
    Console.WriteLine($"{customer.Name}: Credit Score = {score}, Risk Status = {risk}");
    report.Add(new { customer.Name, CreditScore = score, RiskStatus = risk });
}

// Save report to JSON file
string reportJson = JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true });
File.WriteAllText("HighRiskReport.json", reportJson);
Console.WriteLine("\nReport saved to HighRiskReport.json");


//{ "CustomerId": 1, "Name": "Alice", "PaymentHistory": 90, "CreditUtilization": 40, "AgeOfCreditHistory": 5 }, 
//{ "CustomerId": 2, "Name": "Bob", "PaymentHistory": 70, "CreditUtilization": 90, "AgeOfCreditHistory": 15 }, 
//{ "CustomerId": 3, "Name": "Charlie", "PaymentHistory": 60, "CreditUtilization": 30, "AgeOfCreditHistory": 2 }

//[{ "CustomerId": 1, "Name": "Alice", "PaymentHistory": 90, "CreditUtilization": 40, "AgeOfCreditHistory": 5 }, { "CustomerId": 3, "Name": "Charlie", "PaymentHistory": 60, "CreditUtilization": 30, "AgeOfCreditHistory": 2 }]

using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class MortgageCalculatorService
{
    private readonly AppDbContext _context;

    public MortgageCalculatorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<MortgageCalculation> CalculateLoan(
        int userId,
        decimal loanAmount,
        double annualInterestRate,
        int loanTermMonths)
    {
        if (loanAmount <= 0)
            throw new ArgumentOutOfRangeException(nameof(loanAmount));

        if (annualInterestRate < 0)
            throw new ArgumentOutOfRangeException(nameof(annualInterestRate));

        if (loanTermMonths <= 0)
            throw new ArgumentOutOfRangeException(nameof(loanTermMonths));

        var monthlyRate = annualInterestRate / 100.0 / 12.0;

        decimal monthlyPayment;

        if (monthlyRate == 0)
        {
            monthlyPayment = loanAmount / loanTermMonths;
        }
        else
        {
            var factor =
                Math.Pow(1 + monthlyRate, loanTermMonths);

            monthlyPayment =
                loanAmount *
                (decimal)((monthlyRate * factor) / (factor - 1));
        }

        monthlyPayment = decimal.Round(
            monthlyPayment,
            2,
            MidpointRounding.AwayFromZero);

        var totalPayment =
            decimal.Round(
                monthlyPayment * loanTermMonths,
                2,
                MidpointRounding.AwayFromZero);

        var calculation = new MortgageCalculation
        {
            UserId = userId,
            LoanAmount = loanAmount,
            InterestRate = annualInterestRate,
            LoanTermMonths = loanTermMonths,
            MonthlyPayment = monthlyPayment,
            TotalPayment = totalPayment,
            TotalInterest = totalPayment - loanAmount,
            CreatedAt = DateTime.UtcNow
        };

        _context.MortgageCalculations.Add(calculation);

        await _context.SaveChangesAsync();

        return calculation;
    }

    public async Task<List<MortgageCalculation>> GetUserCalculations(
        int userId)
    {
        return await _context.MortgageCalculations
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> DeleteCalculation(
        int id,
        int userId)
    {
        if (id <= 0 || userId <= 0)
            return false;

        var calculation = await _context.MortgageCalculations
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.UserId == userId);

        if (calculation is null)
            return false;

        _context.MortgageCalculations.Remove(calculation);

        await _context.SaveChangesAsync();

        return true;
    }
}

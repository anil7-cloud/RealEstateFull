using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class MortgageService
{
    private readonly AppDbContext _context;

    public MortgageService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<MortgageCalculation> CreateCalculation(
        int userId,
        int propertyId,
        decimal propertyPrice,
        decimal downPayment,
        int loanTermMonths,
        double interestRate)
    {
        if (propertyPrice <= 0)
            throw new ArgumentOutOfRangeException(nameof(propertyPrice));

        if (downPayment < 0 || downPayment >= propertyPrice)
            throw new ArgumentOutOfRangeException(nameof(downPayment));

        if (loanTermMonths <= 0)
            throw new ArgumentOutOfRangeException(nameof(loanTermMonths));

        if (interestRate < 0)
            throw new ArgumentOutOfRangeException(nameof(interestRate));

        var loanAmount = propertyPrice - downPayment;
        var monthlyRate = interestRate / 100d / 12d;

        decimal monthlyPayment;

        if (monthlyRate == 0)
        {
            monthlyPayment = loanAmount / loanTermMonths;
        }
        else
        {
            var factor = Math.Pow(
                1d + monthlyRate,
                loanTermMonths);

            monthlyPayment = loanAmount * (decimal)(
                monthlyRate * factor /
                (factor - 1d));
        }

        var calculation = new MortgageCalculation
        {
            UserId = userId,
            PropertyId = propertyId,
            PropertyPrice = propertyPrice,
            DownPayment = downPayment,
            LoanAmount = loanAmount,
            InterestRate = interestRate,
            LoanTermMonths = loanTermMonths,
            MonthlyPayment = monthlyPayment,
            CreatedAt = DateTime.UtcNow
        };

        _context.MortgageCalculations.Add(calculation);
        await _context.SaveChangesAsync();

        return calculation;
    }


    public async Task<MortgageCalculation?> GetCalculationById(
        int id,
        int userId)
    {
        if (id <= 0 || userId <= 0)
            return null;

        return await _context.MortgageCalculations
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.UserId == userId);
    }


    public async Task<List<MortgageCalculation>> GetAllCalculations()
    {
        return await _context.MortgageCalculations
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<List<MortgageCalculation>> GetUserCalculations(int userId)
    {
        return await _context.MortgageCalculations
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<List<MortgageCalculation>> GetPropertyCalculations(int propertyId)
    {
        return await _context.MortgageCalculations
            .Where(x => x.PropertyId == propertyId)
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


    public async Task<bool> UpdateCalculation(
        int id,
        int userId,
        decimal propertyPrice,
        decimal downPayment,
        int loanTermMonths,
        double interestRate)
    {
        if (id <= 0 || userId <= 0)
            return false;

        if (propertyPrice <= 0 ||
            downPayment < 0 ||
            downPayment >= propertyPrice ||
            loanTermMonths <= 0 ||
            interestRate < 0)
            return false;

        var calculation = await _context.MortgageCalculations
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.UserId == userId);

        if (calculation is null)
            return false;

        calculation.PropertyPrice = propertyPrice;
        calculation.DownPayment = downPayment;
        calculation.LoanAmount = propertyPrice - downPayment;
        calculation.LoanTermMonths = loanTermMonths;
        calculation.InterestRate = interestRate;

        var monthlyRate = interestRate / 100d / 12d;

        if (monthlyRate == 0)
        {
            calculation.MonthlyPayment =
                calculation.LoanAmount / loanTermMonths;
        }
        else
        {
            var factor = Math.Pow(
                1d + monthlyRate,
                loanTermMonths);

            calculation.MonthlyPayment =
                calculation.LoanAmount * (decimal)(
                    monthlyRate * factor /
                    (factor - 1d));
        }

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<List<MortgageCalculation>> GetLatestCalculations(int count)
    {
        if (count <= 0)
            count = 10;

        return await _context.MortgageCalculations
            .OrderByDescending(x => x.CreatedAt)
            .Take(count)
            .ToListAsync();
    }


    public async Task<decimal> GetAverageLoanAmount()
    {
        if (!await _context.MortgageCalculations.AnyAsync())
            return 0;

        return await _context.MortgageCalculations
            .AverageAsync(x => x.LoanAmount);
    }

}

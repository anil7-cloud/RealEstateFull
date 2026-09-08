using Core.Application.DTOs.PropertyMedia;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyMediaService
{
    Task<List<PropertyMediaDto>> GetByPropertyIdAsync(
        int propertyId,
        CancellationToken cancellationToken = default);

    Task<PropertyMediaDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<PropertyMediaDto> CreateAsync(
        CreatePropertyMediaDto dto,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(
        int id,
        UpdatePropertyMediaDto dto,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<bool> SetPrimaryAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<bool> ReorderAsync(
        int propertyId,
        List<int> mediaIds,
        CancellationToken cancellationToken = default);
}

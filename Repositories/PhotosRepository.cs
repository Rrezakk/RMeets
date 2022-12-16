using RMeets.Contexts;
using RMeets.Models;

namespace RMeets.Repositories;

public class PhotosRepository
{
    private readonly ApplicationContext _context;

    public PhotosRepository(ApplicationContext context)
    {
        _context = context;
    }
    public List<BlankPhoto> GetBlankPhotosById(int blankId) =>
        _context.BlankPhotos.Where(p => p.BlankId == blankId).ToList();
}
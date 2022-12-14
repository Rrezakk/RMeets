using RMeets.Models;
using RMeets.Repositories;

namespace RMeets;

public class BlankDataService
{
    private InterestRepository _interestRepository;
    private TargetRepository _targetRepository;
    private FactRepository _factRepository;
    private CityRepository _cityRepository;
    private GenderRepository _genderRepository;
    private ProfileRepository _profileRepository;
    private PhotosRepository _photosRepository;
    private BlankRepository _blankRepository;
    public BlankDataService(InterestRepository interestRepository, TargetRepository targetRepository, FactRepository factRepository, CityRepository cityRepository, GenderRepository genderRepository, ProfileRepository profileRepository, PhotosRepository photosRepository, BlankRepository blankRepository)
    {
        _interestRepository = interestRepository;
        _targetRepository = targetRepository;
        _factRepository = factRepository;
        _cityRepository = cityRepository;
        _genderRepository = genderRepository;
        _profileRepository = profileRepository;
        _photosRepository = photosRepository;
        _blankRepository = blankRepository;
    }
    public List<Gender> GetGenders() => _genderRepository.GetAll();
    public List<Interest> GetInterests => _interestRepository.GetAll();
    public List<Fact> GetFacts => _factRepository.GetAll();
    public List<City> GetCities => _cityRepository.GetAll();
    public List<Target> GetTargets => _targetRepository.GetAll();
    public List<Blank> GetProfileBlanks(int id) => _blankRepository.GetByProfileId(id);
    public Blank CreateBlank(Blank blank)
    {
        return _blankRepository.Create(blank);
    }
    public Blank EditBlank(Blank blank)
    {
        return _blankRepository.Edit(blank);
    }
}

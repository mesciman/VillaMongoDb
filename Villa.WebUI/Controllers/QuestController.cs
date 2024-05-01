using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Villa.Business.Abstract;
using Villa.Business.Validators;
using Villa.Dto.Dtos.QuestDtos;
using Villa.Entity.Entities;

namespace Villa.WebUI.Controllers
{
    public class QuestController : Controller
    {
        private readonly IQuestService _questService;
        private readonly IMapper _mapper;

        public QuestController(IQuestService bannerService, IMapper mapper)
        {
            _questService = bannerService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _questService.TGetAllAsync();
            var bannerList = _mapper.Map<List<ResultQuestDto>>(values);
            return View(bannerList);

        }


        public async Task<IActionResult> DeleteQuest(ObjectId id)
        {
            await _questService.TDeleteAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult CreateQuest()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuest(CreateQuestDto createQuestDto)
        {
            var newValue = _mapper.Map<Quest>(createQuestDto);
            
            var validator = new QuestionValidator();
            var result = validator.Validate(newValue);
            if(!result.IsValid)
            {
                result.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
                return View(createQuestDto);
            }

            await _questService.TCreateAsync(newValue);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateQuest(ObjectId id)
        {
            var value = await _questService.TGetByIdAsync(id);
            var updateQuest = _mapper.Map<UpdateQuestDto>(value);
            return View(updateQuest);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuest(UpdateQuestDto updateQuestDto)
        {
            var banner = _mapper.Map<Quest>(updateQuestDto);
            await _questService.TUpdateAsync(banner);
            return RedirectToAction("Index");
        }

    }
}
